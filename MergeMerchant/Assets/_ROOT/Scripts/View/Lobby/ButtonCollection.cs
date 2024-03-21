using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MJGame.MergeMerchant.Lobby
{
    public class ButtonCollection : MonoBehaviour
    {

        [SerializeField] GameObject notify;
        private void OnEnable()
        {
            MonthEvent();
            ConfigNotice.eventNoticeViewButtonOpenOption += EnableNotify;
            ConfigNotice.eventNoticeViewButtonOpenOption?.Invoke();
        }

        private void OnDisable()
        {
            ConfigNotice.eventNoticeViewButtonOpenOption -= EnableNotify;
        }

        private void EnableNotify()
        {
            if (ConfigNotice.GetNotifyViewOption() > 0 || ConfigNotice.GetNotifyViewBasket() == 1)
            {
                notify.SetActive(true);
                return;
            }
            notify.SetActive(false);
        }

        [SerializeField] GameObject[] monthEvent;

        public void MonthEvent()
        {
            monthEvent[ConfigTime.MonthEvent()].gameObject.SetActive(true);
        }
    }
}