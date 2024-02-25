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
        
        [Header("Board Merge Game")]
        public const int COLUMN = 7;
        public const int ROW = 9;
        public const string SAVE_BOARD = "save_board";
        public const string BASKET_CURRENT = "pasket_ps";// luu vi tri TileBase chua Basket
        public const string ID_BASKET = "id_basket";// id_basket max
        public const int ID_BASKET_DEFAULT = 61;

        public const string SAVE_ORDER_SLOT = "save_order_slot";

    }
}
