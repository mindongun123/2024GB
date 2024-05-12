using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MJGame.MergeMerchant.Lobby
{
    public class ButtonDaily : SingletonComponent<ButtonDaily>
    {
        [SerializeField] GameObject notify;
        private void OnEnable()
        {
            MonthEvent();
            string dateDailyOld = SingletonComponent<SaveLobbyGame>.Instance.DateTimeDailyOld;
            string now = DateTime.Now.ToString();
            bool areDatesDifferent = ConfigTime.CheckAreDatesDifferent(dateDailyOld, now);
            EnableNotify(areDatesDifferent);
        }

        public void EnableNotify(bool _is)
        {
            notify.SetActive(_is);
        }
        [SerializeField] GameObject[] monthEvent;
        
        public void MonthEvent()
        {
            monthEvent[ConfigTime.MonthEvent()].gameObject.SetActive(true);
        }
    }
}