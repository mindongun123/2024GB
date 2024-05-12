namespace Ilumisoft.VolumeControl.Editor
{
    using Ilumisoft.VolumeControl.UI;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.UI;

    public class MenuItems
    {
        /// <summary>
        /// Opens the online documentation
        /// </summary>
        [MenuItem("Help/Ilumisoft/Volume Control")]
        private static void ShowOnlineDocumentation()
        {
            Help.BrowseURL("https://ilumisoft.gitbook.io/documentation/tools/volume-control");
        }

        /// <summary>
        /// Adds a new MusicSource to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/Audio/Music Source", false, 0)]
        private static void CreateMusicSource(MenuCommand menuCommand)
        {
            AudioMixer audioMixer = MenuItemUtils.FindAudioMixer();

            AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Master/Music").FirstOrDefault();

            CreateAudioSource(menuCommand, "Music Source", audioMixerGroup);
        }

        /// <summary>
        /// Adds a new SFXSource to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/Audio/SFX Source", false, 0)]
        private static void CreateSFXSource(MenuCommand menuCommand)
        {
            AudioMixer audioMixer = MenuItemUtils.FindAudioMixer();

            AudioMixerGroup audioMixerGroup = audioMixer.FindMatchingGroups("Master/SFX").FirstOrDefault();

            CreateAudioSource(menuCommand, "SFX Source", audioMixerGroup);
        }

        /// <summary>
        /// Adds a new MasterSlider to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/UI/Volume Control/Master Slider", false, 10)]
        private static void CreateMasterSlider(MenuCommand menuCommand)
        {
            CreateVolumeSlider<MasterVolumeSlider>(menuCommand, "Master Volume Slider");
        }

        /// <summary>
        /// Adds a new MusicSlider to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/UI/Volume Control/Music Slider", false, 10)]
        private static void CreateMusicSlider(MenuCommand menuCommand)
        {
            CreateVolumeSlider<MusicVolumeSlider>(menuCommand, "Music Volume Slider");
        }

        /// <summary>
        /// Adds a new SFXSlider to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/UI/Volume Control/SFX Slider", false, 10)]
        private static void CreateSFXSlider(MenuCommand menuCommand)
        {
            CreateVolumeSlider<SFXVolumeSlider>(menuCommand, "SFX Volume Slider");
        }

        /// <summary>
        /// Adds a new MasterToggle to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/UI/Volume Control/Master Toggle", false, 10)]
        private static void CreateMasterToggle(MenuCommand menuCommand)
        {
            CreateVolumeToggle<MasterVolumeToggle>(menuCommand, "Master Volume Toggle");
        }

        /// <summary>
        /// Adds a new MusicToggle to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/UI/Volume Control/Music Toggle", false, 10)]
        private static void CreateMusicToggle(MenuCommand menuCommand)
        {
            CreateVolumeToggle<MusicVolumeToggle>(menuCommand, "Music Volume Toggle");
        }

        /// <summary>
        /// Adds a new SFXToggle to the scene
        /// </summary>
        /// <param name="menuCommand"></param>
        [MenuItem("GameObject/UI/Volume Control/SFX Toggle", false, 10)]
        private static void CreateSFXToggle(MenuCommand menuCommand)
        {
            CreateVolumeToggle<SFXVolumeToggle>(menuCommand, "SFX Volume Toggle");
        }

        /// <summary>
        /// Creates a volume slider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuCommand"></param>
        /// <param name="name"></param>
        private static void CreateVolumeSlider<T>(MenuCommand menuCommand, string name) where T : VolumeSlider
        {
            Configuration configuration = Configuration.Find();

            GameObject gameObject;

            // Use slider prefab from the configuration if set
            if (configuration != null && configuration.VolumeSliderPrefab != null)
            {
                gameObject = Object.Instantiate(configuration.VolumeSliderPrefab);
            }
            // Otherwise create a default slider
            else
            {
                gameObject = DefaultControls.CreateToggle(MenuItemUtils.GetDefaultControlResources());
            }

            gameObject.name = name;

            //Add the VolumeSlider component to the gameObject
            gameObject.AddComponent<T>();

            MenuItemUtils.SetUIElementParent(gameObject, menuCommand);

            Selection.activeGameObject = gameObject;
        }


        /// <summary>
        /// Creates a volume toggle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuCommand"></param>
        /// <param name="name"></param>
        private static void CreateVolumeToggle<T>(MenuCommand menuCommand, string name) where T : VolumeToggle
        {
            Configuration configuration = Configuration.Find();

            GameObject gameObject;

            // Use toggle prefab from the configuration if set
            if (configuration != null && configuration.VolumeTogglePrefab != null)
            {
                gameObject = Object.Instantiate(configuration.VolumeTogglePrefab);
            }
            // Otherwise create a default toggle
            else
            {
               gameObject = DefaultControls.CreateToggle(MenuItemUtils.GetDefaultControlResources());
            }

            gameObject.name = name;

            //Add the VolumeToggle component to the gameObject
            gameObject.AddComponent<T>();

            MenuItemUtils.SetUIElementParent(gameObject, menuCommand);

            Selection.activeGameObject = gameObject;
        }

        /// <summary>
        /// Adds a new AudioSource Control to the scene
        /// </summary>
        /// <typeparam name="AudioSourceControl"></typeparam>
        /// <param name="menuCommand"></param>
        /// <param name="name"></param>
        private static void CreateAudioSource(MenuCommand menuCommand, string name, AudioMixerGroup audioMixerGroup)
        {
            GameObject go = new GameObject(name);

            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

            AudioSource audioSource = go.AddComponent<AudioSource>();

            audioSource.outputAudioMixerGroup = audioMixerGroup;

            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

            Selection.activeObject = go;
        }
    }
}