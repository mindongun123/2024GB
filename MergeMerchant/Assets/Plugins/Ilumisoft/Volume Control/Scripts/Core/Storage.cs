using UnityEngine;

namespace Ilumisoft.VolumeControl
{
    public abstract class Storage : MonoBehaviour
    {
        public abstract int GetInt(string key, int defaultValue = 0);
        public abstract float GetFloat(string key, float defaultValue = 0.0f);
        public abstract string GetString(string key, string defaultValue = null);
        public abstract void SetInt(string key, int value);
        public abstract void SetFloat(string key, float value);
        public abstract void SetString(string key, string value);

        public abstract bool HasKey(string key);

        public abstract void Save();
    }
}