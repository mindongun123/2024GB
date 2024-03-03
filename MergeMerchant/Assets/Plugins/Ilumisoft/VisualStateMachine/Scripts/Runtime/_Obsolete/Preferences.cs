namespace Ilumisoft.VisualStateMachine.Obsolete
{
    using UnityEngine;

    [AddComponentMenu(""), ExecuteInEditMode()]
    public class Preferences : MonoBehaviour
    {
        [SerializeField]
        private ZoomSettings zoomSettings = new ZoomSettings();

        [SerializeField]
        private DragSettings dragSettings = new DragSettings();

        [SerializeField]
        private GridSettings gridSettings = new GridSettings();

        [SerializeField]
        private LabelSettings labelSettings = new LabelSettings();

        private void Start()
        {
            ExportDataToStateMachine();
        }

        void ExportDataToStateMachine()
        {
            //Nothing to do on runtime, since Preferences only need to be applied in the editor! 
#if Unity_Editor
            if (Application.isPlaying == false && UnityEditor.PrefabUtility.IsPartOfAnyPrefab(this) == false)
            {
                StateMachine stateMachine = GetComponentInParent<StateMachine>();

                if (stateMachine != null)
                {
                    stateMachine.Graph.Import(this);
                    DestroyImmediate(this.gameObject);
                }
            }
#endif
        }
    }
}
