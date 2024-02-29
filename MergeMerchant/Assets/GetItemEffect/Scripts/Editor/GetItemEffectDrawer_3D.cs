using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GIE;

[CustomPropertyDrawer(typeof(Item_3D))]
public class GetItemEffectDrawer_3D : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUIUtility.labelWidth = 100;
            position.height = EditorGUIUtility.singleLineHeight;
            Rect name = new Rect(position)
            {
                width = position.width,
            };
            Rect template = new Rect(name)
            {
                y = name.y + EditorGUIUtility.singleLineHeight + 5
            };
            Rect towhere = new Rect(template)
            {
                y = template.y + EditorGUIUtility.singleLineHeight + 5
            };
            Rect number = new Rect(towhere)
            {
                y = towhere.y + EditorGUIUtility.singleLineHeight + 5
            };

            SerializedProperty nameProperty = property.FindPropertyRelative("mItemName");
            SerializedProperty templateProperty = property.FindPropertyRelative("mItemTemplate");
            SerializedProperty towhereProperty = property.FindPropertyRelative("mItemToWhere");
            SerializedProperty numberProperty = property.FindPropertyRelative("mCacheNumber");

            nameProperty.stringValue = EditorGUI.TextField(name, nameProperty.displayName, nameProperty.stringValue);
            templateProperty.objectReferenceValue = EditorGUI.ObjectField(template, templateProperty.displayName, templateProperty.objectReferenceValue, typeof(GetItem_3D), true);
            towhereProperty.objectReferenceValue = EditorGUI.ObjectField(towhere, towhereProperty.displayName, towhereProperty.objectReferenceValue, typeof(Transform), true);
            numberProperty.intValue = EditorGUI.IntSlider(number, numberProperty.displayName, numberProperty.intValue, 0, 50);
        }
    }
}
