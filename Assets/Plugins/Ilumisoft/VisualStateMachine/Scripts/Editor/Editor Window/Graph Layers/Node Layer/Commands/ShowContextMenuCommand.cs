namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;

    public class ShowContextMenuCommand : ICommand
    {
        private readonly Context context;
        private readonly Node node;

        public ShowContextMenuCommand(Context context, Node node)
        {
            this.context = context;
            this.node = node;
        }

        public void Execute()
        {
            IContextMenu contextMenu = null;

            if (this.context.SelectedNodes.Count > 1)
            {
                contextMenu = new SelectionContextMenu(context.GraphSelection);
            }
            else
            {
                this.context.SelectedNodes.Clear();

                if (node is State state)
                {
                    contextMenu = new StateContextMenu(context, state);
                }
                else if (node is AnyState anyState)
                {
                    contextMenu = new AnyStateContextMenu(context, anyState);
                }
            }

            //Show the context menu if it is defined
            contextMenu?.Show();

            Event.current.Use();
        }
    }
}