using UnityEditor;
using UnityEngine;

namespace Ilumisoft.VolumeControl.Editor
{
    class VolumeControlSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateStartupProfileConfigurationProvider() => CreateProvider("Project/Volume Control", Configuration.Find());

        static SettingsProvider CreateProvider(string settingsWindowPath, Object asset)
        {
            var provider = AssetSettingsProvider.CreateProviderFromObject(settingsWindowPath, asset);

            provider.keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(asset));
            return provider;
        }
    }
}