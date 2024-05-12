using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewTimeDailyEnergy : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txtTime;
        [SerializeField] GameObject btnTxtTime;
        private void OnEnable()
        {
            string _timeOld = SingletonComponent<SaveLobbyGame>.Instance.TimeDailyEnergy;
            Debug.Log(_timeOld);
            TimeSpan _timespan = ConfigTime.ToTimeSpan(_timeOld, DateTime.Now.ToString());
            int _time = _timespan.Seconds + _timespan.Minutes * 60;
            StartCoroutine(IEShowTime());

            IEnumerator IEShowTime()
            {
                if (_time >= 1800)
                {
                    string tdsave = DateTime.Now.ToString();
                    SingletonComponent<SaveLobbyGame>.Instance.TimeDailyEnergy = tdsave;
                    ViewReward.AddEnergy(100);
                    btnTxtTime.transform.DOScale(Vector3.one * 1.25f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                    {
                        btnTxtTime.transform.localScale = Vector3.one;
                    });
                    _time = 0;
                }
                yield return new WaitForSeconds(1);
                txtTime.text = ConfigTime.ConvertTimeMini(1800 - _time);
                _time++;
                StartCoroutine(IEShowTime());
            }
        }
    }
}
