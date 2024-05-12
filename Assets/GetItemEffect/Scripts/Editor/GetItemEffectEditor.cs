using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GIE;
using UnityEditorInternal;

[CustomEditor(typeof(GetItemEffect))]
public class GetItemEffectEditor : Editor
{
    private ReorderableList reorderablelist;
    GetItemEffect getItem;
    bool showProperties_explostion;
    bool showProperties_jump;
    bool showProperties_fly;
    private void OnEnable()
    {
        SerializedProperty prop = serializedObject.FindProperty("mGetItem");
        getItem = (GetItemEffect)target;
        reorderablelist = new ReorderableList(serializedObject, prop, true, true, true, true);

        reorderablelist.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "GetItem");
        };
        reorderablelist.elementHeight = 120;
        reorderablelist.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            //根据index获取对应元素 
            SerializedProperty item = reorderablelist.serializedProperty.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, item, new GUIContent("Index " + index));
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        reorderablelist.DoLayoutList();

        showProperties_explostion = EditorGUILayout.Foldout(showProperties_explostion, "Explosion");
        if (showProperties_explostion)
        {
            getItem.mExplostionRadius = EditorGUILayout.Vector2Field("mExplostionRadius", getItem.mExplostionRadius);
            getItem.mExplostionSpeed = EditorGUILayout.FloatField("mExplostionSpeed", getItem.mExplostionSpeed);
            getItem.mExplostionFlySpeed = EditorGUILayout.FloatField("mExplostionFlySpeed", getItem.mExplostionFlySpeed);
        }

        showProperties_jump = EditorGUILayout.Foldout(showProperties_jump, "Jump");
        if (showProperties_jump)
        {
            getItem.mJumpRadius = EditorGUILayout.Vector2Field("mJumpRadius", getItem.mJumpRadius);
            getItem.mJumpHeight = EditorGUILayout.Vector2Field("mJumpHeight", getItem.mJumpHeight);
            getItem.mJumpToFlyDuration = EditorGUILayout.FloatField("mJumpToFlyDuration", getItem.mJumpToFlyDuration);
            getItem.mJumpSpeed = EditorGUILayout.FloatField("mJumpSpeed", getItem.mJumpSpeed);
            getItem.mJumpFlySpeed = EditorGUILayout.FloatField("mJumpFlySpeed", getItem.mJumpFlySpeed);
        }


        showProperties_fly = EditorGUILayout.Foldout(showProperties_fly, "Fly");
        if (showProperties_fly)
        {
            getItem.mFlyRadius = EditorGUILayout.Vector2Field("mFlyRadius", getItem.mFlyRadius);
            getItem.mFlySpeed = EditorGUILayout.FloatField("mFlySpeed", getItem.mFlySpeed);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
