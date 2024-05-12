namespace Ilumisoft.VisualStateMachine.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class GraphView
    {
        /// <summary>
        /// Reference to the data of the editor
        /// </summary>
        private Context Context { get; }

        /// <summary>
        /// The list of layers
        /// </summary>
        private List<GraphLayer> Layers { get; } = new List<GraphLayer>();

        public GraphView(EditorWindow editorWindow)
        {
            this.Context = editorWindow.Context;

            //Add layers to the graph view
            this.Layers.Add(new GraphBackgroundLayer(editorWindow));
            this.Layers.Add(new GraphTransitionLayer(editorWindow));
            this.Layers.Add(new GraphNodeLayer(editorWindow));
            this.Layers.Add(new GraphMessageBoxLayer(editorWindow));
        }

        public void OnGUI(Rect rect)
        {
            switch (Event.current.type)
            {
                case EventType.Repaint:
                    Repaint(rect);
                    break;

                case EventType.Layout:
                    break;

                default:
                    ProcessEvents(rect);
                    break;
            }
        }

        /// <summary>
        /// Draws the graph
        /// </summary>
        /// <param name="rect"></param>
        public void Repaint(Rect rect)
        {
            EditorGUI.DrawRect(rect, GraphColors.BackgroundColor);

            if (this.Context.IsStateMachineLoaded)
            {
                for (int i = 0; i < this.Layers.Count; i++)
                {
                    this.Layers[i].Draw(rect);
                }
            }

            if (this.Context.IsPrefabAsset)
            {
                EditorGUI.DrawRect(rect, new Color(0, 0, 0, 0.2f));
            }
        }

        /// <summary>
        /// Processes input events on the graph
        /// </summary>
        /// <param name="rect"></param>
        public void ProcessEvents(Rect rect)
        {
            if (this.Context.IsStateMachineLoaded)
            {
                for (int i = this.Layers.Count - 1; i >= 0; i--)
                {
                    this.Layers[i].ProcessEvents(rect, Event.current.mousePosition);
                }
            }
        }
    }
}