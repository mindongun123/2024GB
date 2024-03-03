using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant.Lobby
{
    public class SaveLobbyGame : SingletonComponent<SaveLobbyGame>
    {
        #region  Date Time
        public string DateTimeDailyOld
        {
            set => PlayerPrefs.SetString(ConstGame.DAY_DAILY_OLD, value);
            get => PlayerPrefs.GetString(ConstGame.DAY_DAILY_OLD, DateTime.MinValue.ToString());
        }
        public void CompleteDaily()
        {
            DayDailyNext = DayDailyNext + 1;
            DateTimeDailyOld = DateTime.Now.ToString();
        }
        public int DayDailyNext
        {
            set => PlayerPrefs.SetInt(ConstGame.DAY_DAILY_NEXT, value);
            get => PlayerPrefs.GetInt(ConstGame.DAY_DAILY_NEXT, 1);
        }


        public int TimeCountDown
        {
            set => PlayerPrefs.SetInt(ConstGame.TIME_COUNT_DOWN, value);
            get => PlayerPrefs.GetInt(ConstGame.TIME_COUNT_DOWN, 0);
        }


        #endregion
    }
}