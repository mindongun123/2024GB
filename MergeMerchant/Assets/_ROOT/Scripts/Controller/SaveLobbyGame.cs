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

        #region View Item Option   
        public List<Vector4> GetListViewOption()
        {
            return MJGameSave.GetList<Vector4>(ConstGame.LIST_VIEW_OPTION, new List<Vector4>() { new Vector4(1, 1, 1, 5), new Vector4(2, 1, 1, 5), new Vector4(3, 1, 0, 3) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_vt4">x: id, y: complete, z: diamond/coin, w: reward</param>
        public void SetListViewOption(Vector4 _vt4)
        {
            List<Vector4> _ls = GetListViewOption();
            _ls.Add(_vt4);
            MJGameSave.SetList<Vector4>(ConstGame.LIST_VIEW_OPTION, _ls);
        }

        public void ChangeListViewOptionComplete(int x)
        {
            List<Vector4> _ls = GetListViewOption();
            for (int i = 0; i < _ls.Count; i++)
            {
                int _id = (int)_ls[i].x;
                if (_id == x)
                {
                    _ls[i] = new Vector4(x, 0, 0, 0);
                    break;
                }
            }
            MJGameSave.SetList<Vector4>(ConstGame.LIST_VIEW_OPTION, _ls);
        }
        #endregion
    }
}