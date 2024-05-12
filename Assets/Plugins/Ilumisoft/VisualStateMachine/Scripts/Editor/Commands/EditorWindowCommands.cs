namespace Ilumisoft.VisualStateMachine.Editor
{
    public static class EditorWindowCommands
    {
        public static EditorWindow OpenStateMachineGraph(StateMachine stateMachine)
        {
            EditorWindow window = (EditorWindow)UnityEditor.EditorWindow.GetWindow(typeof(EditorWindow), false, "State Machine Graph", true);

            window.EditStateMachine(stateMachine);

            return window;
        }

        private static void EditStateMachine(this EditorWindow editorWindow, StateMachine stateMachine)
        {
            editorWindow.Context.LoadStateMachine(stateMachine);
        }

        public static void SelectState(this EditorWindow editorWindow, State state)
        {
            ICommand command = new SelectStateCommand(editorWindow, state);

            command.Execute();
        }

        public static void SelectTransition(this EditorWindow editorWindow, Transition transition)
        {
            ICommand command = new SelectTransitionCommand(editorWindow, transition);

            command.Execute();
        }
    }
}