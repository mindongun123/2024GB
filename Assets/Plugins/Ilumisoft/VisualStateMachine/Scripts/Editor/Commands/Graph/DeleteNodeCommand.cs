namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using System.Linq;
    using UnityEditor;

    public class DeleteNodeCommand : ICommand
    {
        readonly StateMachine stateMachine;
        readonly Node node;

        public DeleteNodeCommand(StateMachine stateMachine, Node node)
        {
            this.stateMachine = stateMachine;
            this.node = node;
        }

        public void Execute()
        {
            Undo.RegisterCompleteObjectUndo(stateMachine, "Remove node");

            Selection.activeObject = null;

            var graph = stateMachine.GetStateMachineGraph();

            //Find all transitions with the given node as origin or target
            var transitions = graph.Transitions.Where(t => t.OriginID == node.ID || t.TargetID == node.ID).ToList();

            //Remove them
            foreach (var transition in transitions)
            {
                graph.Transitions.Remove(transition);
            }

            //Remove the node
            graph.TryRemoveNode(node);

            //If the node was entry state, reset it
            if (graph.EntryStateID == node.ID)
            {
                graph.EntryStateID = string.Empty;
            }
        }
    }
}