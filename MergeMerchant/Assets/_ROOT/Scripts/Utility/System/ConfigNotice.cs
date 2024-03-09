using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using UnityEngine;
using UnityEngine.Events;

namespace MJGame.MergeMerchant.Lobby
{
    public static class ConfigNotice
    {
        public static UnityAction eventNoticeViewOption;

        #region Notification
        public static void SaveNotifyViewOption(int _value = 1)
        {
            int _noti = GetNotifyViewOption();
            PlayerPrefs.SetInt(ConstGame.NOTICE_VIEW_OPTION, _noti + _value);
            eventNoticeViewOption?.Invoke();
        }

        /// <summary>
        /// Xem co Notify cua View khong ( neu GetNotifyViewOption > 0)
        /// </summary>
        /// <returns></returns>
        public static int GetNotifyViewOption()
        {
            return PlayerPrefs.GetInt(ConstGame.NOTICE_VIEW_OPTION, 3);
        }


        public static UnityAction eventNotifyViewBasket;
        static int[] _default = { -1, 0, 3, 5, 8, 11, 15, 10000 };

        /// <summary>
        ///  co su thay doi cua Level hoac OnClickBuyBasket thi goi ham nay
        /// </summary>
        public static void SaveNotifyViewBasket()
        {
            PlayerPrefs.SetInt(ConstGame.NOTICE_VIEW_BASKET, IsNoticeViewBasket());
            eventNotifyViewBasket?.Invoke();
        }

        /// <summary>
        /// Xem co Notify cua Basket khong
        /// </summary>
        /// <returns></returns>
        public static int GetNotifyViewBasket()
        {
            return PlayerPrefs.GetInt(ConstGame.NOTICE_VIEW_BASKET, 0);
        }

        private static int IsNoticeViewBasket()
        {
            int _idBasketCurrent = PlayerPrefs.GetInt(ConstGame.ID_BASKET, ConstGame.ID_BASKET_DEFAULT) % 10;
            int _level = SingletonComponent<LevelGame>.Instance.Level;
            if (_default[_idBasketCurrent + 1] <= _level)
            {
                return 1;
            }
            return 0;
        }
        #endregion

        #region Notify Button Level

        public static UnityAction eventNotifyViewLevelPass;
        public static void SaveNotifyLevelPassReward()
        {
            int _getNtf = GetNotifyLevelPassReward();
            PlayerPrefs.SetInt(ConstGame.NOTICE_LEVEL_PASS_REWARD, _getNtf - 1);
            eventNotifyViewLevelPass?.Invoke();
        }

        public static int GetNotifyLevelPassReward()
        {
            return PlayerPrefs.GetInt(ConstGame.NOTICE_LEVEL_PASS_REWARD, 2);
        }
        #endregion
    }
}
