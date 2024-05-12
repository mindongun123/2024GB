using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GIE
{

    public class Example_3D : MonoBehaviour
    {
        public string mItemName = "coin";
        public int mItemNumber = 10;
        public Text mItemNumberText;
        public Animator mChestAni;
        public void OnSetNumber(float number)
        {
            mItemNumber = (int)number;
            mItemNumberText.text = "Number:" + mItemNumber.ToString();
        }

        public void OnClick3DObject(BaseEventData eventData)
        {
            GetItemEffect_3D.mInstance.GetItem(mItemName, mItemNumber, mChestAni.transform.position, null);
            mChestAni.SetTrigger("Open");
        }
    }
}