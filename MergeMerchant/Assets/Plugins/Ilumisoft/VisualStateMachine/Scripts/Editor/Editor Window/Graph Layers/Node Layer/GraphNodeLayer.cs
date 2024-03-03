namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Graph Layer, responsible for drawing all states and handling the input on them
    /// </summary>
    public class GraphNodeLayer : GraphLayer
    {
        private readonly StateStyles stateStyles;

        private enum MouseDragState { None, OnDrag }

        private MouseDragState dragState = MouseDragState.None;

        public GraphNodeLayer(EditorWindow editorWindow) : base(editorWindow)
        {
            this.stateStyles = new StateStyles();
        }

        /// <summary>
        /// Draws all states of the loaded state machine
        /// </summary>
        /// <param name="rect"></param>
        public override void Draw(Rect rect)
        {
            base.Draw(rect);

            DrawNodes(rect);

            this.Context.SelectionRect.Draw();
        }

        private void DrawNodes(Rect rect)
        {
            this.stateStyles.ApplyZoomFactor(this.Context.ZoomFactor);

            var graph = this.Context.Graph;

            foreach (var node in graph.Nodes)
            {
                Rect nodeRect = GetTransformedRect(node.Rect);

                if (rect.Overlaps(nodeRect))
                {
                    DrawNode(node, nodeRect);
                }
            }
        }

        private void DrawNode(Node node, Rect rect)
        {
            string nodeName = node.ID;

            if (node is AnyState)
            {
                nodeName = "Any State";
            }

            GUI.Box(rect, nodeName, GetStateStyle(node));
        }

        /// <summary>
        /// Returns the appropriate style for a given state
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private GUIStyle GetStateStyle(Node node)
        {
            bool isSelected = this.Context.SelectedNodes.Contains(node);

            if (node is State state)
            {
                //Active state (playmode only)
                if (Application.isPlaying && state.ID == this.Context.StateMachine.CurrentState)
                {
                    return this.stateStyles.Get(isSelected ? StateStyles.Style.OrangeOn : StateStyles.Style.Orange);
                }
                //Entry State
                else if (state.ID == this.Context.Graph.EntryStateID)
                {
                    return this.stateStyles.Get(isSelected ? StateStyles.Style.GreenOn : StateStyles.Style.Green);
                }
                //Normal State
                else
                {
                    return this.stateStyles.Get(isSelected ? StateStyles.Style.NormalOn : StateStyles.Style.Normal);
                }
            }
            else if (node is AnyState anyState)
            {
                return this.stateStyles.Get(isSelected ? StateStyles.Style.MintOn : StateStyles.Style.Mint);
            }

            return null;
        }

        /// <summary>
        /// Gets there is an event on the left mouse button
        /// </summary>
        /// <param name="mousePos"></param>
        protected override void OnLeftMouseButtonEvent(Vector2 mousePos)
        {
            switch (Event.current.type)
            {
                //Only down events are relevant here
                case EventType.MouseDown:
                    {
                        this.dragState = MouseDragState.None;

                        var node = Context.Graph.GetClickedNode(this, mousePos);

                        //Has a state been clicked?
                        if (node != null)
                        {
                            //Is the clicked state the target of a new transition?
                            if (EditorApplication.isPlaying == false && this.Context.TransitionPreview != null && this.Context.TransitionPreview != node)
                            {
                                //Only states can be a target of a transition
                                if (node is State state)
                                {
                                    this.Context.StateMachine.AddTransition(this.Context.TransitionPreview, state);
                                    this.Context.TransitionPreview = null;
                                }
                            }

                            //Ctrl Pressed? Add/Remove State from selection
                            else if (EditorApplication.isPlaying == false && Event.current.control)
                            {
                                if (this.Context.SelectedNodes.Count > 0)
                                {
                                    Selection.activeObject = null;
                                }

                                if (this.Context.SelectedNodes.Contains(node))
                                {
                                    this.Context.SelectedNodes.Remove(node);
                                }
                                else
                                {
                                    this.Context.SelectedNodes.Add(node);
                                }
                            }
                            //Otherwise just select this state
                            else
                            {
                                if (this.Context.SelectedNodes.Count <2 || !this.Context.SelectedNodes.Contains(node))
                                {
                                    this.Context.SelectedNodes.Clear();
                                    this.Context.SelectedNodes.Add(node);

                                    //Selection.activeObject = state;
                                    if (node is State state)
                                    {
                                        StateInspectorHelper.Instance.Inspect(this.Context.StateMachine, this.Context.Graph, state);
                                    }
                                    else
                                    {
                                        if (Selection.activeObject == StateInspectorHelper.Instance)
                                        {
                                            Selection.activeObject = null;
                                        }
                                    }
                                }
                            }

                            Event.current.Use();
                        }

                        break;
                    }

                case EventType.MouseDrag:
                    {
                        if (this.Context.IsPlayMode || this.Context.IsPrefabAsset)
                        {
                            break;
                        }

                        if (!Event.current.control)
                        {
                            if (this.dragState == MouseDragState.None)
                            {
                                Undo.RegisterCompleteObjectUndo(this.Context.StateMachine, "Dragged state");
                                this.dragState = MouseDragState.OnDrag;
                            }
                            else
                            {
                                if (this.Context.SelectedNodes.Count > 0)
                                {
                                    Event.current.Use();
                                    GUI.changed = true;
                                }

                                foreach (Node node in this.Context.SelectedNodes)
                                {
                                    Rect rect = node.Rect;

                                    rect.x += Event.current.delta.x / this.Context.ZoomFactor;
                                    rect.y += Event.current.delta.y / this.Context.ZoomFactor;

                                    node.Rect = rect;
                                }
                            }
                        }

                        break;
                    }

                case EventType.MouseUp:

                    if (this.dragState == MouseDragState.OnDrag)
                    {
                        this.dragState = MouseDragState.None;
                    }

                    break;
            }
        }

        /// <summary>
        /// Gets called when an interaction with the right mouse button has been made.
        /// </summary>
        /// <param name="mousePos"></param>
        protected override void OnRightMouseButtonEvent(Vector2 mousePos)
        {
            if (this.Context.IsPlayMode || this.Context.IsPrefabAsset)
            {
                return;
            }

            var node = Context.Graph.GetClickedNode(this, mousePos);

            //Has a state been clicked?
            if (node != null)
            {
                ICommand command = null;

                switch (Event.current.type)
                {
                    //Select clicked state 
                    case EventType.MouseDown:

                        command = new SelectClickedStateCommand(Context, node);

                        break;

                    //Show context menu for selected state
                    case EventType.MouseUp:

                        this.dragState = MouseDragState.None;

                        command = new ShowContextMenuCommand(Context, node);

                        break;
                }

                command?.Execute();
            }
        }

        protected override void OnKeyUp(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Delete)
            {
                this.Context.GraphSelection.Delete();
            }
        }
    }
}