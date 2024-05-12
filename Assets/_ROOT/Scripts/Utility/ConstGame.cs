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
        public const string LIST_ID_OPTION_SPAWN = "list_id_option_spawn";

        public const string NUMBER_ORDER_PRODUCT = "number_order_product";

        public const string INDEX_SEAL_OPEN = "index_seal_open";

        [Header("Lobby")]
        public const string DAY_DAILY_OLD = "day_daily_old";
        public const string DAY_DAILY_NEXT = "day_daily_next";
        public const string TIME_COUNT_DOWN = "time_count_down";
        public const string TIME_DAILY_ENERGY = "time_daily_energy";

        public const string LIST_VIEW_OPTION = "list_view_option";

        public const string LIST_ID_MAX = "list_id_max";

        public const string LIST_LEVEL_PASS_REWARD = "list_level_pass_reward";

        public const string EXP_CURRENT = "exp_current";
        public const float EXP_LEVEL = 15;

        [Header("Notify")]
        public const string NOTICE_LEVEL_PASS_REWARD = "notice_level_pass_reward";
        public const string NOTICE_VIEW_OPTION = "notice_view_option";
        public const string NOTICE_VIEW_BASKET = "notice_view_basket";

        [Header("Decor")]
        public const string ITEM_DECOR_HOUSE = "id_decor_house";
        public const string ITEM_DECOR_BUY = "id_decor_buy";
        public const string TIME_ENERGY_REMAINING = "time_energy_remaining";
        public const int TIME_DEFAULT = 10800;

        public const string LIST_ITEM_REWARD_FLOOR = "list_time_reward_floor";

        public const string NUMBER_SPIN = "number_spin";
        public const string TIME_SPIN_FREE = "time_spin_free";

        public const string UNLOCK_ROULETTE = "unlock_roulette";

        [Header("Charactor")]
        public const string LIST_CUSTOMER = "list_customer";
        public const string LIST_SLOT_COMPLETE = "list_slot_complete";

        [Header("Tutorial")]
        public const string IS_START = "is_start";

        public const string TUTORIAL_ORDER = "tutorials-book";

        [Header("Account")]
        public const string EMAIL = "email";

        [Header("Event")]
        public const string DAY_EVENT = "day_event";
    }
}
