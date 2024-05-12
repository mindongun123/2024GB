using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.House
{
    public class ViewItemRewardFloor : MonoBehaviour
    {

        int _id;
        int _reward = 500;
        const int REWARD_DEFAULT = 500;

        [SerializeField] Image img;

        [SerializeField] Image avatar;
        [SerializeField] Sprite[] sprites;

        public void SetRewardFloor(int _id, string _timeOld)
        {
            _reward = REWARD_DEFAULT * (_id + 1);
            this._id = _id;
            avatar.sprite = sprites[_id];
            TimeSpan timeSpan = ConfigTime.ToTimeSpan(_timeOld, DateTime.Now.ToString());
            int _time = 1200 - (int)timeSpan.TotalSeconds;
            _time = _time > 0 ? _time : 0;

            StartCoroutine(CountDownIE(_time));
        }

        [SerializeField] GameObject btn;
        IEnumerator CountDownIE(int _time)
        {
            img.fillAmount = 1 - (float)_time / 10;
            if (_time <= 0)
            {
                btn.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(1);
                _time--;
                StartCoroutine(CountDownIE(_time));
            }
        }

        public void OnClickAddRewardFloor()
        {
            SingletonComponent<ViewItemRewardFloorController>.Instance.ChangeListItemRewardFloor(_id);
            btn.SetActive(false);
            ViewReward.AddCoin(_reward);
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, 15, NameItem.coin);
        }
    }
}
