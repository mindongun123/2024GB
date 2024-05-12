using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewItemLevelPass : MonoBehaviour
    {
        public GameObject dis;
        public GameObject buttonGetCoin;
        public GameObject buttonGetDiamond;
        [SerializeField] TextMeshProUGUI txtCoin;
        [SerializeField] TextMeshProUGUI txtDiamond;
        [SerializeField] TextMeshProUGUI txtLevel;
        [SerializeField] LEVEL_DATA_REWARD _levelReward;

        public void SetStart(LEVEL_DATA_REWARD kLevelReward)
        {
            _levelReward = kLevelReward;
            GetButton();
        }
        public void GetButton()
        {
            if (_levelReward._level <= SingletonComponent<LevelGame>.Instance.Level)
            {
                _levelReward._complete = true;
            }
            if (!_levelReward._complete) return;
            dis.SetActive(false);
            buttonGetCoin.SetActive(_levelReward._iscoin);
            buttonGetDiamond.SetActive(_levelReward._isdiamond);
            txtCoin.text = $"+{_levelReward._coin}";
            txtDiamond.text = $"+{_levelReward._diamond}";
            txtLevel.text = $"Level {_levelReward._level}";
        }

        public void OnClickAddCoin()
        {
            // Audio 
            SingletonComponent<AudioController>.Instance.AudioOnClickPlay();
            //
            _levelReward._iscoin = false;
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(buttonGetCoin.transform.position, _levelReward._coin / 2, NameItem.coin);
            ViewReward.AddCoin(_levelReward._coin);

            buttonGetCoin.SetActive(false);

            ConfigNotice.SaveNotifyLevelPassReward();
        }

        public void OnClickAddDiamond()
        {
            // Audio
            SingletonComponent<AudioController>.Instance.AudioOnClickPlay();
            //
            _levelReward._isdiamond = false;
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(buttonGetDiamond.transform.position, _levelReward._diamond / 2, NameItem.diamond);
            ViewReward.AddDiamond(_levelReward._diamond);
            buttonGetDiamond.SetActive(false);
            ConfigNotice.SaveNotifyLevelPassReward();
        }
    }
}
