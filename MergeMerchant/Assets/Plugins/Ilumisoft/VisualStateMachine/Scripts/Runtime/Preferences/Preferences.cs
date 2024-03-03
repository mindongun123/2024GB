namespace Ilumisoft.VisualStateMachine
{
    using UnityEngine;

    [System.Serializable]
    public class Preferences
    {
        [SerializeField]
        private ZoomSettings zoomSettings = new ZoomSettings();

        [SerializeField]
        private DragSettings dragSettings = new DragSettings();

        [SerializeField]
        private GridSettings gridSettings = new GridSettings();

        [SerializeField]
        private LabelSettings labelSettings = new LabelSettings();

        public ZoomSettings ZoomSettings { get => this.zoomSettings; set => this.zoomSettings = value; }
        public DragSettings DragSettings { get => this.dragSettings; set => this.dragSettings = value; }
        public GridSettings GridSettings { get => this.gridSettings; set => this.gridSettings = value; }
        public LabelSettings LabelSettings { get => this.labelSettings; set => this.labelSettings = value; }
    }
}
