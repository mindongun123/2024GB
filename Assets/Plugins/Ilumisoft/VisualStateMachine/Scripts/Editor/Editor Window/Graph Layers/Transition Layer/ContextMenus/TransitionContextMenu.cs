namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    public class TransitionContextMenu : IContextMenu
    {
        private readonly StateMachine stateMachine;

        private readonly Transition transition;

        public TransitionContextMenu(StateMachine stateMachine, Transition transition)
        {
            this.stateMachine = stateMachine;

            this.transition = transition;
        }

        public void Show()
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Delete"), false, () =>
            {
                this.stateMachine.DeleteTransition(transition);
            });

            genericMenu.ShowAsContext();
        }
    }
}