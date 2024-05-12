using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant.Lobby
{
    public class ViewBoardDaily : MonoBehaviour
    {
        [SerializeField] SaveLobbyGame saveLobbyGame;
        [SerializeField] ViewDailyItem[] dailyItem;
        private void OnEnable()
        {
            CheckDailyToday();
        }

        private void CheckDailyToday()
        {
            string dateDailyOld = saveLobbyGame.DateTimeDailyOld;
            string now = DateTime.Now.ToString();
            bool areDatesDifferent = ConfigTime.CheckAreDatesDifferent(dateDailyOld, now);

            if (saveLobbyGame.DayDailyNext == 8 && areDatesDifferent)
            {
                saveLobbyGame.DayDailyNext = 1;
            }

            int _idx = saveLobbyGame.DayDailyNext - 1;

            for (int i = 0; i < dailyItem.Length; i++)
            {
                dailyItem[i].TextWaitDay(i - _idx);

                if (i < _idx)
                {
                    dailyItem[i].EnableGetDaily(true);
                }
                else if (i == _idx && areDatesDifferent)
                {
                    dailyItem[_idx].EnableCompleteDaily();
                    /// Co thong bao voi Button Daily (cham do)
                }
            }
        }
    }
}