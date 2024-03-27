using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public class AudioController : SingletonComponent<AudioSource>
    {
        [SerializeField] AudioSource audioUpdateLevel;
        [SerializeField] AudioSource audioReward;
        [SerializeField] AudioSource audioOnClick;
        [SerializeField] AudioSource audioOnClickBasket;
        [SerializeField] AudioSource audioDragOption;
        [SerializeField] AudioSource audioCompleteProduct;

        public void AudioUpdateLevelPlay()
        {
            ConfigAudio.PlayAudio(audioUpdateLevel, false);
        }

        public void AudioRewardPlay()
        {
            ConfigAudio.PlayAudio(audioReward, false);
        }
        public void AudioOnClickPlay()
        {
            ConfigAudio.PlayAudio(audioOnClick, false);
        }

        public void AudioOnClickBasketPlay()
        {
            ConfigAudio.PlayAudio(audioOnClickBasket, false);
        }
        public void AudioDragMergeOptiosPlay()
        {
            ConfigAudio.PlayAudio(audioDragOption, false);
        }

        public void AudioCompleteProductPlay()
        {
            ConfigAudio.PlayAudio(audioCompleteProduct, false);
        }
    }
}

