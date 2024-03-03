namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    public class AnyStateContextMenu : IContextMenu
    {
        private readonly Context context;
        private readonly AnyState anyState;

        public AnyStateContextMenu(Context context, AnyState anyState)
        {
            this.context = context;

            this.anyState = anyState;
        }

        public void Show()
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Make Transition"), false, () =>
            {
                context.TransitionPreview = anyState;
            });
            genericMenu.AddSeparator("");
            genericMenu.AddItem(new GUIContent("Delete"), false, () =>
            {
                context.SelectedNodes.Clear();
                context.StateMachine.DeleteNode(anyState);
            });

            genericMenu.ShowAsContext();
        }
    }
}