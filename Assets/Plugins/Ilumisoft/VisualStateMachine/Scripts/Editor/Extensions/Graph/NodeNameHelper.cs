namespace Ilumisoft.VisualStateMachine.Editor.Extensions
{
    using System.Collections.Generic;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    public static class NodeNames
    {
        public static bool TryRenameState(this StateMachine stateMachine, string oldId, string id)
        {
            var graph = stateMachine.GetStateMachineGraph();

            //Cancel if no name provided
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            
            //Cancel if name not available
            if (graph.TryGetNode(id, out _))
            {
                Debug.LogWarningFormat(ErrorMessages.TakenStateID, oldId, id);

                return false;
            }
            
            Undo.RegisterCompleteObjectUndo(stateMachine, "Change node id");
            
            //Apply new name
            UpdateTransitions(graph, oldId, id);
            UpdateStateName(graph, oldId, id);

            return true;
        }

        /// <summary>
        /// Applies the given new name to the state in the given graph with the given old name
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="oldStateName"></param>
        /// <param name="newStateName"></param>
        private static void UpdateStateName(Graph graph, string oldStateName, string newStateName)
        {
            if(graph.TryGetNode(oldStateName, out Node node))
            {
                var state = node as State;

                if(state != null)
                {
                    //Update entry state name of the graph
                    if (graph.EntryStateID == oldStateName)
                    {
                        graph.EntryStateID = newStateName;
                    }

                    //Apply name to state
                    state.ID = newStateName;

                    graph.Cache.RebuildDictionary();
                }
            }
        }

        /// <summary>
        /// Renames all transitions with reference to the given old state name to use the given new one
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="oldStateName"></param>
        /// <param name="newStateName"></param>
        private static void UpdateTransitions(Graph graph, string oldStateName, string newStateName)
        {
            foreach (var transition in graph.Transitions)
            {
                if (transition.OriginID == oldStateName)
                {
                    transition.OriginID = newStateName;
                }

                if (transition.TargetID == oldStateName)
                {
                    transition.TargetID = newStateName;
                }
            }
        }

        /// <summary>
        /// Returns an available state name for the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string GetUniqueStateName(this Graph graph)
        {
            return GetUniqueNodeName(graph.Nodes, "State");
        }

        /// <summary>
        /// Returns an available AnyNode name for the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string GetUniqueAnyStateName(this Graph graph)
        {
            return GetUniqueNodeName(graph.Nodes, "AnyState");
        }
        
        /// <summary>
        /// Returns a unique new node name 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="baseName"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        private static string GetUniqueNodeName<TValue>(IList<TValue> nodes, string baseName) where TValue: Node
        {
            int x = 1;

            string res = baseName;

            var stringBuilder = new StringBuilder();

            Dictionary<string, Node> dictionary = new Dictionary<string, Node>();

            foreach(Node node in nodes)
            {
                dictionary.Add(node.ID, node);
            }

            while (dictionary.ContainsKey(res))
            {
                stringBuilder.Clear();
                stringBuilder.Append(baseName).Append(" ").Append(x);
                res = stringBuilder.ToString();
                x++;
            }

            return res;
        }
    }
}