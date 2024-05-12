using UnityEngine;

namespace Ilumisoft.VolumeControl
{
    [CreateAssetMenu()]
    public class Configuration : ScriptableObject
    {
        public const string ConfigurationPath = "Ilumisoft/Volume Control/Configuration";

        public GameObject VolumeControlPrefab;

        [Header("Prefabs")]
        public GameObject VolumeSliderPrefab;
        public GameObject VolumeTogglePrefab;

        public static Configuration Find()
        {
            var result = Resources.Load<Configuration>(ConfigurationPath);

            return result;
        }
    }
}