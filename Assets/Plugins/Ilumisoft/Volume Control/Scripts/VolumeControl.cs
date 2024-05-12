namespace Ilumisoft.VolumeControl
{
    using Ilumisoft.VolumeControl.Core;
    using UnityEngine;
    using UnityEngine.Audio;

    [RequireComponent(typeof(Storage))]
    public class VolumeControl : SingletonBehaviour<VolumeControl>
    {
        [SerializeField] private AudioMixer audioMixer = null;

        private VolumeChannel master, music, sfx;

        /// <summary>
        /// Gets the AudioMixer used by Volume Control
        /// </summary>
        public static AudioMixer AudioMixer
        {
            get => Instance.audioMixer;
        }

        /// <summary>
        /// Gets the master channel, allowing to control the overall volume of the game
        /// </summary>
        public static VolumeChannel Master
        {
            get => Instance.master;
        }

        /// <summary>
        /// Gets the music channel, allowing to control the volume of the music
        /// </summary>
        public static VolumeChannel Music
        {
            get => Instance.music;
        }

        /// <summary>
        /// Gets the SFX channel, allowing to control the volume of the soundeffects
        /// </summary>
        public static VolumeChannel SFX
        {
            get => Instance.sfx;
        }

        /// <summary>
        /// Reference to the storage used to save the state of the volume channels
        /// </summary>
        Storage Storage { get; set; }

        /// <summary>
        /// Automatically initializes Volume Control just before the first scene is loaded
        /// </summary>
        [RuntimeInitializeOnLoadMethod(loadType: RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var configuration = Configuration.Find();

            //Load the VolumeControl prefab from the "Resources" folder
            var prefab = configuration.VolumeControlPrefab;

            //Instantiate it and make it persistent
            if (prefab != null)
            {
                var instance = Instantiate(prefab);

                instance.name = prefab.name;

                DontDestroyOnLoad(instance);
            }
            //Throw an error if not found
            else
            {
                Debug.Log("Error: Could not find Volume Control prefab in Resources folder. Did you rename, move or delete it?");
            }
        }

        protected override void Awake()
        {
            base.Awake();

            Storage = GetComponent<Storage>();

            //Create the channels
            this.master = new VolumeChannel(Storage, audioMixer, "MasterVolume", "VolumeControl.MasterVolume");
            this.music = new VolumeChannel(Storage, audioMixer, "MusicVolume", "VolumeControl.MusicVolume");
            this.sfx = new VolumeChannel(Storage, audioMixer, "SFXVolume", "VolumeControl.SFXVolume");
        }

        private void Start()
        {
            Load();
        }

        /// <summary>
        /// Save the settings on Android,IOs and Microsoft UWP
        /// </summary>
        /// <param name="paused"></param>
        private void OnApplicationPause(bool paused)
        {
#if (UNITY_ANDROID || UNITY_IOS || UNITY_WSA)
            if(paused==true)
            {
                Save();
            }
#endif
        }

        /// <summary>
        /// Save the settings on other systems
        /// </summary>
        private void OnApplicationQuit()
        {
            Save();
        }

        /// <summary>
        /// Loads and applies the settings of each channel from the PlayerPrefs
        /// </summary>
        private void Load()
        {
            Master.LoadState();
            Music.LoadState();
            SFX.LoadState();
        }

        /// <summary>
        /// Saves the settings of each channel in the PlayerPrefs
        /// </summary>
        public void Save()
        {
            Master.SaveState();
            Music.SaveState();
            SFX.SaveState();
        }
    }
}