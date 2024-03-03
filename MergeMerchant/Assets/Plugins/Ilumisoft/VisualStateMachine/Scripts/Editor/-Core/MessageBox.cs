namespace Ilumisoft.VisualStateMachine.Editor
{
    using System;
    using UnityEditor;
    using UnityEngine;

    public class MessageBox
    {
        public Rect Rect { get; set; }

        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public Func<bool> IsEnabled;

        public void Draw(Rect rect)
        {
            GUI.BeginGroup(rect);
            EditorGUI.HelpBox(Rect, Message, MessageType);
            GUI.EndGroup();
        }
    }
}