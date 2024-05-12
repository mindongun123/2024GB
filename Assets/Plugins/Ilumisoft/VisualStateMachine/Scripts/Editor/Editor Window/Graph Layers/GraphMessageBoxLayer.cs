namespace Ilumisoft.VisualStateMachine.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class GraphMessageBoxLayer : GraphLayer
    {
        private const int MessageBoxHeight = 40;
        private const int MessageBoxWidth = 200;
        private const int Offset = 4;

        private readonly List<MessageBox> messages = new List<MessageBox>();
        private MessageBox missingEntryStateBox;
        private MessageBox prefabInstanceBox;

        public GraphMessageBoxLayer(EditorWindow editorWindow) : base(editorWindow)
        {
            messages.Add(new MessageBox()
            {
                Rect = new Rect(0, 0, MessageBoxWidth, MessageBoxHeight),
                Message = EditorHelpMessages.GraphIsPrefabInstance,
                MessageType = MessageType.Warning,
                IsEnabled = IsPrefabInstance
            });

            messages.Add(new MessageBox()
            {
                Rect = new Rect(0, 0, MessageBoxWidth, MessageBoxHeight),
                Message = EditorHelpMessages.MissingEntryState,
                MessageType = MessageType.Warning,
                IsEnabled = IsEntryStateMissing
            });
        }

        public override void Draw(Rect rect)
        {
            if (this.Context.IsStateMachineLoaded == false)
            {
                return;
            }

            rect = new Rect(Offset, rect.yMax - MessageBoxHeight - Offset, MessageBoxWidth, MessageBoxHeight);

            foreach (var messageBox in this.messages)
            {
                if (messageBox.IsEnabled())
                {
                    messageBox.Draw(rect);

                    rect.y -= messageBox.Rect.height + Offset;
                }
            }
        }

        private bool IsEntryStateMissing()
        {
            return this.Context.Graph.HasNode(this.Context.Graph.EntryStateID) == false;
        }

        private bool IsPrefabInstance()
        {
            return this.Context.IsPrefabAsset;
        }
    }
}