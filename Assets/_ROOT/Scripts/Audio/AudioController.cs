using System.Collections;
using System.Collections.Generic;
using MidniteOilSoftware;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public class AudioController : SingletonComponent<AudioSource>
    {

        [SerializeField] GameObject audioLevel;
        [SerializeField] GameObject audioReward;
        [SerializeField] GameObject audioClick;
        [SerializeField] GameObject audioClickBasket;
        [SerializeField] GameObject audioMerge;
        [SerializeField] GameObject audioClickCompleteProduct;
        [SerializeField] GameObject audioText;


        #region  INSTANCE AUDIO


        public void AudioUpdateLevelPlay()
        {
            CreateAudio(audioLevel);
        }


        public void AudioRewardPlay()
        {
            CreateAudio(audioReward);
        }
        public void AudioOnClickPlay()
        {
            CreateAudio(audioClick);
        }

        public void AudioOnClickBasketPlay()
        {
            CreateAudio(audioClickBasket);
        }
        public void AudioDragMergeOptiosPlay()
        {
            CreateAudio(audioMerge);
        }

        public void AudioCompleteProductPlay()
        {
            CreateAudio(audioClickCompleteProduct);
        }

        public void AudioSpawnText()
        {
            CreateAudio(audioText);
        }


        public void CreateAudio(GameObject prefabs)
        {
            GameObject obj = ObjectPoolManager.SpawnGameObject(prefabs);
            // obj.transform.parent = transform;
            // obj.gameObject.SetActive(true);
        }
    }

    #endregion
}

