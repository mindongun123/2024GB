using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public class Oder : MonoBehaviour
    {

        [SerializeField] GameObject btOk;

        private void OnEnable()
        {
            ButtonOkActive(false);
        }

        public void OnClickOderComplete()
        {
            SingletonComponent<OderController>.Instance.AddQueue(this);
            gameObject.SetActive(false);
        }

        public void ButtonOkActive(bool _isActive = true)
        {
            btOk.SetActive(_isActive);
        }

    }
}