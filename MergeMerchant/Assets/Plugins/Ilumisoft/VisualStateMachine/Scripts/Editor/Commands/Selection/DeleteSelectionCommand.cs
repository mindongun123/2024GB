namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using System.Collections.Generic;

    public class DeleteSelectionCommand : ICommand
    {
        private readonly StateMachine stateMachine;
        private readonly List<Node> nodes;

        public DeleteSelectionCommand(StateMachine stateMachine, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.nodes = nodes;
        }

        public void Execute()
        {
            for (int i = 0; i < this.nodes.Count; i++)
            {
                this.stateMachine.DeleteNode(this.nodes[i]);

                this.nodes.RemoveAt(i);
                i--;
            }
        }
    }
}