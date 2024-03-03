namespace Ilumisoft.VisualStateMachine.Internal
{
    using System.Collections.Generic;

    public class GraphCache
    {
        [System.NonSerialized]
        public List<Transition> Transitions = new List<Transition>();

        [System.NonSerialized]
        public List<Node> Nodes = new List<Node>();

        public Dictionary<string, Node> NodeDictionary { get; } = new Dictionary<string, Node>();

        public Dictionary<string, Transition> TransitionDictionary { get; } = new Dictionary<string, Transition>();

        public void SerializeCache(Graph graph)
        {
            Graph.SerializedData serializedData = new Graph.SerializedData(graph);

            serializedData.States.Clear();
            serializedData.AnyStates.Clear();

            foreach (var node in Nodes)
            {
                if (node is State state)
                {
                    serializedData.States.Add(state);
                }
                else if (node is AnyState anyState)
                {
                    serializedData.AnyStates.Add(anyState);
                }
            }

            serializedData.Transitions.Clear();

            foreach (var transition in Transitions)
            {
                serializedData.Transitions.Add(transition);
            }
        }

        public void RebuildDictionary()
        {
            BuildNodeDictionary();
            BuildTransitionDictionary();
        }

        public void BuildCache(Graph graph)
        {
            Graph.SerializedData serializedData = new Graph.SerializedData(graph);

            CacheNodes(serializedData);
            CacheTransitions(serializedData);
            BuildNodeDictionary();
            BuildTransitionDictionary();
        }

        void CacheNodes(Graph.SerializedData serializedData)
        {
            Nodes.Clear();

            foreach (var node in serializedData.States)
            {
                Nodes.Add(node);
            }

            foreach (var node in serializedData.AnyStates)
            {
                Nodes.Add(node);
            }
        }

        void CacheTransitions(Graph.SerializedData serializedData)
        {
            Transitions.Clear();

            foreach(var transition in serializedData.Transitions)
            {
                Transitions.Add(transition);
            }
        }

        void BuildNodeDictionary()
        {
            NodeDictionary.Clear();

            foreach (var node in Nodes)
            {
                if (NodeDictionary.ContainsKey(node.ID) == false)
                {
                    NodeDictionary.Add(node.ID, node);
                }
            }
        }

        private void BuildTransitionDictionary()
        {
            TransitionDictionary.Clear();

            foreach (var transition in Transitions)
            {
                if (TransitionDictionary.ContainsKey(transition.ID) == false)
                {
                    TransitionDictionary.Add(transition.ID, transition);
                }
            }
        }

        public Transition GetTransition(string id)
        {
            if (TryGetTransition(id, out Transition transition))
            {
                return transition;
            }

            return null;
        }

        public Node GetNode(string id)
        {
            if (TryGetNode(id, out Node node))
            {
                return node;
            }

            return null;
        }

        public State GetState(string id)
        {
            Node node = GetNode(id);

            if (node != null && node is State)
            {
                return node as State;
            }
            else
            {
                return null;
            }
        }

        public bool TryGetTransition(string id, out Transition transition)
        {
            return TransitionDictionary.TryGetValue(id, out transition);
        }

        public bool TryGetNode(string id, out Node node)
        {
            return NodeDictionary.TryGetValue(id, out node);
        }

        public bool TryGetState(string id, out State state)
        {
            if (TryGetNode(id, out Node node) && node is State)
            {
                state = node as State;
                return true;
            }
            else
            {
                state = null;
                return false;
            }
        }

        public bool TryAddNode(Node node)
        {
            if (NodeDictionary.ContainsKey(node.ID) == false)
            {
                Nodes.Add(node);
                NodeDictionary.Add(node.ID, node);
                return true;
            }

            return false;
        }

        public bool TryRemoveNode(Node node)
        {
            if (NodeDictionary.ContainsKey(node.ID))
            {
                Nodes.Remove(node);
                NodeDictionary.Remove(node.ID);
                return true;
            }

            return false;
        }

        public bool TryAddTransition(Transition transition)
        {
            if (TransitionDictionary.ContainsKey(transition.ID) == false)
            {
                Transitions.Add(transition);
                TransitionDictionary.Add(transition.ID, transition);
                return true;
            }

            return false;
        }

        public bool TryRemoveTransition(Transition transition)
        {
            bool success = false;

            if (TransitionDictionary.ContainsKey(transition.ID))
            {
                TransitionDictionary.Remove(transition.ID);
                success = true;
            }

            if (Transitions.Contains(transition))
            {
                Transitions.Remove(transition);
                success = true;
            }

            return success;
        }

        public bool HasTransition(string id)
        {
            return TransitionDictionary.ContainsKey(id);
        }

        public bool HasNode(string id)
        {
            return NodeDictionary.ContainsKey(id);
        }

        public bool HasState(string id)
        {
            return TryGetState(id, out _);
        }
    }
}