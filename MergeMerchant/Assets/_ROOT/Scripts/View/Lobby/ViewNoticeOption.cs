using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewNoticeOption : MonoBehaviour
    {
        [SerializeField] GameObject notify;

        private void OnEnable()
        {
            ConfigNotice.eventNoticeViewOption += OnActionNotify;
            ConfigNotice.eventNoticeViewOption?.Invoke();
        }

        private void OnDisable()
        {
            ConfigNotice.eventNoticeViewOption -= OnActionNotify;
        }
        
        private void OnActionNotify()
        {
            int _nt = ConfigNotice.GetNotifyViewOption();
            if (_nt > 0)
            {
                notify.SetActive(true);
                return;
            }
            notify.SetActive(false);
        }
    }
}