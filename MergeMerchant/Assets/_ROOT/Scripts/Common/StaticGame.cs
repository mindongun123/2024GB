using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public static class StaticGame
    {

        [Header("Set ID Tile")]
        public const string OPTION_CURRENT = "option_current";
        public const int TILE_MAX = 61;
        public const int TILE_MIN = 1;
        public const int TILE_DEFAULT = 0;
        public const int OPTION_TILE = 5;// 0 den 5

        [Header("Load Game")]
        public const string DATA_BOARD = "data_board";
        public const string QUEUE_TILE_DEFAULT = "queue_tile_default";

    }
}
