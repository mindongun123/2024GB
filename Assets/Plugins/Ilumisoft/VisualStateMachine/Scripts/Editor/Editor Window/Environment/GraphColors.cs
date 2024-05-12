namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;

    public static class GraphColors
    {
        public static Color BackgroundColor { get; } = new Color(42, 42, 42, 255) / 255;

        public static Color SelectionRectColor { get; } = new Color(100, 200, 255, 32) / 255;

        public static Color InnerGridColor { get; } = new Color(0.12f, 0.12f, 0.12f);

        public static Color OuterGridColor { get; } = new Color(0.14f, 0.14f, 0.14f);

        public static Color SelectionColor { get; } = new Color(100, 200, 255, 255) / 255;
    }
}