namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEditor;
    using UnityEngine;

    public class SelectionContextMenu : IContextMenu
    {
        private readonly GraphSelection graphSelection;

        public SelectionContextMenu(GraphSelection graphSelection)
        {
            this.graphSelection = graphSelection;
        }

        public void Show()
        {
            var genericMenu = new GenericMenu();

            genericMenu.AddItem(new GUIContent("Duplicate"), false, () =>
            {
                this.graphSelection.Duplicate();
            });

            genericMenu.AddItem(new GUIContent("Delete"), false, () =>
            {
                this.graphSelection.Delete();
            });

            genericMenu.ShowAsContext();
        }
    }
}