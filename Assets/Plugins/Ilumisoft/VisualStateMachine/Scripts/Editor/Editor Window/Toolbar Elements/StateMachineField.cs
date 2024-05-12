namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEditor;
    using UnityEngine;

    public class StateMachineField : ToolbarElement
    {
        public StateMachineField(EditorWindow window) : base(window)
        {
            this.Width = 248;
        }

        public override void OnGUI(Rect rect)
        {
            var fieldRect = new Rect(2, rect.y + 1, this.Width, 16);

            EditorGUI.BeginChangeCheck();
            {
                StateMachine stateMachine = (StateMachine)EditorGUI.ObjectField(fieldRect, Context.StateMachine, typeof(StateMachine), true);

                if (EditorGUI.EndChangeCheck())
                {
                    Context.LoadStateMachine(stateMachine);
                }
            }
        }
    }
}