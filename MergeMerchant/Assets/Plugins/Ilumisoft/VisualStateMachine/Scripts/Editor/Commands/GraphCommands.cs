namespace Ilumisoft.VisualStateMachine.Editor.Extensions
{
    using UnityEngine;

    public static class GraphCommands
    {
        /// <summary>
        /// Adds a new State with the given position to the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="position">The position of the state</param>
        /// <returns></returns>
        public static void AddState(this StateMachine stateMachine, Vector2 position)
        {
            ICommand command = new AddStateCommand(stateMachine, position);

            command.Execute();
        }

        /// <summary>
        /// Adds a new Any State with the given position to the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static void AddAnyState(this StateMachine stateMachine, Vector2 position)
        {
            ICommand command = new AddAnyStateCommand(stateMachine, position);

            command.Execute();
        }

        /// <summary>
        /// Removes the given node from the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        public static void DeleteNode(this StateMachine stateMachine, Node node)
        {
            ICommand command = new DeleteNodeCommand(stateMachine, node);

            command.Execute();
        }

        /// <summary>
        /// Adds a new transition with the given origin and target node to the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        public static void AddTransition(this StateMachine stateMachine, Node origin, State target)
        {
            ICommand command = new AddTransitionCommand(stateMachine, origin, target);

            command.Execute();
        }

        /// <summary>
        /// Removes the given transition from the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="transition"></param>
        public static void DeleteTransition(this StateMachine stateMachine, Transition transition)
        {
            ICommand command = new DeleteTransitionCommand(stateMachine, transition);

            command.Execute();
        }
    }
}