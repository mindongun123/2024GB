namespace Ilumisoft.VisualStateMachine.Editor.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class GraphInputHelper
    {
        /// <summary>
        /// Returns the first state found, which contains the mouse position
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="layer"></param>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public static Node GetClickedNode(this Graph graph, GraphLayer layer, Vector2 mousePos)
        {
            IEnumerable<Node> reverse = graph.Nodes.Reverse<Node>();

            foreach (var node in reverse)
            {
                Rect transformedRect = layer.GetTransformedRect(node.Rect);

                if (transformedRect.Contains(mousePos))
                {
                    return node;
                }
            }

            return null;
        }
    }
}
