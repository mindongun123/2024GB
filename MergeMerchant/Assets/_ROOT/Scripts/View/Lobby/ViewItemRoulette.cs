using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{
    public enum NameRoulette
    {
        normal, vip
    }
    public class ViewItemRoulette : MonoBehaviour
    {
        public int _reward;
        public NameItem nameItem;
        [SerializeField] TextMeshProUGUI txtReward;

        [SerializeField] Sprite[] sprites;
        [SerializeField] Image img;


        private void OnEnable()
        {
            SetRewardRoulette();
        }


        private void SetRewardRoulette()
        {
            int _idx = Random.Range(0, 3);
            img.sprite = sprites[_idx];
            nameItem = (NameItem)_idx;
            _reward = Random.Range(0, 30);
            txtReward.text = _reward.ToString();
        }
    }
}