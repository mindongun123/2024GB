namespace Ilumisoft.VolumeControl.Editor
{
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    internal static class MenuItemUtils
    {
        /// <summary>
        /// Searches the AudioControl Audio Mixer in the AssetDatabase and returns it
        /// </summary>
        /// <returns></returns>
        public static AudioMixer FindAudioMixer()
        {
            //Find the Audio Control Audio Mixer
            string[] guids = AssetDatabase.FindAssets("Volume Control Mixer t:AudioMixer", null);

            if (guids.Length > 0)
            {
                string guid = guids.FirstOrDefault();

                string path = AssetDatabase.GUIDToAssetPath(guid);

                AudioMixer audioMixer = AssetDatabase.LoadAssetAtPath<AudioMixer>(path);

                return audioMixer;
            }
            else
            {
                Debug.Log("Nothing found");

                return null;
            }
        }

        /// <summary>
        /// Returns resources for toggles and sliders used by DefaultControls
        /// </summary>
        /// <returns></returns>
        internal static DefaultControls.Resources GetDefaultControlResources()
        {
            DefaultControls.Resources resources = new DefaultControls.Resources
            {
                standard = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd"),
                background = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                knob = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd"),
                checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd")
            };

            return resources;
        }

        /// <summary>
        /// Sets the parent of a UIElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="menuCommand"></param>
        internal static void SetUIElementParent(GameObject element, MenuCommand menuCommand)
        {
            //Get the selected GameObject from context
            GameObject parent = menuCommand.context as GameObject;

            //Find or create a Canvas if parent is null or not a canvas
            if (parent == null || parent.GetComponentInParent<Canvas>() == null)
            {
                //Find canvas in scene
                Canvas canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;

                //Create one if none existing
                if (canvas == null)
                {
                    parent = CreateCanvas();
                }
                else
                    parent = canvas.gameObject;
            }

            string uniqueName = GameObjectUtility.GetUniqueNameForSibling(parent.transform, element.name);
            element.name = uniqueName;

            //Set undo operations
            Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);
            Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);

            //Set parent
            GameObjectUtility.SetParentAndAlign(element, parent);
        }

        /// <summary>
        /// Creates a new Canvas GameObject
        /// </summary>
        /// <returns></returns>
        private static GameObject CreateCanvas()
        {
            // Root for the UI
            var gameObject = new GameObject("Canvas")
            {
                layer = LayerMask.NameToLayer("UI")
            };

            //Add canvas components to root
            Canvas canvas = gameObject.AddComponent<Canvas>();
            gameObject.AddComponent<CanvasScaler>();
            gameObject.AddComponent<GraphicRaycaster>();

            //Set canvas render mode
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            //Register undo operation
            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);

            //Add new EventSystem, if none existing
            if (Object.FindObjectOfType<EventSystem>() == null)
            {
                CreateEventSystem();
            }

            return gameObject;
        }

        /// <summary>
        /// Creates a new EventSystem
        /// </summary>
        private static void CreateEventSystem()
        {
            var eventSystem = new GameObject("EventSystem");
            GameObjectUtility.SetParentAndAlign(eventSystem, null);

            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
        }
    }
}