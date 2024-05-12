using UnityEngine;

namespace Ilumisoft.VolumeControl
{
    public class PlayerPrefsStorage : Storage
    {
        public override float GetFloat(string key, float defaultValue = 0) => PlayerPrefs.GetFloat(key, defaultValue);

        public override int GetInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(key, defaultValue);

        public override string GetString(string key, string defaultValue = null) => PlayerPrefs.GetString(key, defaultValue);

        public override bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public override void SetFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);

        public override void SetInt(string key, int value) => PlayerPrefs.SetInt(key, value);

        public override void SetString(string key, string value) => PlayerPrefs.SetString(key, value);

        public override void Save() => PlayerPrefs.Save();
    }
}