namespace Ilumisoft.VisualStateMachine.Editor
{
    public class GraphSelection
    {
        private readonly Context context;

        public GraphSelection(Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Duplicates the current selection
        /// </summary>
        public void Duplicate()
        {
            ICommand command = new DuplicateSelectionCommand(this.context.Graph, this.context.SelectedNodes);

            command.Execute();
        }

        /// <summary>
        /// Deletes the current selection
        /// </summary>
        public void Delete()
        {
            ICommand command = new DeleteSelectionCommand(this.context.StateMachine, this.context.SelectedNodes);

            command.Execute();
        }
    }
}