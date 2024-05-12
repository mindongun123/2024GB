using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GIE;
using UnityEditorInternal;

[CustomEditor(typeof(GetItemEffect_3D))]
public class GetItemEffectEditor_3D : Editor
{
    private ReorderableList reorderablelist;
    GetItemEffect_3D getItem;
    bool showProperties_jump;
    private void OnEnable()
    {
        SerializedProperty prop = serializedObject.FindProperty("mGetItem");
        getItem = (GetItemEffect_3D)target;
        reorderablelist = new ReorderableList(serializedObject, prop, true, true, true, true);

        reorderablelist.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "GetItem");
        };
        reorderablelist.elementHeight = 120;
        reorderablelist.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
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

        showProperties_jump = EditorGUILayout.Foldout(showProperties_jump, "Jump");
        if (showProperties_jump)
        {
            getItem.mJumpRadius_X = EditorGUILayout.Vector2Field("mJumpRadius_X", getItem.mJumpRadius_X);
            getItem.mJumpRadius_Y = EditorGUILayout.Vector2Field("mJumpRadius_Y", getItem.mJumpRadius_Y);
            getItem.mJumpRadius_Z = EditorGUILayout.Vector2Field("mJumpRadius_Z", getItem.mJumpRadius_Z);
            getItem.mJumpForce = EditorGUILayout.Vector2Field("mJumpForce", getItem.mJumpForce);
            getItem.mJumpToFlyDuration = EditorGUILayout.FloatField("mJumpToFlyDuration", getItem.mJumpToFlyDuration);
            getItem.mJumpFlySpeed = EditorGUILayout.FloatField("mJumpFlySpeed", getItem.mJumpFlySpeed);
        }

        serializedObject.ApplyModifiedProperties();
    }
}