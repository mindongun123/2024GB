using System;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MJGame.MergeMerchant.Utility
{
    public struct SlotData
    {
        public int _id;
        public int _coin;
        public bool _complete;


        [Button]
        public void InitSlot()
        {
            this._complete = false;
            int _idBasketCurrent = PlayerPrefs.GetInt(ConstGame.ID_BASKET, ConstGame.ID_BASKET_DEFAULT) % 10;
            this._id = Random.Range(1, _idBasketCurrent * 10);// chinh sua lai phu hop voi ID Option toi da co the co
            this._coin = 10 * _id;
        }
    }


    [Serializable]
    public class Slot
    {
        public int _id;
        public int _coin;//coin, diamond
        public bool _complete;


        public Slot(SlotData kSlot)
        {
            _id = kSlot._id;
            _coin = kSlot._coin;
            _complete = kSlot._complete;
        }


        SlotData slot;
        public SlotData GetSlot()
        {
            slot._id = _id;
            slot._coin = _coin;
            slot._complete = _complete;
            return slot;
        }
    }
}

