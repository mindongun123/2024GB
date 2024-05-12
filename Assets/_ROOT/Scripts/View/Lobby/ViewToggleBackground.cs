using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant.Lobby
{

    public class ViewToggleBackground : MonoBehaviour
    {
        [SerializeField] GameObject backgroundLight;
        [SerializeField] GameObject viewDay;
        [SerializeField] Toggle toggle;
        public void OnToggleMusic()
        {
            backgroundLight.SetActive(toggle.isOn);
            viewDay.SetActive(toggle.isOn);
        }

    }
}
