namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    public class StateContextMenu : IContextMenu
    {
        private readonly Context context;
        private readonly State state;

        public StateContextMenu(Context context, State state)
        {
            this.context = context;

            this.state = state;
        }

        public void Show()
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Make Transition"), false, () =>
            {
                this.context.TransitionPreview = this.state;
            });
            genericMenu.AddSeparator("");
            genericMenu.AddItem(new GUIContent("Set as Entry"), false, () =>
            {
                Undo.RegisterCompleteObjectUndo(this.context.StateMachine, "Set entry state");
                this.context.Graph.EntryStateID = this.state.ID;
            });
            genericMenu.AddSeparator("");
            genericMenu.AddItem(new GUIContent("Delete"), false, () =>
            {
                this.context.SelectedNodes.Clear();
                this.context.StateMachine.DeleteNode(this.state);
            });

            genericMenu.ShowAsContext();
        }
    }
}