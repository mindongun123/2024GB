
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FindMissingScriptsWindow : EditorWindow
{
    [MenuItem("Window/Missing Script Window")]
    private static void Init()
    {
        GetWindow<FindMissingScriptsWindow>("Missing Script Finder").Show();
    }

    public List<GameObject> results = new List<GameObject>();

    private void OnGUI()
    {
        if (GUILayout.Button("Search Project"))
            SearchProject();
        if (GUILayout.Button("Search scene"))
            SearchScene();
        if (GUILayout.Button("Find all, including disabled objects."))
            FindAll();
        if (GUILayout.Button("Search Selected Objects"))
            SearchSelected();
        if (GUILayout.Button("Remove Selected Objects"))
            RemoveScripts();

        // src: https://answers.unity.com/questions/859554/editorwindow-display-array-dropdown.html
        var so = new SerializedObject(this);
        var resultsProperty = so.FindProperty(nameof(results));
        EditorGUILayout.PropertyField(resultsProperty, true);
        so.ApplyModifiedProperties();
    }

    private void SearchProject()
    {
        results = AssetDatabase.FindAssets("t:Prefab")
           .Select(AssetDatabase.GUIDToAssetPath)
           .Select(AssetDatabase.LoadAssetAtPath<GameObject>)
           .Where(x => IsMissing(x, true))
           .Distinct()
           .ToList();
    }

    private void SearchScene()
    {
        results = FindObjectsOfType<GameObject>()
           .Where(x => IsMissing(x, false))
           .Distinct()
           .ToList();

    }

    private void FindAll()
    {
        results = Resources.FindObjectsOfTypeAll<GameObject>()
             .Where(x => x.hideFlags == HideFlags.None && IsMissing(x, false))
             .Distinct()
             .ToList();
    }

    private void SearchSelected()
    {
        results = Selection.gameObjects
           .Where(x => IsMissing(x, false))
           .Distinct()
           .ToList();
    }

    private void RemoveScripts()
    {
        for (int i = 0; i < results.Count; i++)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(results[i]);
        }
    }

    private static bool IsMissing(GameObject go, bool includeChildren)
    {
        var components = includeChildren
           ? go.GetComponentsInChildren<Component>()
           : go.GetComponents<Component>();

        return components.Any(x => x == null);
    }
}
