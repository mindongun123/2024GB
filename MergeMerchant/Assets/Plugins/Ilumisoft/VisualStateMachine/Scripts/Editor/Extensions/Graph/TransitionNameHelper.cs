namespace Ilumisoft.VisualStateMachine.Editor.Extensions
{
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    public static class TransitionNameHelper
    {
        /// <summary>
        /// Tries to rename the given transitions name to the given name
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="transition"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool TryRenameTransition(this StateMachine stateMachine, Transition transition, string id)
        {
            var graph = stateMachine.GetStateMachineGraph();

            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            else if (graph.TryGetTransition(id, out _))
            {
                Debug.LogWarningFormat(ErrorMessages.TakenTransitionName, transition.ID, id);

                return false;
            }
            else
            {
                Undo.RegisterCompleteObjectUndo(stateMachine, "Change transition name");

                transition.ID = id;

                graph.Cache.RebuildDictionary();

                return true;
            }
        }

        /// <summary>
        /// Returns an available transition name for the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string GetUniqueTransitionID(this Graph graph)
        {
            int x = 1;

            string res = "Transition";

            var stringBuilder = new StringBuilder();

            while (graph.TryGetTransition(res, out _))
            {
                stringBuilder.Clear();
                stringBuilder.Append("Transition").Append(" ").Append(x);
                res = stringBuilder.ToString();
                x++;
            }

            return res;
        }
    }
}