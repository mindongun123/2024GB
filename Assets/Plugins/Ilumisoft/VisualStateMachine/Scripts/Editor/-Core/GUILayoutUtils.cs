namespace Ilumisoft.VisualStateMachine.Editor
{
    using System;
    using UnityEditor;
    using UnityEngine;
    
    public static class GUILayoutUtils
    {
        public static void VerticalSpace(float pixels)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(pixels);
            GUILayout.EndVertical();
        }

        public static void Ident(Action action)
        {
            EditorGUI.indentLevel++;

            action();

            EditorGUI.indentLevel--;
        }

        public static void HorizontalGroup(Action action)
        {
            GUILayout.BeginHorizontal();

            action();

            GUILayout.EndHorizontal();
        }
    }
}