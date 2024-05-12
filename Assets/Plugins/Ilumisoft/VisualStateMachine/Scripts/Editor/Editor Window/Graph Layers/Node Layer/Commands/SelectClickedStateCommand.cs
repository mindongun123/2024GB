namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;

    public class SelectClickedStateCommand : ICommand
    {
        private readonly Context context;
        private readonly Node node;

        public SelectClickedStateCommand(Context context, Node node)
        {
            this.context = context;
            this.node = node;
        }

        public void Execute()
        {
            if (this.context.SelectedNodes.Contains(this.node) == false)
            {
                this.context.SelectedNodes.Clear();
                this.context.SelectedNodes.Add(this.node);
            }

            Event.current.Use();
        }
    }
}