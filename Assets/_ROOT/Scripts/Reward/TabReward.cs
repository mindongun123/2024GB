using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.Reward
{
    public class TabReward : MonoBehaviour
    {
        [SerializeField] GameObject[] tabReward;

        private void OnEnable()
        {
            int idx = Random.Range(0, tabReward.Length);
            tabReward[idx].SetActive(true);
        }

    }
}