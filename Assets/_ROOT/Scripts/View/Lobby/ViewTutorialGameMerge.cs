using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.Merge
{
    public class ViewTutorialGameMerge : MonoBehaviour
    {
        [SerializeField] GameObject basket;
        [SerializeField] GameObject merge;
        private void OnEnable()
        {
            merge.SetActive(ViewTutorials.IsStartGame == 1);
            ViewTutorials.eventTutorialGameMerge += NextTutorials;
            ViewTutorials.eventTutorialComplete += TutorialComplete;
        }
        private void OnDisable()
        {
            ViewTutorials.eventTutorialGameMerge -= NextTutorials;
            ViewTutorials.eventTutorialComplete -= TutorialComplete;
        }

        public void TutorialComplete()
        {
            basket.SetActive(false);
        }

        public void NextTutorials()
        {
            ViewTutorials.IsStartGame = 0;
            merge.SetActive(false);
            basket.SetActive(true);
        }
    }
}