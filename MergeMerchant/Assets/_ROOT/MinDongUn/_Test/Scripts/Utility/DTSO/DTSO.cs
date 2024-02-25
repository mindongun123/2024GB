using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MJGame.MergeMerchant.Utility
{
    public struct SlotData
    {
        public int _id;
        public int _money;
        public bool _complete;


        [Button]
        public void InitSlot()
        {
            this._complete = false;
            this._id = Random.Range(1, 10);
            this._money = 10 * _id;
        }
    }


    [Serializable]
    public class Slot
    {
        public int _id;
        public int _money;
        public bool _complete;
        
        public Slot(SlotData kSlot)
        {
            _id = kSlot._id;
            _money = kSlot._money;
            _complete = kSlot._complete;
        }


        SlotData slot;
        public SlotData GetSlot()
        {
            slot._id = _id;
            slot._money = _money;
            slot._complete = _complete;
            return slot;
        }
    }
}

