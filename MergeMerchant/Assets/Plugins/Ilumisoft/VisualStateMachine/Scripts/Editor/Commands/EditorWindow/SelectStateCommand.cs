namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;

    public class SelectStateCommand : ICommand
    {
        private readonly EditorWindow editorWindow;
        private readonly State state;

        public SelectStateCommand(EditorWindow editorWindow, State state)
        {
            this.editorWindow = editorWindow;
            this.state = state;
        }

        public void Execute()
        {
            this.editorWindow.Context.SelectedNodes.Clear();
            this.editorWindow.Context.SelectedNodes.Add(this.state);

            var rect = this.editorWindow.Rect;

            var dragOffset = new Vector2()
            {
                x = rect.center.x - this.state.Rect.center.x,
                y = rect.center.y - this.state.Rect.center.y
            };

            this.editorWindow.Context.DragOffset = dragOffset;

            StateInspectorHelper.Instance.Inspect(this.editorWindow.Context.StateMachine, this.editorWindow.Context.Graph, this.state);
        }
    }
}