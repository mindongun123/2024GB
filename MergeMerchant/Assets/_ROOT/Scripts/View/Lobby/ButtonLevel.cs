using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{
    public class ButtonLevel : MonoBehaviour
    {

        [SerializeField]
        GameObject notify;

        [SerializeField] float _exp;

        private void OnEnable()
        {
            _exp = SingletonComponent<SaveLobbyGame>.Instance.GetExp();

            ConfigNotice.eventNotifyViewLevelPass += EnableNotify;
            eventExpLevel += ViewExpLevel;
            ConfigNotice.eventNotifyViewLevelPass?.Invoke();
            eventExpLevel?.Invoke();
        }

        private void EnableNotify()
        {
            if (ConfigNotice.GetNotifyLevelPassReward() > 0)
            {
                notify.SetActive(true);
            }
            else
            {
                notify.SetActive(false);
            }
        }

        private void OnDisable()
        {
            ConfigNotice.eventNotifyViewLevelPass -= EnableNotify;
            eventExpLevel -= ViewExpLevel;
        }

        [SerializeField] Image imgExp;
        public UnityAction eventExpLevel;

        public void ViewExpLevel()
        {
            imgExp.fillAmount = _exp / ConstGame.EXP_LEVEL;
        }

        [Button]
        public void AddExp(int _val)
        {
            _exp += _val;
            SingletonComponent<SaveLobbyGame>.Instance.SaveExp(_exp);
            eventExpLevel?.Invoke();
        }
    }
}