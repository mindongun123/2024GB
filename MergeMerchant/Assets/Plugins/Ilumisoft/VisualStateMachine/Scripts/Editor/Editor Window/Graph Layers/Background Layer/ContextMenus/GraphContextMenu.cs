namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    public class GraphContextMenu : IContextMenu
    {
        private readonly EditorWindow view;
        private readonly Vector2 mousePosition;

        public GraphContextMenu(EditorWindow view, Vector2 mousePosition)
        {
            this.view = view;
            this.mousePosition = mousePosition;
        }

        public void Show()
        {
            Context context = view.Context;

            Vector2 nodePosition = GetNodePosition(mousePosition - context.DragOffset);

            context.SelectedNodes.Clear();

            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Create State"), false, () =>
            {
                CreateState(nodePosition);
            });
            genericMenu.AddItem(new GUIContent("Create Any State"), false, () =>
            {
                CreateAnyState(nodePosition);
            });
            genericMenu.ShowAsContext();
        }

        private void CreateAnyState(Vector2 position)
        {
            view.Context.StateMachine.AddAnyState(position);
        }

        private void CreateState(Vector2 position)
        {
            view.Context.StateMachine.AddState(position);
        }

        private Vector2 GetNodePosition(Vector2 position)
        {
            EditorWindow window = this.view;

            //Compute the distance vector to the vanishing point (multplied by 1-zoomFactor)
            Vector2 distance = (position + window.Context.DragOffset - new Vector2(window.Rect.width, window.Rect.height) / 2) * (1 - window.Context.ZoomFactor);

            return new Vector2()
            {
                x = position.x + 2 * distance.x,
                y = position.y + 2 * distance.y
            };
        }
    }
}