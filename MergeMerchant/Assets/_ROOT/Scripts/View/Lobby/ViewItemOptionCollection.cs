using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{

    public class ViewItemOptionCollection : MonoBehaviour
    {
        [SerializeField] int _diamond;
        [SerializeField] int _coin;
        [SerializeField] GameObject rewardIconCoin;
        [SerializeField] GameObject rewardIconDiamond;

        [SerializeField] TextMeshProUGUI txtReward;
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] GameObject buttonRewardComplete;
        [SerializeField]
        GameObject open;
        int ID;
        public void SetViewItemCollection(int _id, int _complete, int _idreward, int _reward, Sprite _sprite)
        {
            image.enabled = true;
            ID = _id;
            image.sprite = _sprite;
            txt.text = $"{_id / 10 + 1}-{_id % 10}";
            txt.gameObject.SetActive(true);
            EnableOpenOption();
            EnableButtonComplete(_complete, _idreward, _reward);
            txtReward.text = _reward.ToString();
        }

        public void OnClickAddReward()
        {
            SingletonComponent<SaveLobbyGame>.Instance.ChangeListViewOptionComplete(ID);
            buttonRewardComplete.SetActive(false);
            if (_diamond > 0)
            {
                SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, _diamond, NameItem.diamond);
                ViewReward.AddDiamond(_diamond);
                return;
            }
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, _coin, NameItem.coin);
                ViewReward.AddCoin(_coin);
        }

        public void EnableOpenOption()
        {
            open.SetActive(false);
            return;
        }
        int _rd;
        public void SetReward(int _idreward, int _reward)
        {
            _rd = _idreward;
            if (_rd == 0)
            {
                _diamond = _reward;
                rewardIconCoin.SetActive(false);
                rewardIconDiamond.SetActive(true);
                return;
            }
            _coin = _reward;
            rewardIconCoin.SetActive(true);
            rewardIconDiamond.SetActive(false);
        }


        public void EnableButtonComplete(int _complete, int _idreward, int _reward)
        {
            if (_complete == 1)
            {
                buttonRewardComplete.SetActive(true);

                /// <summary>
                /// Set Data Reward
                /// </summary>
                SetReward(_idreward, _reward);
                return;
            }
            buttonRewardComplete.SetActive(false);
        }
    }

}