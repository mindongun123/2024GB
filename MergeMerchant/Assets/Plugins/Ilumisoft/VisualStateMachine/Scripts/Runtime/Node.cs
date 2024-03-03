namespace Ilumisoft.VisualStateMachine
{
    using UnityEngine;
    using UnityEngine.Serialization;

    [System.Serializable]
    public class Node
    {
        private static readonly int Width = 120;
        private static readonly int Height = 60;

        [SerializeField] private Vector2 position;

        [SerializeField, FormerlySerializedAs("name")] 
        private string id = string.Empty;

        /// <summary>
        /// Gets or sets the name of the node
        /// </summary>
        public string ID
        {
            get => this.id;
            set => this.id = value;
        }
        
        /// <summary>
        /// Gets or sets the rect of the node.
        /// Remark: The setter will only apply the center of the position
        /// </summary>
        public Rect Rect
        {
            get => new Rect()
                {
                    x = Position.x - Width / 2.0f,
                    y = Position.y - Height / 2.0f,
                    width = Width,
                    height = Height
                };

            set => Position = value.center;
        }

        /// <summary>
        /// Gets or sets the position (center) of the node in the graph
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }
    }
}