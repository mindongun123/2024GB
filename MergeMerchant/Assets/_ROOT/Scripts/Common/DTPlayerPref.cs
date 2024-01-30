using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant
{
    public class DTPlayerPref : SingletonComponent<DTPlayerPref>
    {
        public int OptionCurrent
        {
            set => ES3.Save(StaticGame.OPTION_CURRENT, value < StaticGame.OPTION_TILE ? value : StaticGame.OPTION_TILE);
            get => ES3.Load(StaticGame.OPTION_CURRENT, 0);
        }







#if UNITY_EDITOR

        [Button]
        public void TestSetOptionCurrent()
        {
            OptionCurrent = 100;
        }

        private void OnApplicationQuit()
        {
            ES3.DeleteKey(StaticGame.OPTION_CURRENT);
            ES3.DeleteKey(StaticGame.DATA_BOARD);
        }
    }
#endif
}