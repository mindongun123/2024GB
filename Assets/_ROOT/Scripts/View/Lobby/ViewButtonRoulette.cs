using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewButtonRoulette : MonoBehaviour
    {
        [SerializeField] Button btn;
        [SerializeField] GameObject look;
        private void OnEnable()
        {
            ConfigNotice.eventUnlockRoulette += UnlockRoulette;
            ConfigNotice.eventUnlockRoulette?.Invoke(SingletonComponent<LevelGame>.Instance.Level);
        }

        private void OnDisable()
        {
            ConfigNotice.eventUnlockRoulette -= UnlockRoulette;
        }

        public void UnlockRoulette(int _level)
        {
            if (PlayerPrefs.GetInt(ConstGame.UNLOCK_ROULETTE, 3) > _level) return;
            else if (PlayerPrefs.GetInt(ConstGame.UNLOCK_ROULETTE, 3) == _level)
            {
                // hien panel chỉ chỉ là đã được Unlock, thêm các hiệu ứng nữa
                PlayerPrefs.SetInt(ConstGame.UNLOCK_ROULETTE, 0);
            }
            btn.interactable = true;
            look.SetActive(false);
        }
    }
}
