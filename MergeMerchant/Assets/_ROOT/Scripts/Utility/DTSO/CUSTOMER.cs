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
        public Vector3 _pos;
        public int _idx;
        public CustomerStatus customerStatus;
        public Slot slot;
        public string _time;

        public CUSTOMER(int _idx, Vector3 _pos)
        {
            SetSlot();
            SetTime();
            this._pos = _pos;
            this._idx = _idx;
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