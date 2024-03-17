using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MJGame.MergeMerchant.Lobby
{

    public class ViewNoticeBasket : MonoBehaviour
    {
        [SerializeField] GameObject notify;

        private void OnEnable()
        {
            ConfigNotice.eventNotifyWhenHasNewBasket += OpenNotifyWhenHasNewBasket;
            ConfigNotice.eventNotifyWhenHasNewBasket?.Invoke();
        }

        private void OnDisable()
        {
            ConfigNotice.eventNotifyWhenHasNewBasket -= OpenNotifyWhenHasNewBasket;
        }
        public void OpenNotifyWhenHasNewBasket()
        {
            if (ConfigNotice.GetNotifyViewBasket() == 1)
            {
                notify.SetActive(true);
                return;
            }
            notify.SetActive(false);
        }
    }

}