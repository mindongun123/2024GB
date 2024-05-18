// #if UNITY_FIREBASE

using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Lobby;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.MiniGame
{
    public class ViewShowTime : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] ViewPanelMiniGame viewPanelMiniGame;

        public void SetTimeStart(int _time)
        {
            txt.text = $"{_time} s";
            ConfigTime.timeEvent = _time;
        }

        public void TextTime(int _time)
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                ConfigTime.timeEvent--;
                yield return new WaitForSeconds(1);

                if (ConfigTime.timeEvent <= 0)
                {
                    viewPanelMiniGame.Lose();
                }
                else if (SingletonComponent<MiniGameController>.Instance.miniGameStatus == MiniGameStatus.play)
                {
                    txt.text = $"{ConfigTime.timeEvent} s";
                    StartCoroutine(Delay());
                }
            }
        }
    }
}
// #endif