using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public static class ConfigAudio
    {
        public static void PlayAudio(AudioSource kSource, bool _isLoop)
        {
            kSource.loop = _isLoop;
            kSource.Play();
        }
    }
}
