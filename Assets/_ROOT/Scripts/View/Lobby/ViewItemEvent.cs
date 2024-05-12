using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant.Lobby
{
    public class ViewItemEvent : MonoBehaviour
    {
        private int _coinEv;
        private int _coinEvMax;
        private int _stt;
        private string _userName;
        public Sprite[] sprites;
        public Image image;
        public Slider slider;
        public TextMeshProUGUI txtUserNameAccount;
        public TextMeshProUGUI txtCoin;
        [SerializeField] GameObject ok;

        public void SetViewItem(int _coinEv, int _coinEvMax, int _stt, string _userName, bool _isOk = false)
        {
            this._stt = _stt;
            this._coinEv = _coinEv;
            this._coinEvMax = _coinEvMax;
            this._userName = _userName;
            ok.SetActive(_isOk);
            ShowViewItem();
        }

        private void ShowViewItem()
        {
            slider.value = (float)_coinEv / _coinEvMax;
            image.sprite = sprites[_stt < 3 ? _stt : 3];
            txtUserNameAccount.text = $"{_userName}";
            txtCoin.text = _coinEv.ToString();
        }

    }
}