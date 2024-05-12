namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// The State Machine Editor Window used to edit a StateMachine Monobhevaiour
    /// </summary>
    public class EditorWindow : UnityEditor.EditorWindow
    {
        /// <summary>
        /// Data container of the window
        /// </summary>
        public Context Context { get; private set; } = new Context();

        /// <summary>
        /// The GUI of the editor window
        /// </summary>
        [System.NonSerialized] private EditorWindowGUI editorWindowGUI;

        /// <summary>
        /// Returns true if the window has been enabled
        /// </summary>
        private bool IsEnabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the rect of the view in its parent
        /// </summary>
        public Rect Rect
        {
            get { return new Rect(0, 0, this.position.width, this.position.height); }
        }

        /// <summary>
        /// Initializes the editor window
        /// </summary>
        private void OnEnable()
        {
            this.wantsMouseMove = true;

            this.editorWindowGUI = new EditorWindowGUI(this);

            Context.Reload();

            IsEnabled = true;
        }

        /// <summary>
        /// Repaint the window regularly when Unity is in Playmode
        /// </summary>
        private void Update()
        {
            if (EditorApplication.isPlaying)
            {
                Repaint();
            }
        }

        /// <summary>
        /// Repaint the window on inspector updates
        /// </summary>
        private void OnInspectorUpdate()
        {
            Repaint();
        }

        /// <summary>
        /// Reloads the context if the hierarchy changed 
        /// </summary>
        private void OnHierarchyChange()
        {
            Context.Reload();
        }

        /// <summary>
        /// Updates the context if a state machine is selected
        /// </summary>
        private void OnSelectionChange()
        {
            if(IsEnabled)
            {
                Context.UpdateSelection();
            }
        }

        /// <summary>
        /// Reloads the context if the editor window is enabled and gets focused
        /// </summary>
        private void OnFocus()
        {
            if(IsEnabled)
            {
                Context.Reload();
            }
        }

        /// <summary>
        /// Draws the Graph and processes Input
        /// </summary>
        private void OnGUI()
        {
            this.editorWindowGUI.OnGUI();
        }
    }
}