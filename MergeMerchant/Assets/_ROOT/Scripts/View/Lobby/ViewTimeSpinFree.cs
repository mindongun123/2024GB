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

        [SerializeField] ViewNoticeButtonRoulette viewNoticeButtonRoulette;

        private void OnEnable()
        {
            viewNoticeButtonRoulette.eventShowTextTimeFree += ShowTextTimeFree;
            viewNoticeButtonRoulette.ShowTextSpinTimeFree();
        }
        private void OnDisable()
        {
            viewNoticeButtonRoulette.eventShowTextTimeFree -= ShowTextTimeFree;
        }

        public void OnClickButtonFree()
        {
            PlayerPrefs.SetString(ConstGame.TIME_SPIN_FREE, DateTime.Now.ToString());
            viewNoticeButtonRoulette.LoadTimeFree();
        }

        private void ShowTextTimeFree(int _time)
        {
            if (_time <= 0)
            {
                btnSpinFree.SetActive(true);
                txtTimeFree.text = "";
            }
            else
            {
                btnSpinFree.SetActive(false);
                txtTimeFree.text = ConfigTime.ConvertTime(_time);
            }
        }

    }
}