using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using UnityEngine;
using UnityEngine.Events;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewNoticeButtonRoulette : SingletonComponent<ViewNoticeButtonRoulette>
    {
        public UnityAction<int> eventShowTextTimeFree;
        [SerializeField] GameObject notice;
        private int _time;
        private void OnEnable()
        {
            LoadTimeFree();
            ConfigNotice.eventNoticeRoulette += CheckNotice;
            ConfigNotice.eventNoticeRoulette?.Invoke();
        }
        private void OnDisable()
        {
            ConfigNotice.eventNoticeRoulette -= CheckNotice;
        }
        public void ShowTextSpinTimeFree()
        {
            eventShowTextTimeFree?.Invoke(_time);
        }
        
        public void LoadTimeFree()
        {
            string timeOld = PlayerPrefs.GetString(ConstGame.TIME_SPIN_FREE, "20/03/2000 9:15:29 CH");
            TimeSpan timeSpan = ConfigTime.ToTimeSpan(timeOld, DateTime.Now.ToString());
            this._time = 259199 - (int)timeSpan.TotalSeconds;
            _time = _time > 0 ? _time : 0;
            CheckTimeFree();
        }

        private void CheckTimeFree()
        {
            if (_time <= 0)
            {
                OpenNotice(true);
            }
            else
            {
                StartCoroutine(CountDownIE());
                IEnumerator CountDownIE()
                {
                    eventShowTextTimeFree?.Invoke(_time);
                    yield return new WaitForSeconds(1);
                    _time--;
                    StartCoroutine(CountDownIE());
                }
            }
        }

        private void CheckNotice()
        {
            if (SingletonComponent<SaveLobbyGame>.Instance.NumberSpin > 0 || this._time <= 0)
            {
                OpenNotice(true);
            }
            else
            {
                OpenNotice(false);
            }
        }

        private void OpenNotice(bool _is)
        {
            notice.SetActive(_is);
        }
    }
}