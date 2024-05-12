using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MJGame.MergeMerchant.Merge;
using UnityEngine.Events;
using TMPro;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewRoulette : MonoBehaviour
    {
        int _idx;
        [SerializeField] ViewItemRoulette[] viewItemRoulettes;
        bool _isRou = false;
        public void RotateStart()
        {
            if (_isRou) return;
            btnClose.SetActive(true);
            ConfigNotice.eventNoticeRoulette?.Invoke();
            transform.rotation = Quaternion.identity;
            _idx = Random.Range(0, 8);
            _isRou = true;

            transform.DORotate(new Vector3(0f, 0f, _idx * 45 + 360f * 25), 12f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuint)
                .OnComplete(() =>
                {
                    AddReward();
                    _isRou = false;
                });
        }

        UnityAction actionClickRotate;
        public void OnClickRouterRotate()
        {
            if (_isRou) return;
            SingletonComponent<SaveLobbyGame>.Instance.NumberSpin -= 1;
            RotateStart();
            actionClickRotate?.Invoke();
        }


        private void AddReward()
        {
            int _rw = viewItemRoulettes[_idx]._reward;
            switch (viewItemRoulettes[_idx].nameItem)
            {
                case NameItem.coin:
                    ViewReward.AddCoin(_rw);
                    break;
                case NameItem.diamond:
                    ViewReward.AddDiamond(_rw);
                    break;
                case NameItem.energy:
                    ViewReward.AddEnergy(_rw);
                    break;
                default:
                    break;
            }
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position + new Vector3(0, -200, 0), _rw, viewItemRoulettes[_idx].nameItem, GIE.GetItemEffectType.JumpAway_First);
            btnClose.SetActive(false);
        }

        [SerializeField] TextMeshProUGUI txtNumberSpin;
        [SerializeField] GameObject btnClose;

        private void OnEnable()
        {
            actionClickRotate += GetNumberSpin;
            actionClickRotate?.Invoke();
            _isRou = false;
        }

        private void OnDisable()
        {
            actionClickRotate -= GetNumberSpin;
            transform.DOKill();
        }


        [SerializeField] GameObject btnSpin, btnSpinAd;

        public void GetNumberSpin()
        {
            int _numberSpin = SingletonComponent<SaveLobbyGame>.Instance.NumberSpin;
            btnSpin.SetActive(_numberSpin > 0);
            btnSpinAd.SetActive(_numberSpin <= 0);
            txtNumberSpin.text = _numberSpin.ToString();
        }



        public void OnClickButtonSpinFreeAdd()
        {
            if (ViewReward.AddDiamond(-35))
            {
                SingletonComponent<SaveLobbyGame>.Instance.NumberSpin += 1;
                ConfigNotice.eventNoticeRoulette?.Invoke();
                actionClickRotate?.Invoke();
            }
        }
    }
}