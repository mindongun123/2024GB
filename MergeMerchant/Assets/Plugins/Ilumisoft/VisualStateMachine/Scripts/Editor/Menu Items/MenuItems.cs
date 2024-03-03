namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEditor;
    using UnityEngine;

    public static class MenuItems
    {
        /// <summary>
        /// Adds a menu item to the window toolbar field of unity
        /// </summary>
        [MenuItem("Window/Visual State Machine/State Machine Graph")]
        private static void OpenStateMachineGraphWindow()
        {
            EditorWindow window = (EditorWindow)UnityEditor.EditorWindow.GetWindow(typeof(EditorWindow),false, "State Machine Graph", true);
            window.Show();
        }

        /// <summary>
        /// Adds a menu item to create a state machine game object
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/Ilumisoft/State Machine", false, 10)]
        private static void CreateStateMachine(MenuCommand menuCommand)
        {
            GameObject gameObject = new GameObject("State Machine");

            GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);

            gameObject.AddComponent<StateMachine>();

            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
            Selection.activeObject = gameObject;
        }
    }
}