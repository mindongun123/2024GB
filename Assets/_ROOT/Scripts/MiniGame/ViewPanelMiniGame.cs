#if UNITY_FIREBASE

using System.Collections;
using Ilumisoft.VisualStateMachine;
using MJGame.MergeMerchant.Firebase;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.MiniGame
{
    public class ViewPanelMiniGame : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt;
        [SerializeField] TextMeshProUGUI txtAdd;
        [SerializeField] GameObject btn;
        public GameObject panel;

        public void Win()
        {
            txt.text = "<color=white>YOU WIN</color>";
            int _idx =  SingletonComponent<MiniGameController>.Instance.ScoreWinminiGame();
            ShowCoin(_idx);
        }

        public void Lose()
        {
            txt.text = "<color=red>YOU LOSE</color>";
            int _idx = SingletonComponent<MiniGameController>.Instance.ScoreLoseMiniGame();
            ShowCoin(_idx);
        }
        public void ShowCoin(int _idx)
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(3f);
                txtAdd.text = _idx > 0 ? $"+{_idx}" : $"{_idx}";
                if (_idx > 0)
                {
                    txtAdd.text = $"+{_idx}";
                    SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(txtAdd.transform.position, ConfigTime.timeEvent * 10 * SingletonComponent<MiniGameController>.Instance._health);
                }
                else
                {
                    txtAdd.text = $"{_idx}";
                }
                ViewReward.AddCoin(_idx);
                ShowButton();
            }
        }

      
        private void ShowButton()
        {
            btn.SetActive(true);
        }
    }
}

#endif