using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public static class ConfigTime
    {
        public static TimeSpan ToTimeSpan(string a, string b)
        {
            DateTime dateTime1 = DateTime.ParseExact(a, "dd/MM/yyyy h:mm:ss tt", null);
            DateTime dateTime2 = DateTime.ParseExact(b, "dd/MM/yyyy h:mm:ss tt", null);

            TimeSpan timeDifference = dateTime2 - dateTime1;
            return timeDifference;
        }

        public static bool CheckAreDatesDifferent(string a, string b)
        {
            DateTime dateTime1 = DateTime.ParseExact(a, "dd/MM/yyyy h:mm:ss tt", null);
            DateTime dateTime2 = DateTime.ParseExact(b, "dd/MM/yyyy h:mm:ss tt", null);


            if (dateTime1.Date != dateTime2.Date)
            {
                return true;
            }
            return false;
        }

        public static int MonthEvent()
        {
            DateTime now = DateTime.Now;
            int _idxEvent = 0;
            switch (now.Month)
            {
                case 9:
                    _idxEvent = 1;
                    break;
                case 10:
                    _idxEvent = 2;
                    break;
                case 11:
                case 12:
                    _idxEvent = 3;
                    break;
                default:
                    _idxEvent = 0;
                    break;
            }
            return _idxEvent;
        }


        public static string ConvertTime(int _time)
        {
            int days = _time / 86400;
            _time %= 86400;
            int hours = _time / 3600;
            int minutes = (_time % 3600) / 60;
            int seconds = _time % 60;

            return $"{days}n {hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public static Vector2Int GetDateToday()
        {
            DateTime today = DateTime.Now;
            return new Vector2Int(today.Day, today.Month);
        }

        public static int timeEvent;
    }
}