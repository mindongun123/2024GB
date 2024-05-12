namespace Ilumisoft.VisualStateMachine
{
    using System.Collections;
    using UnityEngine;

    public class TransitionTrigger : MonoBehaviour
    {
        [System.Serializable]
        public enum TriggerType
        {
            ID = 0,
            Label = 1,
        }

        [SerializeField]
        StateMachine stateMachine = null;

        [SerializeField]
        TriggerType type = TriggerType.ID;

        [SerializeField]
        string key = string.Empty;

        [SerializeField]
        bool executeOnStart = false;

        [SerializeField]
        TimeMode timeMode = TimeMode.Scaled;

        [SerializeField]
        float delay = 0.0f;

        [SerializeField]
        bool logWarnings = true;

        /// <summary>
        /// Gets the state machine the trigger is executed on
        /// </summary>
        public StateMachine StateMachine => stateMachine;

        bool IsStateMachineSet => stateMachine != null;

        /// <summary>
        /// Gets or sets the time mode of the delay (scaled vs unscaled)
        /// </summary>
        public TimeMode TimeMode
        {
            get => timeMode;
            set => timeMode = value;
        }

        /// <summary>
        /// Gets or sets the delay in seconds, which the trigger waits after Execute is called, 
        /// before the transition is triggered
        /// </summary>
        public float Delay
        {
            get => delay;
            set => delay = value;
        }

        void Start()
        {
            if(executeOnStart)
            {
                Execute();
            }
        }

        /// <summary>
        /// Executes the trigger
        /// </summary>
        public void Execute()
        {
            if(!IsStateMachineSet && logWarnings)
            {
                Debug.LogWarning("Could not execute transition trigger, because no state machine has been set", this);
                return;
            }

            if(delay>0.0f)
            {
                DelayedTriggerTransition(delay);
            }
            else
            {
                TriggerTransition();
            }
        }

        /// <summary>
        /// Triggers the defined transition after a given delay
        /// </summary>
        /// <param name="delay"></param>
        private void DelayedTriggerTransition(float delay)
        {
            StopAllCoroutines();
            StartCoroutine(DelayedTriggerCoroutine(delay));
        }     

        /// <summary>
        /// Coroutine that triggers the defined transition after a given delay
        /// </summary>
        /// <returns></returns>
        IEnumerator DelayedTriggerCoroutine(float delay)
        {
            if(timeMode == TimeMode.Scaled)
            {
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return new WaitForSecondsRealtime(delay);
            }

            TriggerTransition();
        }

        /// <summary>
        /// Triggers the transition according to the trigger settings
        /// </summary>
        private void TriggerTransition()
        {
            if (type == TriggerType.ID)
            {
                TriggerTransitionByID(key);
            }
            else
            {
                TriggerTransitionByLabel(key);
            }
        }

        /// <summary>
        /// Triggers the transition with the given id
        /// </summary>
        /// <param name="id"></param>
        private void TriggerTransitionByID(string id)
        {
            if (stateMachine.TryTrigger(id) == false && logWarnings)
            {
                Debug.LogWarning($"Failed to trigger transition with ID '{id}'.", this);
            }
        }

        /// <summary>
        /// Triggers the first valid transition found with the given label
        /// </summary>
        /// <param name="label"></param>
        private void TriggerTransitionByLabel(string label)
        {
            if (stateMachine.TryTriggerByLabel(label) == false && logWarnings)
            {
                Debug.LogWarning($"Failed to trigger transition with Label '{label}'.", this);
            }
        }
    }
}