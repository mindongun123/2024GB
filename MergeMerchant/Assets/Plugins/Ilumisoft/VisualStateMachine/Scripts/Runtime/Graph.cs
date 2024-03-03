namespace Ilumisoft.VisualStateMachine
{
    using Ilumisoft.VisualStateMachine.Internal;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Graph : ISerializationCallbackReceiver
    {
        /// <summary>
        /// Serialized list containing all states of the graph
        /// </summary>
        [SerializeField] 
        private List<State> states = new List<State>();

        /// <summary>
        /// Serialized list containing all any state nodes of the graph
        /// </summary>
        [SerializeField] 
        private List<AnyState> anyStates = new List<AnyState>();

        /// <summary>
        /// Serialized list containing all transitions of the graph
        /// </summary>
        [SerializeField] 
        private List<Transition> transitions = new List<Transition>();

        /// <summary>
        /// Serialized preferences containing all the graph preferences like the zoom factor, etc.
        /// </summary>
        [SerializeField] 
        private Preferences preferences = new Preferences();

        /// <summary>
        /// The id of the entry state
        /// </summary>
        [SerializeField] 
        private string entryState = string.Empty;

        /// <summary>
        /// The graph cache caches all serialized data and allows to access them in an efficient way
        /// </summary>
        [NonSerialized]
        private GraphCache cache = new GraphCache();

        /// <summary>
        /// Wrapper class allowing to access the serialized fields of a graph. 
        /// This is mainly used by the graph cache.
        /// </summary>
        public struct SerializedData
        {
            public SerializedData(Graph graph)
            {
                this.States = graph.states;
                this.AnyStates = graph.anyStates;
                this.Transitions = graph.transitions;
            }

            public List<State> States { get; private set; }

            public List<AnyState> AnyStates { get; private set; }

            public List<Transition> Transitions { get; private set; }
        }

        /// <summary>
        /// Gets or sets the id of the entry state
        /// </summary>
        public string EntryStateID
        {
            get => this.entryState;
            set => this.entryState = value;
        }

        /// <summary>
        /// Gets all transitions
        /// </summary>
        public IList<Transition> Transitions => cache.Transitions;

        /// <summary>
        /// Gets all nodes
        /// </summary>
        public IList<Node> Nodes => cache.Nodes;

        /// <summary>
        /// Gets the cache
        /// </summary>
        public GraphCache Cache => cache;

        /// <summary>
        /// Gets the preferences
        /// </summary>
        public Preferences Preferences => preferences;

        /// <summary>
        /// This is called just before the data is serialized. Do not call this method on your own!
        /// </summary>
        public void OnBeforeSerialize()
        {
            cache.SerializeCache(this);
        }

        /// <summary>
        /// This is called just after the data has been deserialized. Do not call this method on your own!
        /// </summary>
        public void OnAfterDeserialize()
        {
            cache.BuildCache(this);
        }

        /// <summary>
        /// Tries to get the transition with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transition"></param>
        /// <returns></returns>
        public bool TryGetTransition(string id, out Transition transition) => cache.TransitionDictionary.TryGetValue(id, out transition);

        /// <summary>
        /// Gets the transition with the given id. Returns null if it does not exist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Transition GetTransition(string id) => cache.GetTransition(id);

       
        /// <summary>
        /// Returns the node with the given id or null if it does bot exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Node GetNode(string id) => cache.GetNode(id);

        /// <summary>
        /// Returns the state with the given id or null if it doesbot exist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public State GetState(string id) => cache.GetState(id);

        /// <summary>
        /// Tries to get the node with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool TryGetNode(string id, out Node node) => cache.TryGetNode(id, out node);


        /// <summary>
        /// Tries to get the state with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool TryGetState(string id, out State state) => cache.TryGetState(id, out state);

        /// <summary>
        /// Tries to add the given node to the graph and returns true on success.
        /// Returns false if anther node with he given id already exists.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool TryAddNode(Node node) => cache.TryAddNode(node);

        /// <summary>
        /// Tries to remove the node with given id and returns true on success.
        /// Returns false if no node with the given id exists. 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool TryRemoveNode(Node node) => cache.TryRemoveNode(node);

        /// <summary>
        /// Tries to add the given transition and returns true on success.
        /// Returns false if another transition with the same id already exists.
        /// </summary>
        /// <param name="transition"></param>
        /// <returns></returns>
        public bool TryAddTransition(Transition transition) => cache.TryAddTransition(transition);

        /// <summary>
        /// Tries to remove the transition with given id and returns true on success.
        /// Returns false if no transition with the given id exists.
        /// </summary>
        /// <param name="transition"></param>
        /// <returns></returns>
        public bool TryRemoveTransition(Transition transition) => cache.TryRemoveTransition(transition);

        /// <summary>
        /// return true if the graph contains a transition with the given id, false otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasTransition(string id) => cache.HasTransition(id);

        /// <summary>
        /// Returns true if thegraph contains node with the given id, false otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasNode(string id) => cache.HasNode(id);

        /// <summary>
        /// Returns true if the graph contains a state with the given id, false otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasState(string id) => cache.HasState(id);

        /// <summary>
        /// Imports the graph data from the given obsolete graph container
        /// </summary>
        /// <param name="container"></param>
        public void Import(Obsolete.Graph container)
        {
            var data = JsonUtility.ToJson(container);

            JsonUtility.FromJsonOverwrite(data, this);
        }

        /// <summary>
        /// Imports the preferences data from the given obsolete preferences container
        /// </summary>
        /// <param name="preferences"></param>
        public void Import(Obsolete.Preferences preferences)
        {
            var data = JsonUtility.ToJson(preferences);

            JsonUtility.FromJsonOverwrite(data, this.preferences);
        }
    }
}