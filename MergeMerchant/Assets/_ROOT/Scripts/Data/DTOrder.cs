using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MJGame.MergeMerchant
{

    [Serializable]
    public class DTOrdrer
    {
        public int _coin;// gia tri moi options
        public int _idSprite;// loai san pham
        public bool _isComplete;

        #region Khoi tao Options  -> cho playerboss di mua hang
        private void SetOptions()
        {
            _idSprite = Random.Range(1, StaticGame.OPTIONS_ORDER);
            _coin = 100;
        }

        public DTOrdrer()
        {
            SetOptions();
        }


        #endregion


    }


}