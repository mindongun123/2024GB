using UnityEngine;
using System.Collections;

namespace RejectedGames
{
    public class GUIView : MonoBehaviour
    {
        private float musicVolume = 0;
        private bool isCloudSaveEnabled = false;
        private int lastUnlockedLevel = 0;
        private string playername = "Noname";

        private const string MUSICVOLUME_KEY = "MusicVolume";
        private const string CLOUDSAVE_KEY = "IsCloudSaveEnabled";
        private const string LASTUNLOCKEDLEVEL_KEY = "LastUnlockedLevel";
        private const string PLAYERNAME_KEY = "PlayerName";

        // Use this for initialization
        void Start()
        {
            if (!PlayerPrefs.HasKey(PLAYERNAME_KEY))
                SaveData();

            RefreshData();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
                RefreshData();
        }

        void OnGUI()
        {
            int width = 450;
            int height = 400;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height));
            {
                GUILayout.BeginVertical();
                {
                    string macRefresh = "Unfortunately, this will take a a few seconds. This is due Unity working different on a Mac :(";
                    GUILayout.TextArea(string.Format("Instructions: {0} 1) Open the Advanced PlayerPrefs Window and dock it somewhere. {0} 2) Change the values in the scene using the gui widgets. {0} 3) Go back to the Advanced PlayerPrefs Window and click the refresh button. " + (Application.platform == RuntimePlatform.OSXEditor ? macRefresh : "") + " {0} 4) Observe that the values in the Advanced PlayerPrefs Window has changed to your scene input. {0}{0} 5) Now in the Advanced PlayerPrefs Window, change the values and save those changes {0} 6) Go give the scene focus by clicking in the sceneview. {0} 7) Watch the gui values update to your changes", System.Environment.NewLine));

                    GUILayout.Space(12);

                    //Music volume bar
                    GUILayout.Label("Music Volume: " + (int)musicVolume + "%");
                    float newVolume = GUILayout.HorizontalSlider(musicVolume, 0, 100);
                    if (!Mathf.Approximately(newVolume, musicVolume))
                    {
                        musicVolume = newVolume;
                        SaveData();
                    }

                    GUILayout.Space(12);

                    //Cloud Save
                    bool newCloudSaveEnabled = GUILayout.Toggle(isCloudSaveEnabled, "Enable Cloud Save?");
                    if (newCloudSaveEnabled != isCloudSaveEnabled)
                    {
                        isCloudSaveEnabled = newCloudSaveEnabled;
                        SaveData();
                    }

                    GUILayout.Space(12);

                    //Last Unlocked Level label
                    GUILayout.Label("Last Unlocked Level: " + lastUnlockedLevel);

                    GUILayout.Space(12);

                    //Player Name label
                    GUILayout.Label("Playername");
                    string newPlayerName = GUILayout.TextField(playername);
                    if(newPlayerName != playername)
                    {
                        playername = newPlayerName;
                        SaveData();
                    }

                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }

        public void RefreshData()
        {
            musicVolume = PlayerPrefs.GetFloat(MUSICVOLUME_KEY, 100);
            isCloudSaveEnabled = PlayerPrefs.GetString(CLOUDSAVE_KEY, "true") == "true"; //convert string to bool
            lastUnlockedLevel = PlayerPrefs.GetInt(LASTUNLOCKEDLEVEL_KEY, 123);
            playername = PlayerPrefs.GetString(PLAYERNAME_KEY, "Noname");
        }

        public void SaveData()
        {
            PlayerPrefs.SetFloat(MUSICVOLUME_KEY, musicVolume);
            PlayerPrefs.SetString(CLOUDSAVE_KEY, isCloudSaveEnabled ? "true" : "false"); //convert bool to string
            PlayerPrefs.SetInt(LASTUNLOCKEDLEVEL_KEY, lastUnlockedLevel);
            PlayerPrefs.SetString(PLAYERNAME_KEY, playername);
        }
    }
}