using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant.Merge
{
    public partial class Options
    {
        public Button basketButton;

        [SerializeField]
        private bool _isBasket = false;
        private int IDBasket; // 1-> 8
        public void OnClickBasket()
        {
            if (ViewReward.AddEnergy(-1))
            {
                SingletonComponent<SelectNow>.Instance.SetPositionSelectNow(transform.parent.position);
                SingletonComponent<SpawnOptions>.Instance.CreateNewOptions();
                return;
            }
        }

    }
}
