using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Charactor;
using MJGame.MergeMerchant.Utility;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    [Serializable]
    public class CUSTOMER
    {
        public Vector3 _pos = new Vector3(700f, -227f);
        public int _idx;
        public CustomerStatus customerStatus;
        public Slot slot;
        public string _time;

        public CUSTOMER(int _idx)
        {
            SetSlot();
            SetTime();
            this._idx = _idx;
            this._pos = new Vector3(700f, -276f);
        }

        public void SetSlot()
        {
            SlotData sl = new SlotData();
            sl.InitSlot();
            slot = new Slot(sl);
        }

        public void SetTime()
        {
            _time = DateTime.Now.ToString();
        }
    }
}