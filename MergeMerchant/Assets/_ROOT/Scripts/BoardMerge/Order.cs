using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant
{
    public class Order : MonoBehaviour
    {

        [SerializeField] GameObject btOk;
        [SerializeField] GameObject slots;
        [SerializeField] GameObject check;
        [SerializeField] Sprite[] sprites;

        [SerializeField] TextMeshProUGUI txt;



        public void OnClickOrderComplete()
        {
            SingletonComponent<OrderController>.Instance.AddOrderWaitToQueue(this);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// trang thai ban dau
        /// </summary>
        /// <param name="kDTOrder"></param>
        public void SetupOrderSlot(DTOrdrer kDTOrder)
        {
            slots.SetActive(true);
            slots.GetComponent<Image>().sprite = sprites[kDTOrder._idSprite];
            txt.text = kDTOrder._coin.ToString();
        }

        public void CompleteSlot(bool _isCpl)
        {
            check.SetActive(_isCpl);
            btOk.SetActive(_isCpl);
        }

    }
}