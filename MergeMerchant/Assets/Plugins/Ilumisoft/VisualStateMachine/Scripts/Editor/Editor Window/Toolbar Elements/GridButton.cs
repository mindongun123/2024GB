namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEditor;
    using UnityEngine;

    public class GridButton : ToolbarElement
    {
        public GridButton(EditorWindow window) : base(window)
        {
            this.Width = 80;
        }

        public override void OnGUI(Rect rect)
        {
            EditorGUI.BeginDisabledGroup(!Context.IsStateMachineLoaded);
            {
                rect = new Rect(rect.width - Width, rect.y, Width, rect.height);

                if (GUI.Button(rect, Context.IsGridEnabled ? "Grid On" : "Grid Off", EditorStyles.toolbarButton))
                {
                    Context.IsGridEnabled = !Context.IsGridEnabled;
                }
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}