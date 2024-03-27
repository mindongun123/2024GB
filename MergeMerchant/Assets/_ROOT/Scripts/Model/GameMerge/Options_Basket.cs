using System.Collections;
using System.Collections.Generic;
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
            ViewTutorials.eventTutorialComplete?.Invoke();

            // Audio 
            SingletonComponent<AudioController>.Instance.AudioOnClickPlay();
            // 
            if (ViewReward.AddEnergy(-1))
            {
                SingletonComponent<SelectNow>.Instance.SetPositionSelectNow(transform.parent.position);
                SingletonComponent<SpawnOptions>.Instance.CreateNewOptions();
                SingletonComponent<ViewInformationWhenSellect>.Instance.ShowInformationText(_isBasket, ID, this, parentAfterDrag);
                return;
            }
        }

    }
}
