using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{

    public class ViewTimeRemaining : SingletonComponent<ViewTimeRemaining>
    {

        [SerializeField] TextMeshProUGUI txtRemaining;
        [SerializeField] TextMeshProUGUI txtCoinUpTime;
        [SerializeField] TextMeshProUGUI txtEnergyCurrent;
        [SerializeField] TextMeshProUGUI txtEnergyNext;
        [SerializeField] TextMeshProUGUI txtCoinUpEnergy;
        TIME_REMAINING timeRemaining;

        private void OnEnable()
        {
            noUpTime.SetActive(false);
            noUpEnergy.SetActive(false);
        }
        public void SetTimeRemaining(TIME_REMAINING kTime)
        {
            timeRemaining = kTime;
            ShowTimeRemaining();
            ShowEnergy();

            ShowIconRewardEnergy();
        }

        public GameObject noUpTime, noUpEnergy;
        public void ShowTimeRemaining()
        {
            if (timeRemaining._remaining < 6000)
            {
                noUpTime.SetActive(true);
                return;
            }
            txtRemaining.text = timeRemaining._remaining.ToString();
            txtCoinUpTime.text = timeRemaining.upTime.y.ToString();
        }

        public void ShowEnergy()
        {
            if (timeRemaining._energy > 195)
            {
                noUpEnergy.SetActive(true);
                return;
            }
            txtEnergyCurrent.text = timeRemaining._energy.ToString();
            txtEnergyNext.text = (timeRemaining._energy + timeRemaining.upEnergy.x).ToString();
            txtCoinUpEnergy.text = timeRemaining.upEnergy.y.ToString();
        }

        public void OnClickUpdateEnergy()
        {
            if (ViewReward.AddCoin(-timeRemaining.upEnergy.y))
            {
                timeRemaining.UpdateEnergy();
                ShowEnergy();
            }
        }

        public void OnClickUpdateTime()
        {
            if (ViewReward.AddCoin(-timeRemaining.upTime.y))
            {
                timeRemaining.UpdateTime();
                ShowTimeRemaining();
                ShowIconRewardEnergy();
            }
        }


        [SerializeField] Image imgIcon;
        private void ShowIconRewardEnergy()
        {
            imgIcon.fillAmount = 1 - (float)timeRemaining._remaining / ConstGame.TIME_DEFAULT;
        }
    }
}