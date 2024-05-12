using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EasyTransition;
using MJGame.MergeMerchant.Merge;
using UnityEngine;


namespace MJGame.MergeMerchant.Firebase
{

    public class CheckHasAccount : MonoBehaviour
    {

        [SerializeField] GameObject viewLogin;
        [SerializeField] DemoLoadScene demoLoadScene;
        [SerializeField] GameObject canvas;
        [SerializeField] CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup.DOFade(0, Random.Range(3, 10)).OnComplete(() =>
            {
                canvas.SetActive(false);
                if (PlayerPrefs.GetString(ConstGame.EMAIL, "").Length > 0)
                {
                    demoLoadScene.LoadScene("Lobby");
                }
                else
                {
                    viewLogin.SetActive(true);
                }
            });
        }
    }
}