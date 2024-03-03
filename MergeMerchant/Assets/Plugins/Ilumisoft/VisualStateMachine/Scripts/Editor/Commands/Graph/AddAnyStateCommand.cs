namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    public class AddAnyStateCommand : ICommand
    {
        private readonly StateMachine stateMachine;
        private readonly Vector2 position;

        public AddAnyStateCommand(StateMachine stateMachine, Vector2 position)
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

            var anyState = new AnyState()
            {
                Rect = rect,
                ID = graph.GetUniqueAnyStateName()
            };

            Undo.RecordObject(this.stateMachine, "Added anyState");

            graph.TryAddNode(anyState);
        }
    }
}