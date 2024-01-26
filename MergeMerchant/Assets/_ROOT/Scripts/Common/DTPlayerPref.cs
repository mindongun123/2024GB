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
            set => PlayerPrefs.SetInt(StaticGame.OPTION_CURRENT, value < StaticGame.OPTION_TILE ? value : StaticGame.OPTION_TILE);
            get => PlayerPrefs.GetInt(StaticGame.OPTION_CURRENT, 0);
        }







#if UNITY_EDITOR

        [Button]
        public void TestSetOptionCurrent()
        {
            OptionCurrent = 100;
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.DeleteKey(StaticGame.OPTION_CURRENT);

            PlayerPrefs.DeleteKey(StaticGame.DATA_BOARD);


        }
    }
#endif
}