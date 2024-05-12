namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEditor;
    using UnityEngine;

    public class StateInspectorHelper : ScriptableObjectSingleton<StateInspectorHelper>
    {
        public void Inspect(StateMachine stateMachine, Graph graph, State state)
        {
            this.StateMachine = stateMachine;
            this.Graph = graph;
            this.StateID = state.ID;

            Selection.activeObject = this;

            var inspectors = Resources.FindObjectsOfTypeAll<StateInspector>();

            foreach(var inspector in inspectors)
            {
                inspector.Reload();
            }
        }

        public string StateID { get; private set; }

        public Graph Graph { get; private set; }

        public StateMachine StateMachine { get; private set; }
    }
}