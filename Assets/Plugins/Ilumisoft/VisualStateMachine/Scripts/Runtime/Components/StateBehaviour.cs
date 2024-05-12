namespace Ilumisoft.VisualStateMachine
{
    using UnityEngine;

    /// <summary>
    /// Base class to create custom behaviours for states
    /// </summary>
    public abstract class StateBehaviour : MonoBehaviour
    {
        [SerializeField]
        StateMachine stateMachine;

        State state = null;

        /// <summary>
        /// ID of the state this behaviour is belonging to
        /// </summary>
        public abstract string StateID { get; }

        /// <summary>
        /// The State Machine owning the state this behaviour is belonging to
        /// </summary>
        public StateMachine StateMachine { get => this.stateMachine; set => this.stateMachine = value; }

        protected virtual void Awake()
        {
            if (StateMachine != null)
            {
                // Get the state
                state = StateMachine.Graph.GetState(StateID);

                // Add listeners to enter, exit and update events of the state
                if (state != null)
                {
                    state.OnEnterState.AddListener(OnEnterState);
                    state.OnExitState.AddListener(OnExitState);
                    state.OnUpdateState.AddListener(OnUpdateState);
                }
                else
                {
                    Debug.Log($"Could not find state with the id '{StateID}'", this);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            // Stop listening to state events when the behaviour gets destroyed
            if (StateMachine != null && state != null)
            {
                state.OnEnterState.RemoveListener(OnExitState);
                state.OnExitState.RemoveListener(OnExitState);
                state.OnUpdateState.RemoveListener(OnUpdateState);
            }
        }

        /// <summary>
        /// Automatically tries to assign a state machine, when the component gets created or reset
        /// </summary>
        private void Reset()
        {
            if (StateMachine == null)
            {
                StateMachine = GetComponentInParent<StateMachine>();
            }
        }

        /// <summary>
        /// Returns true if the state is the currently active one
        /// </summary>
        public bool IsActiveState => StateMachine.CurrentState == StateID;

        /// <summary>
        /// Callback invoked when the state is entered
        /// </summary>
        protected virtual void OnEnterState() { }

        /// <summary>
        /// Callback invoked when the state is exit
        /// </summary>
        protected virtual void OnExitState() { }

        /// <summary>
        /// Callback invoked when the state is active and updated
        /// </summary>
        protected virtual void OnUpdateState() { }
    }
}