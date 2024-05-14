using System;
using System.Collections;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.House
{
    public class ViewItemEnergy : MonoBehaviour
    {
        TIME_REMAINING timeRemaining;

        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] Image imgEnergy;

        public void SetTimeRemaining(TIME_REMAINING kTime)
        {
            timeRemaining = kTime;

            CheckRemaining();
            ShowTimeRemaining();
        }

        public void CheckRemaining()
        {
            TimeSpan timeSpan = ConfigTime.ToTimeSpan(timeRemaining._start, DateTime.Now.ToString());

            int _second = (int)timeSpan.TotalSeconds;
            timeRemaining._remaining = ConstGame.TIME_DEFAULT - _second;
            timeRemaining._remaining = timeRemaining._remaining > 0 ? timeRemaining._remaining : 0;
        }


        public void ShowTimeRemaining()
        {
            StartCoroutine(CoundownIE());
            IEnumerator CoundownIE()
            {
                imgEnergy.fillAmount = 1 - ((float)timeRemaining._remaining / ConstGame.TIME_DEFAULT);
                txt.text = TextTimeRemaining(timeRemaining._remaining);
                timeRemaining._remaining--;
                if (timeRemaining._remaining >= 0)
                {
                    yield return new WaitForSeconds(1);
                    StartCoroutine(CoundownIE());
                }
                else
                {
                    txt.transform.parent.gameObject.SetActive(false);
                }
            }
        }

        public string TextTimeRemaining(int _rem)
        {
            int _hour = _rem / 3600;
            int _min = (_rem - _hour * 3600) / 60;
            int _sec = _rem - _hour * 3600 - _min * 60;
            return $"{_hour}:{_min.ToString("D2")}:{_sec.ToString("D2")}";
        }

        public void OnClickAddEnergy()
        {
            if (timeRemaining._remaining >= 0)
            {
                SingletonComponent<ViewItemEnergyController>.Instance.OpenViewTimeRemaining(timeRemaining);
                return;
            }
            ViewReward.AddEnergy(50);
            txt.transform.parent.gameObject.SetActive(true);
            timeRemaining.Reset();
            ShowTimeRemaining();
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, 10, NameItem.energy);
        }
    }
}
