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

        [SerializeField] Image img;

        public void SetRewardFloor(int _id, string _timeOld)
        {
            _reward = _reward * (_id + 1);
            this._id = _id;
            TimeSpan timeSpan = ConfigTime.ToTimeSpan(_timeOld, DateTime.Now.ToString());
            // int _time = 86399 - (int)timeSpan.TotalSeconds;
            int _time = 10 - (int)timeSpan.TotalSeconds;
            _time = _time > 0 ? _time : 0;

            StartCoroutine(CountDown(_time));
        }

        [SerializeField] GameObject btn;
        IEnumerator CountDown(int _time)
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
                StartCoroutine(CountDown(_time));
            }
        }

        public void OnClickAddRewardFloor()
        {
            SingletonComponent<ViewItemRewardFloorController>.Instance.ChangeListItemRewardFloor(_id);
            btn.SetActive(false);
            ViewReward.AddCoin(_reward);
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, 20, NameItem.coin);
        }
    }
}
