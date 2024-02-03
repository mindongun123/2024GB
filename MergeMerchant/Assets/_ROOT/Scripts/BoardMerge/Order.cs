using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant
{
    public class Order : MonoBehaviour
    {

        [SerializeField] GameObject btOk;
        [SerializeField] Sprite[] sprites;
        [SerializeField] GameObject[] slots;

        private void OnEnable()
        {
            ButtonOkActive(false);
        }

        public void OnClickOderComplete()
        {
            SingletonComponent<OrderController>.Instance.AddQueueOderWait(this);
            gameObject.SetActive(false);
        }

        public void ButtonOkActive(bool _isActive = true)
        {
            btOk.SetActive(_isActive);
        }

        /// <summary>
        /// hien thi yeu cau cau khach hang
        /// </summary>
        /// <param name="_amt">so luong san pham yeu cau</param>

        public void ShowSetupOderOnTop(DTOrdrer kDTOder)
        {
            for (int i = 0; i < kDTOder._option; i++)
            {
                slots[i].SetActive(true);
                slots[i].GetComponent<Image>().sprite = sprites[kDTOder._idOptions[i]];
            }
        }
    }
}