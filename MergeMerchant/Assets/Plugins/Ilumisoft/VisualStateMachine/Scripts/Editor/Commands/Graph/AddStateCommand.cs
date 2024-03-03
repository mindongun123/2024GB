namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    public class AddStateCommand : ICommand
    {
        private readonly StateMachine stateMachine;
        private readonly Vector2 position;

        public AddStateCommand(StateMachine stateMachine, Vector2 position)
        {
            this.stateMachine = stateMachine;
            this.position = position;
        }

        public void Execute()
        {
            var graph = stateMachine.GetStateMachineGraph();

            //Create the rect of the state (for the graph)
            var rect = new Rect(0, 0, 120, 60);
            rect.x = this.position.x - rect.width / 2;
            rect.y = this.position.y - rect.height / 2;

            var node = new State
            {
                Rect = rect,
                ID = graph.GetUniqueStateName()
            };

            Undo.RegisterCompleteObjectUndo(this.stateMachine, "Added node");

            if (graph.TryAddNode(node))
            {
                if (graph.Nodes.Count == 1)
                {
                    graph.EntryStateID = node.ID;
                }
            }
        }
    }
}