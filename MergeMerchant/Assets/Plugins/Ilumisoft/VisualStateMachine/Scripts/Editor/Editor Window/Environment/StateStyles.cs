namespace Ilumisoft.VisualStateMachine.Editor
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
  
    public class StateStyles
    {
        /// <summary>
        /// Enum-List of all available State Styles
        /// </summary>
        public enum Style
        {
            Normal = 0,
            Blue = 1,
            Mint = 2,
            Green = 3,
            Yellow = 4,
            Orange = 5,
            Red = 6,
            NormalOn = 7,
            BlueOn = 8,
            MintOn = 9,
            GreenOn = 10,
            YellowOn = 11,
            OrangeOn = 12,
            RedOn = 13
        }

        /// <summary>
        /// Dictionary holding all gui styles
        /// </summary>
        Dictionary<int, GUIStyle> styleDictionary = null;

        /// <summary>
        /// Creates the dictionary containing all styles
        /// </summary>
        public StateStyles()
        {
            styleDictionary = new Dictionary<int, GUIStyle>();

            for (int i = 0; i <= 6; i++)
            {
                styleDictionary.Add(i, CreateStateStyle("builtin skins/darkskin/images/node"+i.ToString()+".png"));
                styleDictionary.Add(i+7, CreateStateStyle("builtin skins/darkskin/images/node" + i.ToString() + " on.png"));
            }
        }

        /// <summary>
        /// Returns the requested style
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public GUIStyle Get(Style style)
        {
            //Return appropriate style
            return styleDictionary[(int)style];
        }

        /// <summary>
        /// Creates a new style out of the builtin texture path
        /// </summary>
        /// <param name="builtinTexturePath"></param>
        /// <returns></returns>
        GUIStyle CreateStateStyle(string builtinTexturePath)
        {
            GUIStyle style = new GUIStyle
            {
                border = new RectOffset(10, 10, 10, 10),
                alignment = TextAnchor.MiddleCenter,
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                contentOffset = new Vector2(0, -3)
            };

            style.normal.background = EditorGUIUtility.Load(builtinTexturePath) as Texture2D;
            style.normal.textColor = Color.white;

            return style;
        }

        /// <summary>
        /// Applies a zoom factor to all styles
        /// </summary>
        /// <param name="style"></param>
        /// <param name="zoomFactor"></param>
        public void ApplyZoomFactor(float zoomFactor)
        {
            foreach(GUIStyle style in styleDictionary.Values)
            {
                style.fontSize = Mathf.RoundToInt(10 * zoomFactor);
                style.contentOffset = new Vector2(0, -3.0f * zoomFactor);
            }
        }
    }
}
