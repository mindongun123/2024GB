using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewBasket : MonoBehaviour
    {
        [SerializeField] GameObject blank;
        [SerializeField] GameObject buy;
        [SerializeField] GameObject itemSelect;
        [SerializeField] TextMeshProUGUI txt;
        int _diamond;

        public void SetupViewBasket()
        {
            blank.SetActive(false);
            SetSelect(false);
        }

        public void SetBuy(int _level)
        {
            blank.SetActive(false);
            buy.SetActive(true);
            _diamond = (int)Random.Range(10f, 30f) * _level;
            txt.text = _diamond.ToString();
        }

        public void SetSelect(bool _select)
        {
            itemSelect.SetActive(_select);
        }

        public void OnClickUpdateBasket()
        {
            SingletonComponent<AudioController>.Instance.AudioOnClickPlay();
            if (ViewReward.AddDiamond(-_diamond))
            {
                buy.SetActive(false);
                SetSelect(false);
                SingletonComponent<SaveLobbyGame>.Instance.UpdateBasket();
                ConfigNotice.SaveNotifyViewBasket();
                SingletonComponent<SaveLobbyGame>.Instance.SetUpdateViewOptionWhenUpdateBasket();
            }
        }
    }
}
