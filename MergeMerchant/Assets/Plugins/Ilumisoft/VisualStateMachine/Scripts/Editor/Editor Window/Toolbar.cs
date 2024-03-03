namespace Ilumisoft.VisualStateMachine.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class Toolbar
    {
        private readonly List<ToolbarElement> elements;

        /// <summary>
        /// Creates a new layer for the Toolbar
        /// </summary>
        /// <param name="editorWindow"></param>
        public Toolbar(EditorWindow editorWindow)
        {
            this.elements = new List<ToolbarElement>
            {
                new GridButton(editorWindow),
                new LabelButton(editorWindow),
                new ZoomSlider(editorWindow),
                new StateMachineField(editorWindow)
            };
        }

        /// <summary>
        /// Draws the toolbar
        /// </summary>
        /// <param name="rect"></param>
        public void OnGUI(Rect rect)
        {
            if (!Event.current.isKey)
            {
                GUI.BeginGroup(new Rect(0, 0, rect.width, EditorStyles.toolbar.fixedHeight), EditorStyles.toolbar);
                {
                    Rect toolbarRect = new Rect(0, 0, rect.width, EditorStyles.toolbar.fixedHeight);

                    foreach (var element in this.elements)
                    {
                        element.OnGUI(toolbarRect);
                    }
                }
                GUI.EndGroup();
            }
        }
    }
}