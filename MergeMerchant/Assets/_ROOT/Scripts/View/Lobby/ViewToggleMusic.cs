using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant.Lobby
{
    public class ViewToggleMusic : MonoBehaviour
    {
        [SerializeField] GameObject musicDisable;
        [SerializeField] Toggle toggle;

        private void OnEnable()
        {
            OnToggleMusic();
        }
        public void OnToggleMusic()
        {
            musicDisable.SetActive(toggle.isOn);
        }
    }
}
