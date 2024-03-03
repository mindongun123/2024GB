using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{

    public class ViewEventMonthDaily : MonoBehaviour
    {
        [SerializeField]
        GameObject[] eventDaily;
        private void OnEnable()
        {
            MonthEventDaily();
        }
        public void MonthEventDaily()
        {
            eventDaily[ConfigTime.MonthEvent()].SetActive(true);
        }
    }

}