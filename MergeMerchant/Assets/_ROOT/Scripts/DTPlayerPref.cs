using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant
{
    public class DTPlayerPref : SingletonComponent<DTPlayerPref>
    {
        public int TileCurrent
        {
            set => PlayerPrefs.SetInt(StaticGame.TILE_CURRENT, value < StaticGame.TILE_MAX ? value : StaticGame.TILE_MAX);
            get => PlayerPrefs.GetInt(StaticGame.TILE_CURRENT, 4);
        }





#if UNITY_EDITOR 
        private void OnApplicationQuit()
        {
            PlayerPrefs.DeleteKey(StaticGame.TILE_CURRENT);
        }
    }
#endif
}
