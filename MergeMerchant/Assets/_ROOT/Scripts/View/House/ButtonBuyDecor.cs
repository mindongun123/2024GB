using System.Collections;
using System.Collections.Generic;
using Ilumisoft.VisualStateMachine;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.House
{
    public class ButtonBuyDecor : MonoBehaviour
    {
        ViewItemDecorHouse itemDecorHouse;
        [SerializeField] TextMeshProUGUI txt;
        int _coin = 500;
        public void OnClickButtonBuy()
        {
            BuyDecor();
        }

        private void OnEnable()
        {
            NumberCoin();
        }

        private void NumberCoin()
        {
            int _levelGame = SingletonComponent<LevelGame>.Instance.Level;
            _coin = _levelGame * 250;
            txt.text = _coin.ToString();
        }

        private void BuyDecor()
        {
            if (ViewReward.AddCoin(-_coin))
            {
                itemDecorHouse = transform.parent.GetComponent<ViewItemDecorHouse>();
                itemDecorHouse.ScaleItemDecor(Vector3.one);
                gameObject.SetActive(false);
                SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, 10, NameItem.exp);
                SingletonComponent<ButtonLevel>.Instance.AddExp(1);
            }
            else
            {
                SingletonComponent<ViewItemDecorHouseController>.Instance.OpenViewGoldPlay();
            }
        }
    }
}