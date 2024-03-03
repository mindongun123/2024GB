namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;
    using UnityEditor;
    
    public class TransitionInspectorHelper : ScriptableObjectSingleton<TransitionInspectorHelper>
    {
        public void Inspect(StateMachine stateMachine, Transition transition)
        {
            this.StateMachine = stateMachine;

            this.TransitionID = transition.ID;

            this.Transition = transition;

            Selection.activeObject = this;

            var inspectors = Resources.FindObjectsOfTypeAll<TransitionInspector>();

            foreach (var inspector in inspectors)
            {
                inspector.Reload();
            }
        }

        public StateMachine StateMachine { get; private set; }

        public Transition Transition { get; private set; }

        public string TransitionID { get; private set; }
    }
}