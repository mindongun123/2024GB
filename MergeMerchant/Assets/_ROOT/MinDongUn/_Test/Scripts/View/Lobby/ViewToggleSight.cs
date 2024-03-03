using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewToggleSight : MonoBehaviour
    {
        [SerializeField] GameObject menuTop;
        [SerializeField] GameObject viewSight;
        [SerializeField] Toggle toggleSight;
        public void OnClickToggle()
        {
            menuTop.SetActive(toggleSight.isOn);
            viewSight.SetActive(!toggleSight.isOn);
        }
    }
}
