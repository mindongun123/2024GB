using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using MJGame;
using MJGame.MergeMerchant.Merge;

namespace GIE
{

    public class Example : MonoBehaviour
    {
        public GetItemEffectType mGetItemEffectType = GetItemEffectType.Explostion_First;
        public string mItemName = "coin";
        public int mItemNumber = 10;
        public Text mItemNumberText;

        public void OnSetNumber(float number)
        {
            mItemNumber = (int)number;
            mItemNumberText.text = "Number:" + mItemNumber.ToString();
        }

        public void OnSetExplostion(bool set_value)
        {
            if (set_value == true) mGetItemEffectType = GetItemEffectType.Explostion_First;
        }

        public void OnSetJump(bool set_value)
        {
            if (set_value == true) mGetItemEffectType = GetItemEffectType.JumpAway_First;
        }

        public void OnSetFly(bool set_value)
        {
            if (set_value == true) mGetItemEffectType = GetItemEffectType.FlyAway;
        }



        public void OnSetCoin(bool set_value)
        {
            if (set_value == true) mItemName = "coin";
        }

        public void OnSetDiamond(bool set_value)
        {
            if (set_value == true) mItemName = "diamond";
        }

        public void OnSetEquipment(bool set_value)
        {
            if (set_value == true) mItemName = "equipment";
        }



        public void OnClickMoney(RectTransform from_where)
        {
            SingletonComponent<GetItemEffect>.Instance.GetItem(mItemName, mItemNumber, from_where, null, mGetItemEffectType);
        }


        public void OnClick3DObject(BaseEventData eventData)
        {
            //Debug.Log("OnClick3DObject:" +  ((PointerEventData)eventData).position);
            Vector2 position = ((PointerEventData)eventData).position;
            SingletonComponent<GetItemEffect>.Instance.GetItem(mItemName, mItemNumber, new Vector3(position.x, position.y, 0), null, mGetItemEffectType);
        }
    }

}

