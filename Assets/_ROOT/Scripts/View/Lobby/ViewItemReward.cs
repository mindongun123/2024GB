using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewItemReward : MonoBehaviour
    {
        public int _money;
        public int _reward;
        public enum OptionReward
        {
            money, diamond, coin, energy
        }

        public OptionReward optionReward;

        public OptionReward optionBuy;

        public int _min;
        public int _max;

        [SerializeField] TextMeshProUGUI txtReward;
        [SerializeField] TextMeshProUGUI txtMoney;

        private void OnEnable()
        {
            SetMoney();
        }

        private void SetMoney()
        {
            _money = Random.Range(_min, _max);
            txtMoney.text = $"{_money} $";
            txtReward.text = $"+{_reward}";
        }

        public void OnClickBuyReward()
        {
            // Audio
            SingletonComponent<AudioController>.Instance.AudioOnClickPlay();
            //
            SetBuy();

        }
        private void SetRewardComplete()
        {
            if (optionReward == OptionReward.coin)
            {
                ViewReward.AddCoin(_reward);
                SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, _reward / 100, NameItem.coin);
            }
            else if (optionReward == OptionReward.diamond)
            {
                ViewReward.AddDiamond(_reward);
                SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, _reward / 100, NameItem.diamond);
            }
            else if (optionReward == OptionReward.energy)
            {
                ViewReward.AddEnergy(_reward);
                SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, _reward / 10, NameItem.energy);
            }
        }

        private void SetBuy()
        {
            if (optionBuy == OptionReward.money)
            {
                print(" Nap the de mua hang");
            }
            else if (optionBuy == OptionReward.diamond)
            {
                if (ViewReward.AddDiamond(-_money))
                {
                    SetRewardComplete();
                }
            }
            else if (optionBuy == OptionReward.coin)
            {
                if (ViewReward.AddCoin(-_money))
                {
                    SetRewardComplete();
                }
            }
        }

    }
}