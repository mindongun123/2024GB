using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;

namespace MJGame.MergeMerchant.Merge
{
    public static class ConstGame
    {
        [Header("Common")]
        public const string LEVEL = "level";
        public const string COIN = "coin";
        public const string DIAMOND = "diamond";
        public const string ENERGY = "energy";

        [Header("Board Merge Game")]
        public const int COLUMN = 7;
        public const int ROW = 9;
        public const string SAVE_BOARD = "save_board";
        public const string BASKET_CURRENT = "pasket_ps";// luu vi tri TileBase chua Basket
        public const string ID_BASKET = "id_basket";// id_basket max
        public const int ID_BASKET_DEFAULT = 61;
        public const string SAVE_ORDER_SLOT = "save_order_slot";


        public const string LIST_ID_OPTION_SPAWN = "list_id_option_spawn";

        [Header("Lobby")]
        public const string DAY_DAILY_OLD = "day_daily_old";
        public const string DAY_DAILY_NEXT = "day_daily_next";
        public const string TIME_COUNT_DOWN = "time_count_down";

        public const string LIST_VIEW_OPTION = "list_view_option";

        public const string LIST_ID_MAX = "list_id_max";
    }
}
