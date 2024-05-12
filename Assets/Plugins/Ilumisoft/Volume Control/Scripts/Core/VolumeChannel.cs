namespace Ilumisoft.VolumeControl.Core
{
    using UnityEngine;
    using UnityEngine.Audio;

    [System.Serializable]
    public class VolumeChannel
    {
        [System.NonSerialized]
        private AudioMixer audioMixer;

        private readonly string exposedParameterName;
        private readonly string key;

        [SerializeField]
        private bool isMuted = false;

        [SerializeField]
        private float volume = 1.0f;

        /// <summary>
        /// Gets or sets the muting
        /// </summary>
        public bool IsMuted
        {
            get => isMuted;
            set
            {
                isMuted = value;
                ApplyMuting(value);
            }
        }

        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        public float Volume
        {
            get => volume;

            set
            {
                volume = value;
                ApplyVolume(value);
            }
        }

        /// <summary>
        /// Reference to the storage used to save the state of the volume channel
        /// </summary>
        Storage Storage { get; set; }

        /// <summary>
        /// Creates a new VolumeChannel instance
        /// </summary>
        /// <param name="audioMixer">The AudioMixer used by Volume Control</param>
        /// <param name="exposedParameterName">The name of the exposed volume parameter</param>
        /// <param name="key">The PlayerPrefs key used to store the settings of the channel</param>
        public VolumeChannel(Storage storage, AudioMixer audioMixer, string exposedParameterName, string key)
        {
            this.Storage = storage;
            this.audioMixer = audioMixer;
            this.exposedParameterName = exposedParameterName;
            this.key = key;
        }

        /// <summary>
        /// Applies the muting to the AudioMixer 
        /// </summary>
        /// <param name="value"></param>
        private void ApplyMuting(bool value)
        {
            isMuted = value;

            if (isMuted)
            {
                ApplyVolume(0.0f);
            }
            else
            {
                ApplyVolume(volume);
            }
        }

        /// <summary>
        /// Applies the given volume to the AudioMixer
        /// </summary>
        /// <param name="volume"></param>
        private void ApplyVolume(float volume)
        {
            //If the channel is muted, volume is set to zero
            if (IsMuted)
            {
                volume = 0.0f;
            }

            //Convert volume to decibel
            float dBValue = Utils.ConvertToDecibel(volume);

            //Set volume
            audioMixer.SetFloat(exposedParameterName, dBValue);
        }

        /// <summary>
        /// Serializes the settings and stores them in the PlayerPrefs
        /// </summary>
        public void SaveState()
        {
            string value = JsonUtility.ToJson(this);

            Storage.SetString(key, value);

            Storage.Save();
        }

        /// <summary>
        /// Loads the settings from the PlayerPrefs
        /// </summary>
        public void LoadState()
        {
            if (Storage.HasKey(key))
            {
                string value = Storage.GetString(key);

                JsonUtility.FromJsonOverwrite(value, this);

                ApplyVolume(Volume);
            }
        }
    }
}