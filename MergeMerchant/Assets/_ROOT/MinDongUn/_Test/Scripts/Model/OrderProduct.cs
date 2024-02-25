using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using MJGame.MergeMerchant.Utility;

namespace MJGame.MergeMerchant.Merge
{
    public class OrderProduct : MonoBehaviour
    {
        [SerializeField] Sprite[] sprites;

        [ShowInInspector]
        public Slot slot;
        [SerializeField] TextMeshProUGUI txtMoneySlot;
        [SerializeField] Image imgSlot;
        [SerializeField] GameObject m_btnComplete;
        // [SerializeField] Button m_btn;


        public void SetSlotOrder(Slot kSlot)
        {
            slot = kSlot;
            ViewSlotOrder();
        }

        private void ViewSlotOrder()
        {
            imgSlot.sprite = sprites[slot._id];
            txtMoneySlot.text = slot._money.ToString();
            ButtonComplete();
            print("Order Product Complete");
        }

        public void Complete(bool _isTrue = true)
        {
            slot._complete = _isTrue;
            ButtonComplete();
        }
        private void ButtonComplete()
        {
            m_btnComplete.SetActive(slot._complete);
        }

        public void OnClickComplete()
        {
            // Xu li them tien, thuong, gia ca o day
            SingletonComponent<OrderController>.Instance.DeleteOrderProductFromDictionary(this);
            print("complete slot order");
        }

    }


}
