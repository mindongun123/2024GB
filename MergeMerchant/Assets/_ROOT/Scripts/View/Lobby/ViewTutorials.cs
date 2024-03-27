using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using UnityEngine;
using UnityEngine.Events;

namespace MJGame.MergeMerchant
{
    public static class ViewTutorials
    {

        public static UnityAction eventTutorialGameMerge;
        public static UnityAction eventTutorialComplete;
        public static int IsStartGame
        {
            get => PlayerPrefs.GetInt(ConstGame.IS_START, 1);
            set => PlayerPrefs.SetInt(ConstGame.IS_START, value);
        }

    }
}
