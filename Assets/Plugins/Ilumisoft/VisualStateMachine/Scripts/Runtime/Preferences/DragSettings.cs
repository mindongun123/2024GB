namespace Ilumisoft.VisualStateMachine
{
    using UnityEngine;

    [System.Serializable]
    public class DragSettings
    {
        [SerializeField]
        private Vector2 dragOffset = Vector2.zero;

        /// <summary>
        /// Gets or sets the Drag Offset of the Graph
        /// </summary>
        public Vector2 DragOffset
        {
            get => this.dragOffset;
            set => this.dragOffset = value;
        }
    }
}
