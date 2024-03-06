using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewBasketController : SingletonComponent<ViewBasketController>
    {
        [SerializeField] ViewBasket[] viewBaskets;
        int[] _default = { -1, 0, 3, 5, 8, 11, 15 };

        private void OnEnable()
        {
            ConfigNotice.eventNotifyViewBasket += OnActionSetupViewBoardBaket;
            ConfigNotice.eventNotifyViewBasket?.Invoke();
        }

        private void OnDisable()
        {
            ConfigNotice.eventNotifyViewBasket -= OnActionSetupViewBoardBaket;
        }

        private void OnActionSetupViewBoardBaket()
        {
            int IdBasket = PlayerPrefs.GetInt(ConstGame.ID_BASKET, ConstGame.ID_BASKET_DEFAULT) % 10;
            for (int i = 0; i < IdBasket; i++)
            {
                viewBaskets[i].SetupViewBasket();
            }
            viewBaskets[IdBasket - 1].SetSelect(true);
            
            if (IdBasket >= 6) return;
            if (ConfigNotice.GetNotifyViewBasket() == 1)
            {
                viewBaskets[IdBasket].SetBuy(SingletonComponent<LevelGame>.Instance.Level);
            }
        }
    }
}
