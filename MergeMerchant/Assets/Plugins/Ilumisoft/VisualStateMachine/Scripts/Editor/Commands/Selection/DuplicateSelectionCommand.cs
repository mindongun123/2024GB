namespace Ilumisoft.VisualStateMachine.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    public class DuplicateSelectionCommand : ICommand
    {
        [System.NonSerialized] private Dictionary<string, Node> nodeDict;

        [System.NonSerialized] private Dictionary<string, Transition> transitionDict;

        //The graph we are working on
        private readonly Graph graph;

        //The nodes which should be duplicated
        private readonly List<Node> templateNodes;

        public DuplicateSelectionCommand(Graph graph, List<Node> nodes)
        {
            nodeDict = new Dictionary<string, Node>();
            transitionDict = new Dictionary<string, Transition>();

            this.graph = graph;
            templateNodes = nodes;
        }

        public void Execute()
        {
            //Check if nodes are graph members
            foreach (var node in templateNodes)
            {
                if (graph.HasNode(node.ID) == false)
                {
                    Debug.Log("Can only duplicate nodes that are part of the graph");

                    return;
                }
            }

            //Copy transitions
            var transitions = CopyTransitions();

            //Copy nodes
            var nodes = CopyNodes(transitions).Values.ToList();

            //Add nodes to graph
            foreach (var node in nodes)
            {
                graph.TryAddNode(node);
            }

            //Add transitions to graph
            foreach (var transition in transitions)
            {
                graph.TryAddTransition(transition);
            }

            //Select the new nodes
            templateNodes.Clear();

            foreach (var node in nodes)
            {
                templateNodes.Add(node);
            }
        }

        /// <summary>
        /// Copies all given template nodes and returns a dictionary containing all copies
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="templateNodes"></param>
        /// <param name="transitions"></param>
        /// <returns></returns>
        private Dictionary<string, Node> CopyNodes(List<Transition> transitions)
        {
            var copy = new Dictionary<string, Node>();

            foreach (var node in templateNodes)
            {
                if (!copy.ContainsKey(node.ID))
                {
                    Rect rect = node.Rect;
                    rect.x += 40;
                    rect.y += 40;

                    Node nodeCopy = CopyNode(node);

                    nodeCopy.ID = GetUniqueNodeNameCopy(copy, node.ID);
                    nodeCopy.Rect = rect;

                    UpdateTransitions(transitions, node.ID, nodeCopy.ID);

                    copy.Add(nodeCopy.ID, nodeCopy);
                }
            }

            return copy;
        }

        /// <summary>
        /// Copies a given node and returns the copy
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node CopyNode(Node node)
        {
            //Unity's JsonUtility does not suppot polymorphism, 
            //therefore we need to distinct between the concete types.
            if (node is State state)
            {
                return CopyNode(state);
            }
            else if (node is AnyState anyState)
            {
                return CopyNode(anyState);
            }

            return null;
        }

        /// <summary>
        /// Copies a given node by using serialization and returns the copy. In that way all event data will be copied too.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        private T CopyNode<T>(T node) where T : Node
        {
            var json = JsonUtility.ToJson(node);

            T copy = JsonUtility.FromJson<T>(json);

            return copy;
        }

        /// <summary>
        /// Updates the origin and target node name of all transitions
        /// </summary>
        /// <param name="transitions"></param>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        private void UpdateTransitions(List<Transition> transitions, string oldName, string newName)
        {
            foreach (var transition in transitions)
            {
                if (transition.OriginID == oldName)
                {
                    transition.OriginID = newName;
                }
                if (transition.TargetID == oldName)
                {
                    transition.TargetID = newName;
                }
            }
        }

        /// <summary>
        /// Copies all transitons which origin and target nodes are part of the given node list
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="templateNodes"></param>
        /// <returns></returns>
        private List<Transition> CopyTransitions()
        {
            //Create a dictionary from the given nodes
            var nodeDict = new Dictionary<string, Node>();

            foreach (var node in templateNodes)
            {
                if (!nodeDict.ContainsKey(node.ID))
                {
                    nodeDict.Add(node.ID, node);
                }
            }

            //Get all transitions which origin and target node is in selection
            var query = graph.Transitions.Where(x => nodeDict.ContainsKey(x.OriginID) && nodeDict.ContainsKey(x.TargetID));

            //Create a dictionary to hold all copied transitions
            var result = new List<Transition>();

            //Copy all transitions
            foreach (var transition in query)
            {
                var json = JsonUtility.ToJson(transition);

                var transitionCopy = JsonUtility.FromJson<Transition>(json);

                transitionCopy.ID = GetUniqueTransitionNameCopy(result, transition.ID);

                result.Add(transitionCopy);
            }

            return result;
        }

        private string GetUniqueNodeNameCopy(Dictionary<string, Node> workingDict, string name)
        {
            int x = 1;

            name = TrimEndNumbers(name);

            var stringBuilder = new StringBuilder(name);

            while (graph.HasNode(stringBuilder.ToString()) || workingDict.ContainsKey(stringBuilder.ToString()))
            {
                stringBuilder.Clear();
                stringBuilder.Append(name).Append(" ").Append(x);
                x++;
            }

            return stringBuilder.ToString();
        }

        private string GetUniqueTransitionNameCopy(List<Transition> workingDict, string name)
        {
            int x = 1;

            name = TrimEndNumbers(name);

            var stringBuilder = new StringBuilder(name);

            while (graph.HasTransition(stringBuilder.ToString()) || workingDict.Exists(t => t.ID == stringBuilder.ToString()))
            {
                stringBuilder.Clear();
                stringBuilder.Append(name).Append(" ").Append(x);
                x++;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Trims all numbers and whitespaces at the end of a string and returns the result
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string TrimEndNumbers(string value)
        {
            var characters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };

            return value.TrimEnd(characters);
        }
    }
}