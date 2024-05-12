using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewDailyItem : MonoBehaviour
    {
        public int _diamond;
        public int _coin = 0;
        [SerializeField] TextMeshProUGUI txtWait;
        [SerializeField] GameObject completeDaily;
        [SerializeField] GameObject getDaily;
        public void TextWaitDay(int _cl)
        {
            txtWait.text = _cl.ToString() + "n";
        }
        public void EnableCompleteDaily(bool _isTrue = true)
        {
            completeDaily.SetActive(_isTrue);
        }
        public void EnableGetDaily(bool _isTrue)
        {
            getDaily.SetActive(_isTrue);
        }

        public void OnClickComplete()
        {
            // Audio 
            SingletonComponent<AudioController>.Instance.AudioOnClickPlay();
            //
            SingletonComponent<SaveLobbyGame>.Instance.CompleteDaily();
            EnableGetDaily(true);
            EnableCompleteDaily(false);
            AddDaily();
            ShowVFX();
            SingletonComponent<ButtonDaily>.Instance.EnableNotify(false);
        }

        public void AddDaily()
        {
            ViewReward.AddCoin(_coin);
            ViewReward.AddDiamond(_diamond);
        }

        private void ShowVFX()
        {
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, _coin, NameItem.coin);
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, _diamond, NameItem.diamond);
        }
    }

}