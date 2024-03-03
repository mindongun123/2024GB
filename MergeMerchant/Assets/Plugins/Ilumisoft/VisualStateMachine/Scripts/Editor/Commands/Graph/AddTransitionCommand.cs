namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using System.Linq;
    using UnityEditor;

    public class AddTransitionCommand : ICommand
    {
        private readonly StateMachine stateMachine;
        private readonly Node origin;
        private readonly State target;

        public AddTransitionCommand(StateMachine stateMachine, Node origin, State target)
        {
            this.stateMachine = stateMachine;
            this.origin = origin;
            this.target = target;
        }

        public void Execute()
        {
            var graph = stateMachine.GetStateMachineGraph();

            //Cancel if any transition with the same origin and target already exist
            if (graph.Transitions.Any(t => t.OriginID == this.origin.ID && t.TargetID == this.target.ID))
            {
                return;
            }

            var transition = new Transition()
            {
                ID = graph.GetUniqueTransitionID(),
                OriginID = this.origin.ID,
                TargetID = this.target.ID,
            };

            Undo.RegisterCompleteObjectUndo(this.stateMachine, "Add transition");

            graph.TryAddTransition(transition);
        }
    }
}