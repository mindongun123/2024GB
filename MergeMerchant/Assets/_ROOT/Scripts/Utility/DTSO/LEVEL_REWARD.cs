using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace MJGame.MergeMerchant.Lobby
{

    public struct LEVEL_REWARD
    {
        public int _coin;
        public int _diamond;
        public bool _iscoin;
        public bool _isdiamond;
        public int _level;
        public bool _complete;

        public void Init()
        {
            _coin = Random.Range(10, 25);
            _diamond = Random.Range(12, 35);
            _iscoin = true;
            _isdiamond = true;
        }
    }
    [Serializable]
    public class LEVEL_DATA_REWARD
    {
        public int _coin;
        public int _diamond;
        public bool _iscoin;
        public bool _isdiamond;
        public int _level;
        public bool _complete;

        public LEVEL_DATA_REWARD(LEVEL_REWARD kLevelReward )
        {
            _coin =kLevelReward._coin;
            _diamond = kLevelReward._diamond;
            _iscoin = true;
            _isdiamond = true;
        }
    }

}