using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.System
{

    public class ButtonQuitApp : MonoBehaviour
    {
        public void QuitApp()
        {
#if UNITY_EDITOR
            print("Click");
            UnityEditor.EditorApplication.isPlaying = false;
#else
    print("Click");
    Application.Quit();
#endif
        }

    }
}