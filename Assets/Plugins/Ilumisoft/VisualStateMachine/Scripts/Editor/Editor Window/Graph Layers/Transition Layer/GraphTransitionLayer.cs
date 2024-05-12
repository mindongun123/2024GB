namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class GraphTransitionLayer : GraphLayer
    {
        [System.NonSerialized] 
        private readonly GUIStyle labelStyle;

        private Dictionary<string, Node> nodeDict = new Dictionary<string, Node>();

        public GraphTransitionLayer(EditorWindow view) : base(view)
        {
            labelStyle = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 9,
                fontStyle = FontStyle.Normal,
            };

            labelStyle.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        }

        /// <summary>
        /// Draws all transitions
        /// </summary>
        /// <param name="rect"></param>
        public override void Draw(Rect rect)
        {
            base.Draw(rect);

            UpdateNodeDictionary();

            DrawTransitions();
            DrawTransitionPreview();
        }

        private void UpdateNodeDictionary()
        {
            nodeDict.Clear();

            foreach (var node in Context.Graph.Nodes)
            {
                nodeDict.Add(node.ID, node);
            }
        }

        private bool TryGetTransitionNodes(Transition transition, out Node origin, out Node target)
        {
            if(this.Context.Graph.TryGetNode(transition.OriginID, out origin) && this.Context.Graph.TryGetNode(transition.TargetID, out target))
            {
                return true;
            }

            origin = null;
            target = null;

            return false;
        }

        /// <summary>
        /// Draws all transitions of the loaded state machine
        /// </summary>
        private void DrawTransitions()
        {
            var graph = this.Context.Graph;

            foreach (var transition in graph.Transitions)
            {
                DrawTransition(transition);
            }
        }

        /// <summary>
        /// Draws the given transition
        /// </summary>
        /// <param name="transition"></param>
        private void DrawTransition(Transition transition)
        {
            if (TryGetTransitionNodes(transition, out Node originNode, out Node targetNode))
            {
                Color color = GetTransitionColor(transition);

                Rect startRect = GetTransformedRect(originNode.Rect);

                Rect endRect = GetTransformedRect(targetNode.Rect);

                Vector2 offset = GetTransitionOffset(originNode, targetNode, startRect, endRect);

                endRect.center += offset;

                DrawTransition(startRect.center + offset, endRect, color);

                if (Context.ShowLabels)
                {
                    DrawLabel(transition, originNode, targetNode);
                }
            }
        }

        private Color GetTransitionColor(Transition transition)
        {
            if (Selection.activeObject == TransitionInspectorHelper.Instance && TransitionInspectorHelper.Instance.TransitionID == transition.ID)
            {
                return GraphColors.SelectionColor;
            }
            else
            {
                return Color.white;
            }
        }

        private void DrawTransitionPreview()
        {
            if (this.Context.TransitionPreview != null)
            {
                DrawTransition(GetTransformedRect(this.Context.TransitionPreview.Rect).center, new Rect(Event.current.mousePosition, Vector2.one), Color.white);
            }
        }

        private Vector2 GetTransitionOffset(Node originNode, Node targetNode, Rect originRect, Rect targetRect)
        {
            if (originNode is AnyState)
            {
                return Vector2.zero;
            }

            Vector2 distance = originRect.center - targetRect.center;

            Vector2 offset = Vector2.zero;

            if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
            {
                offset.x = Mathf.Sign(distance.y) * 5.0f;
            }
            else
            {
                offset.y = Mathf.Sign(distance.x) * 5.0f;
            }

            return offset * this.Context.ZoomFactor;
        }

        private Vector2 GetLabelOffset(Node originNode, Node targetNode)
        {
            Vector2 distance = GetTransformedRect(originNode.Rect).center - GetTransformedRect(targetNode.Rect).center;

            Vector2 offset = Vector2.zero;

            float factor = (originNode is AnyState) ? 4.0f : 5.0f;

            if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
            {
                offset.x = Mathf.Sign(distance.y) * factor;
            }
            else
            {
                offset.y = Mathf.Sign(distance.x) * factor;
            }

            return offset * this.Context.ZoomFactor;
        }

        private void DrawTransition(Vector2 startPos, Rect end, Color color)
        {
            end.x -= 2;
            end.y -= 2;
            end.width += 4;
            end.height += 4;

            Line transitionLine = new Line(startPos, end.center);

            //Compute the angle of startPos->endPos
            float angle = Vector2.SignedAngle(Vector2.right, transitionLine.Direction) + 90.0f;

            //Create a triangle on origin
            Vector3[] triangle = {
                new Vector2(-1, 0.5f) * (4 * this.Context.ZoomFactor),
                new Vector2(1, 0.5f) * (4 * this.Context.ZoomFactor),
                new Vector2(0, -0.5f) * (14 * this.Context.ZoomFactor)
            };

            //Rotate the triangle and move it to the center of the line
            for (int i = 0; i < triangle.Length; i++)
            {
                triangle[i] = Quaternion.Euler(0, 0, angle) * triangle[i];

                Vector2 pos = transitionLine.Lerp(0.45f);

                triangle[i] += (Vector3)(pos);
            }

            //Is the line intersecting with the windows viewport (= visible)? Draw it
            if (transitionLine.Intersects(EditorWindow.Rect))
            {
                //Begin drawing
                Handles.BeginGUI();

                Handles.color = color;

                //Draw line
                Handles.DrawAAPolyLine(3.0f * this.Context.ZoomFactor, startPos, end.center);

                //Draw triangle
                Handles.DrawAAConvexPolygon(triangle);

                //End drawing
                Handles.EndGUI();
            }
        }

        void DrawLabel(Transition transition, Node origin, Node target)
        {
            Vector2 offset = GetLabelOffset(origin, target);

            Rect originRect = GetTransformedRect(origin.Rect);
            Rect targetRect = GetTransformedRect(target.Rect);

            Vector2 direction = (targetRect.center - originRect.center);
            Vector2 position = originRect.center+offset*4 + direction *0.45f;

            Rect renderRect = new Rect(Vector2.zero, new Vector2(100, 20));

            renderRect.center = position;

            float angle = Vector2.SignedAngle(Vector2.right, direction);

            if (angle < -95 || angle>85)
            {
                angle += 180;
            }

            var matrix = GUI.matrix;

            string label = transition.Label;

            if(transition.Duration>0.0f)
            {
                label += $" [{transition.Duration}s]";
            }

            GUIUtility.ScaleAroundPivot(new Vector2(Context.ZoomFactor, Context.ZoomFactor), position);
            GUIUtility.RotateAroundPivot(angle, position);
            EditorGUI.LabelField(renderRect, label, labelStyle);

            GUI.matrix = matrix;
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

                    Transition transition = GetClickedTransition(this.EditorWindow, mousePos);

                    if (transition != null)
                    {
                        this.Context.SelectedNodes.Clear();

                        TransitionInspectorHelper.Instance.Inspect(this.Context.StateMachine, transition);

                        Event.current.Use();
                    }

                    break;

                case EventType.MouseDrag:
                    break;
            }
        }

        /// <summary>
        /// Gets called when an interaction with the right mouse button has been made.
        /// </summary>
        /// <param name="mousePos"></param>
        protected override void OnRightMouseButtonEvent(Vector2 mousePos)
        {
            if (Context.IsPlayMode || Context.IsPrefabAsset)
            {
                return;
            }

            Transition transition = GetClickedTransition(this.EditorWindow, mousePos);

            if (transition == null)
                return;


            switch (Event.current.type)
            {
                //Select clicked state 
                case EventType.MouseDown:

                    this.Context.SelectedNodes.Clear();

                    TransitionInspectorHelper.Instance.Inspect(this.Context.StateMachine, transition);

                    Event.current.Use();

                    break;

                //Show context menu for selected state
                case EventType.MouseUp:

                    Selection.activeObject = null;

                    this.Context.SelectedNodes.Clear();

                    Event.current.Use();

                    IContextMenu contextMenu = new TransitionContextMenu(Context.StateMachine, transition);

                    contextMenu.Show();

                    break;
            }
        }

        protected override void OnMouseMoveEvent(Vector2 mousePos)
        {
            //Trigger the gui to repaint when a transition is pulled
            if (this.Context.TransitionPreview != null)
            {
                GUI.changed = true;
            }
        }

        protected override void OnKeyUp(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Delete)
            {
                if (this.Context.SelectedNodes.Count == 0)
                {
                    if (Selection.activeObject == TransitionInspectorHelper.Instance)
                    {
                        var transition = TransitionInspectorHelper.Instance.Transition;

                        if (this.Context.Graph.Transitions.Contains(transition))
                        {
                            this.Context.StateMachine.DeleteTransition(transition);
                            Selection.activeObject = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the first state found, which contains the mouse position
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public Transition GetClickedTransition(EditorWindow view, Vector2 mousePos)
        {
            float maxDistance = 5.0f * this.Context.ZoomFactor;

            var graph = this.Context.Graph;

            foreach (var transition in graph.Transitions)
            {
                if (TryGetTransitionNodes(transition, out Node originNode, out Node targetNode))
                {
                    Rect originRect = GetTransformedRect(originNode.Rect);
                    Rect targetRect = GetTransformedRect(targetNode.Rect);

                    Vector2 a = originRect.center + GetTransitionOffset(originNode, targetNode, originRect, targetRect);
                    Vector2 c = targetRect.center + GetTransitionOffset(originNode, targetNode, originRect, targetRect);

                    Line transitionLine = new Line(a, c);

                    Rect rect = new Rect()
                    {
                        x = Mathf.Min(a.x, c.x) - maxDistance,
                        y = Mathf.Min(a.y, c.y) - maxDistance,
                        width = Mathf.Max(a.x, c.x) - Mathf.Min(a.x, c.x) + 2 * maxDistance,
                        height = Mathf.Max(a.y, c.y) - Mathf.Min(a.y, c.y) + 2 * maxDistance
                    };

                    if (rect.Contains(mousePos))
                    {
                        if (transitionLine.GetMinDistance(mousePos) < maxDistance)
                        {
                            return transition;
                        }
                    }
                }
            }

            return null;
        }
    }
}