namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;

    public class EditorWindowGUI
    {
        [System.NonSerialized] private readonly EditorWindow editorWindow;

        /// <summary>
        /// The Toolbar at the top of the window
        /// </summary>
        private Toolbar Toolbar { get; set; }

        /// <summary>
        /// The StateMachine graph
        /// </summary>
        private GraphView Graph { get; set; }

        public EditorWindowGUI(EditorWindow editorWindow)
        {
            this.editorWindow = editorWindow;

            this.Graph = new GraphView(this.editorWindow);

            this.Toolbar = new Toolbar(this.editorWindow);
        }
        
        /// <summary>
        /// Draws the Graph and processes Input
        /// </summary>
        public void OnGUI()
        {
            var rect = editorWindow.Rect;

            if (Event.current.type == EventType.Repaint)
            {
                this.Graph.Repaint(rect);
            }

            this.Toolbar.OnGUI(editorWindow.Rect);

            if (Event.current.isMouse || 
                Event.current.isKey || 
                Event.current.isScrollWheel || 
                Event.current.rawType == EventType.MouseUp)
            {
                this.Graph.ProcessEvents(editorWindow.Rect);
            }

            if (GUI.changed)
            {
                editorWindow.Repaint();
            }
        }
    }
}