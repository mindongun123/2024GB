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
        GameObject play, profile, product;

        int _idx = 0;

        private void OnEnable()
        {
            ShowTutorialCurrent(ViewTutorials.IsStartGame);
        }

        private void ShowItemTutorialsProfile(int _nub)
        {
            tutorials[_idx].SetActive(false);
            _idx = (_idx + _nub + tutorials.Length) % tutorials.Length;
            tutorials[_idx].SetActive(true);
        }

        public void OnClickNextTutorials()
        {
            if (_idx > 3)
            {
                ViewTutorials.IsStartGame = 2;
                ShowTutorialCurrent(ViewTutorials.IsStartGame);
                return;
            }
            if (_idx == 4)
            {
                btnReturn.SetActive(true);
            }
            ShowItemTutorialsProfile(1);
        }

        public void OnClickReturnTutorials()
        {
            tutorials[_idx].SetActive(false);
            _idx = 0;
            tutorials[_idx].SetActive(true);
        }

        /// <summary>
        /// 1-> Lobby -> Profile demo -> 3
        /// 2-> Play
        /// 3-> Book
        /// </summary>
        /// 

        public void OnClickTutorialProduct()
        {
            ViewTutorials.IsStartGame = 4;
        }
        public void ShowTutorialCurrent(int _tutorials)
        {
            switch (_tutorials)
            {
                case 1:
                    profile.SetActive(true);
                    ShowItemTutorialsProfile(0);
                    break;
                case 2:
                    play.SetActive(true);
                    profile.SetActive(false);
                    break;
                case 3:
                    product.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
