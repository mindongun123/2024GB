using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant
{
    public class EventController : MonoBehaviour
    {
        [SerializeField]
        EVENT eventGame;

        [SerializeField] GameObject menuEvent;

        public int DayEvent
        {
            get => PlayerPrefs.GetInt(ConstGame.DAY_EVENT, 0);
            set => PlayerPrefs.SetInt(ConstGame.DAY_EVENT, value);
        }
        private void Awake()
        {
            CheckEventGame();
        }
        public void CheckEventGame()
        {

            Vector2Int today = ConfigTime.GetDateToday();

            foreach (var item in eventGame.date)
            {
                Vector2Int day = item;
                if (day == today)
                {
                    menuEvent.SetActive(true);
                    break;
                }
            }
        }
    }
}
