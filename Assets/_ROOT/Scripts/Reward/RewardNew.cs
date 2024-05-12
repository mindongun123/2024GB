using System.Collections;
using System.Collections.Generic;
using Ilumisoft.VisualStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Reward
{
    public class RewardNew : MonoBehaviour
    {
        [SerializeField] StateMachine stateMachine;
        private void Start()
        {
            SelectReward();
        }

        [Button]
        public void SelectReward()
        {
            int[] showReward = new int[] { 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0 };
            int rd = Random.Range(0, showReward.Length);
            if (showReward[rd] == 0)
            {
                stateMachine.Trigger("OpenReward");
            }
        }


    }
}
