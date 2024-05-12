namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;

    public class DeleteTransitionCommand : ICommand
    {
        private readonly StateMachine stateMachine;
        private readonly Transition transition;

        public DeleteTransitionCommand(StateMachine stateMachine, Transition transition)
        {
            this.stateMachine = stateMachine;
            this.transition = transition;
        }

        public void Execute()
        {
            var graph = stateMachine.GetStateMachineGraph();

            Undo.RegisterCompleteObjectUndo(this.stateMachine, "Remove transition");

            graph.TryRemoveTransition(this.transition);
        }
    }
}