using System.Collections;
using System.Collections.Generic;
using MidniteOilSoftware;
using UnityEngine;

namespace MJGame.MergeMerchant
{
    public class AudioItem : DespawnAfterDelay, IDespawnedPoolObject
    {
        [SerializeField] AudioSource audioSource;
        private void OnEnable()
        {
            AudioPlay();
            StartCoroutine(Despawn());
        }

        public void AudioPlay()
        {
            ConfigAudio.PlayAudio(audioSource, false);
        }
        
        IEnumerator Despawn()
        {
            yield return new WaitForSeconds(2);
            ObjectPoolManager.DespawnGameObject(gameObject);
        }

        public void ReturnedToPool()
        {

        }
    }
}