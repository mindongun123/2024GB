using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.Reward
{
    public class SelectBackgroundReward : MonoBehaviour
    {
        [SerializeField] GameObject[] bg;
        private void OnEnable()
        {
            bg[Random.Range(0, bg.Length)].SetActive(true);
        }
    }
}