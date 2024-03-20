using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewTimeSpinFree : MonoBehaviour
    {
        [SerializeField] GameObject btnSpinFree;
        [SerializeField] TextMeshProUGUI txtTimeFree;
        private void LoadTimeFree()
        {
            string timeOld = PlayerPrefs.GetString(ConstGame.TIME_SPIN_FREE, "20/03/2000 9:15:29 CH");
            TimeSpan timeSpan = ConfigTime.ToTimeSpan(timeOld, DateTime.Now.ToString());
            CheckTimeFree(259199 - (int)timeSpan.TotalSeconds);
        }

        private void CheckTimeFree(int _timeSpan)
        {
            if (_timeSpan <= 0)
            {
                btnSpinFree.SetActive(true);
                txtTimeFree.text = "";
            }
            else
            {
                StartCoroutine(CountDown());
                IEnumerator CountDown()
                {
                    txtTimeFree.text = SetTextTime(_timeSpan);
                    yield return new WaitForSeconds(1);
                    _timeSpan--;
                    StartCoroutine(CountDown());
                }
            }
        }

        public void OnClickButtonFree()
        {
            PlayerPrefs.SetString(ConstGame.TIME_SPIN_FREE, DateTime.Now.ToString());
            LoadTimeFree();
        }

        private string SetTextTime(int _time)
        {
            int _day = _time / 86400;
            _time = _time % 86400;
            int _hour = _time / 3600;
            int _min = (_time - _hour * 3600) / 60;
            int _sec = _time - _hour * 3600 - _min * 60;
            return $"{_day}n {_hour}:{_min.ToString("D2")}:{_sec.ToString("D2")}";
        }

        private void OnEnable()
        {
            LoadTimeFree();
        }
    }
}