using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using UnityEngine;

namespace MJGame.MergeMerchant
{

    public class Tutorials : MonoBehaviour
    {
        [SerializeField] GameObject tutorialsBook;
        private void OnEnable()
        {
            int _isBook = PlayerPrefs.GetInt(ConstGame.TUTORIAL_ORDER, 0);
            if (_isBook == 1)
            {
                tutorialsBook.SetActive(true);
            }
        }

        public void OnClickPlay()
        {
            int _isBook = PlayerPrefs.GetInt(ConstGame.TUTORIAL_ORDER, 0);
            if (_isBook > 0) return;
            PlayerPrefs.SetInt(ConstGame.TUTORIAL_ORDER, 1);
        }

        public void OnClickBook()
        {
            PlayerPrefs.SetInt(ConstGame.TUTORIAL_ORDER, 2);
        }

    }
}