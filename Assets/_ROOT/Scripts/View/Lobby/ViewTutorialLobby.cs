using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewTutorialLobby : MonoBehaviour
    {
        [SerializeField] GameObject btnReturn;
        [SerializeField] GameObject[] tutorials;
        [SerializeField]
        GameObject playt, profilet;

        int _idx = 0;

        private void OnEnable()
        {
            if (ViewTutorials.IsStartGame == 1)
            {
                profilet.SetActive(true);
                ShowTutorials(0);
            }
        }

        public void ShowTutorials(int _nub)
        {
            tutorials[_idx].SetActive(false);
            _idx = (_idx + _nub + tutorials.Length) % tutorials.Length;
            tutorials[_idx].SetActive(true);
        }

        public void OnClickNextTutorials()
        {
            if (_idx > 3)
            {
                profilet.SetActive(false);
                playt.SetActive(true);
                return;
            }
            if (_idx == 3)
            {
                btnReturn.SetActive(true);
            }
            ShowTutorials(1);
        }

        public void OnClickReturnTutorials()
        {
            tutorials[_idx].SetActive(false);
            _idx = 0;
            tutorials[_idx].SetActive(true);
        }
    }
}
