namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEditor;
    using UnityEngine;

    public class LabelButton : ToolbarElement
    {
        public LabelButton(EditorWindow window) : base(window)
        {
            this.Width = 80;
        }

        public override void OnGUI(Rect rect)
        {
            EditorGUI.BeginDisabledGroup((Context.IsStateMachineLoaded == false));
            {
                var buttonRect = new Rect(rect.width - 80 - this.Width, rect.y, this.Width, rect.height);

                if (GUI.Button(buttonRect, Context.ShowLabels ? "Labels On" : "Labels Off", EditorStyles.toolbarButton))
                {
                    Context.ShowLabels = !Context.ShowLabels;
                }
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}