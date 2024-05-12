using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{
    public class ButtonLevel : SingletonComponent<ButtonLevel>
    {

        [SerializeField]
        GameObject notify;

        [SerializeField] float _exp;

        [SerializeField]
        TextMeshProUGUI txtLevel;


        private void OnEnable()
        {
            _exp = SingletonComponent<SaveLobbyGame>.Instance.GetExp();

            ConfigNotice.eventNotifyViewLevelPass += EnableNotify;
            eventExpLevel += ViewExpLevel;
            ConfigNotice.eventNotifyViewLevelPass?.Invoke();
            eventExpLevel?.Invoke();

            ShowLevel();
        }

        public void ShowLevel()
        {
            txtLevel.text = PlayerPrefs.GetInt(ConstGame.LEVEL, 1).ToString();
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

        public void AddExp(int _val)
        {
            SingletonComponent<AudioController>.Instance.AudioRewardPlay();
            _exp += _val;
            if (_exp == 15)
            {
                SingletonComponent<LevelGame>.Instance.UpdateLevel();
                _exp = 0;
            }
            SingletonComponent<SaveLobbyGame>.Instance.SaveExp(_exp);
            eventExpLevel?.Invoke();
        }
    }
}