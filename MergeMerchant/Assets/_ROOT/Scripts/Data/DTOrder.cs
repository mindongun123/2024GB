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
        public int _option;// toi da 2
        public int _coin;// gia tri moi options
        public int [] _idOptions;


        #region Khoi tao Options  -> cho playerboss di mua hang
        private void SetOptions()
        {
            _idOptions = new int[_option];
            for (int i = 0; i < _option; i++)
            {
                _idOptions[i] =  Random.Range(1, StaticGame.OPTIONS_ORDER);
            }
        }

        public DTOrdrer(int _opt)
        {
            _option = _opt;
            SetOptions();
        }

        #endregion
        

    }


}