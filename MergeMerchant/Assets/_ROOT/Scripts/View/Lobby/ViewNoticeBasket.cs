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
            if (ConfigNotice.GetNotifyViewBasket() == 1)
            {
                notify.SetActive(true);
                return;
            }
            notify.SetActive(false);
        }
    }

}