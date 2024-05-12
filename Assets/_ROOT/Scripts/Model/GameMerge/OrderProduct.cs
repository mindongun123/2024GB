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


        public void SetSlotOrder(Slot kSlot)
        {
            slot = kSlot;
            ViewSlotOrder();
        }

        private void ViewSlotOrder()
        {
            imgSlot.sprite = sprites[slot._id];
            txtMoneySlot.text = slot._coin.ToString();

            ButtonComplete();
            SetEnableInteractableButtonComplete();
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


        private void SetEnableInteractableButtonComplete(bool _istrue = true)
        {
            m_btnComplete.GetComponent<Button>().interactable = _istrue;
        }

        public void OnClickComplete()
        {
            //Audio 
            SingletonComponent<AudioController>.Instance.AudioOnClickPlay();
            SingletonComponent<AudioController>.Instance.AudioCompleteProductPlay();
            //

            SetEnableInteractableButtonComplete(false);

            //Add Reward Coin sau khi ban xong
            StartCoroutine(AddRewardIE(1f, slot._coin));

            SingletonComponent<OrderController>.Instance.DeleteOrderProductFromDictionary(this, slot._id);

            SingletonComponent<SaveGameMerge>.Instance.UpdateNumberID(slot._id, -1);

        }

        IEnumerator AddRewardIE(float _time, int _value)
        {
            yield return new WaitForSeconds(_time);
            AddReward(_value);
        }
        private void AddReward(int _value)
        {
            ViewReward.AddCoin(_value);
        }

    }


}
