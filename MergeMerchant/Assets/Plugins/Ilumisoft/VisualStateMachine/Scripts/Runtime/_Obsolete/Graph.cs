namespace Ilumisoft.VisualStateMachine.Obsolete
{
    using System.Collections.Generic;
    using UnityEngine;

    [AddComponentMenu(""), DisallowMultipleComponent, ExecuteInEditMode]
    public class Graph : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] private string entryState = string.Empty;

        [SerializeField] private List<State> states = new List<State>();

        [SerializeField] private List<AnyState> anyStates = new List<AnyState>();

        [SerializeField] private List<Transition> transitions = new List<Transition>();

        public string EntryState
        {
            get => this.entryState;
            set => this.entryState = value;
        }

        public IList<Node> Nodes { get; } = new List<Node>();

        public IList<Transition> Transitions => transitions;

        protected virtual void OnEnable()
        {
            gameObject.hideFlags = HideFlags.None;
        }

        private void Start()
        {
            if (Application.isPlaying)
            {
                ExportDataToStateMachineRuntime();
            }
            else
            {
                ExportDataToStateMachine();
            }
        }

        void ExportDataToStateMachineRuntime()
        {
            var stateMachine = GetComponentInParent<StateMachine>();

            stateMachine.Graph.Import(this);
        }

        public void ExportDataToStateMachine()
        {
#if UNITY_EDITOR
            var stateMachine = GetComponentInParent<StateMachine>();

            stateMachine.Graph.Import(this);

            Debug.Log("Imported graph", stateMachine);

            UnityEditor.EditorUtility.SetDirty(stateMachine);
                
            //Not part of any prefab? Destroy the obsolete graph data
            if (UnityEditor.PrefabUtility.IsPartOfAnyPrefab(this) == false)
            {
                DestroyImmediate(this.gameObject);
            }
            //Otherwise get the prefab and update it
            else
            {
                // Get the Prefab Asset root GameObject and its asset path.
                GameObject assetRoot = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(this.gameObject);
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(assetRoot);

                // Load the contents of the Prefab Asset.
                GameObject contentsRoot = UnityEditor.PrefabUtility.LoadPrefabContents(assetPath);

                Graph[] graphs = contentsRoot.GetComponentsInChildren<Graph>();

                foreach (Graph graph in graphs)
                {
                    graph.ExportDataToStateMachine();
                }

                // Save contents back to Prefab Asset and unload contents.
                UnityEditor.PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
                UnityEditor.PrefabUtility.UnloadPrefabContents(contentsRoot);

                stateMachine.Graph.Import(this);
            }
#endif
        }

        public void OnBeforeSerialize()
        {
            SerializeNodes();
        }

        public void OnAfterDeserialize()
        {
            DeserializeNodes();
        }

        private void SerializeNodes()
        {
            states.Clear();
            anyStates.Clear();

            foreach (var node in Nodes)
            {
                if (node is State state)
                {
                    states.Add(state);
                }
                else if (node is AnyState anyState)
                {
                    anyStates.Add(anyState);
                }
            }
        }

        private void DeserializeNodes()
        {
            Nodes.Clear();

            DeserializeStates();

            DeserializeAnyStates();
        }

        private void DeserializeStates()
        {
            foreach (var node in states)
            {
                Nodes.Add(node);
            }
        }

        private void DeserializeAnyStates()
        {
            foreach (var node in anyStates)
            {
                Nodes.Add(node);
            }
        }
    }
}
