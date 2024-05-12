using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Lobby;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace MJGame.MergeMerchant.Merge
{
    public class ButtonBuild : MonoBehaviour
    {
        int _coinBuyDecor = 250;
        [SerializeField] GameObject build;
        [SerializeField] Button btn;
        private void Awake()
        {
            int _level = SingletonComponent<LevelGame>.Instance.Level;
            _coinBuyDecor = _level * 250;
        }
        private void OnEnable()
        {
            ConfigNotice.eventNoticeBuild += IsBuild;
            ConfigNotice.eventNoticeBuild?.Invoke();
        }
        private void OnDisable()
        {
            ConfigNotice.eventNoticeBuild -= IsBuild;
        }

        public void IsBuild()
        {
            bool _is = ViewReward.Coin >= _coinBuyDecor;
            build.SetActive(_is);
            btn.interactable = _is;
        }
    }
}