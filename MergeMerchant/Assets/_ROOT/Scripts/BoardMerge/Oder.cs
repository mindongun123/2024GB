using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant
{
    public class Oder : MonoBehaviour
    {

        [SerializeField] GameObject btOk;

        [SerializeField] GameObject[] slots;

        private void OnEnable()
        {
            ButtonOkActive(false);
        }

        public void OnClickOderComplete()
        {
            SingletonComponent<OderController>.Instance.AddQueueOderWait(this);
            gameObject.SetActive(false);
        }

        public void ButtonOkActive(bool _isActive = true)
        {
            btOk.SetActive(_isActive);
        }

        /// <summary>
        /// yeu cau cua khach hang -> san pham can phai lam duoc
        /// </summary>
        /// <param name="_amt">so luong san pham yeu cau</param>
        public void SetupOder(DTOder kDTOder)
        {
            for (int i = 0; i < kDTOder._amount; i++)
            {
                slots[i].SetActive(true);
                slots[i].GetComponent<Image>().sprite = kDTOder.sprites[kDTOder._idTile[i]];
            }
        }

    } 
}