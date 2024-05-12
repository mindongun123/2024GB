namespace Ilumisoft.VisualStateMachine.Editor
{
    public class SelectTransitionCommand : ICommand
    {
        private readonly EditorWindow editorWindow;
        private readonly Transition transition;

        public SelectTransitionCommand(EditorWindow editorWindow, Transition transition)
        {
            this.editorWindow = editorWindow;
            this.transition = transition;
        }

        public void Execute()
        {
            this.editorWindow.Context.SelectedNodes.Clear();

            TransitionInspectorHelper.Instance.Inspect(this.editorWindow.Context.StateMachine, this.transition);
        }
    }
}