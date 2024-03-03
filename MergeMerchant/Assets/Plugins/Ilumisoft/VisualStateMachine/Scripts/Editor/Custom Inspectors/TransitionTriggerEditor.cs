namespace Ilumisoft.VisualStateMachine
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TransitionTrigger))]
    public class TransitionTriggerEditor : UnityEditor.Editor
    {
        private readonly int LabelWidth = 120;

        SerializedProperty stateMachine;
        SerializedProperty type;
        SerializedProperty key;
        SerializedProperty timeMode;
        SerializedProperty delay;
        SerializedProperty executeOnStart;
        SerializedProperty logWarnings;

        GUIContent delayContent = new GUIContent("Delay", "Time in seconds the trigger gets delayed when executed");
        GUIContent executeOnStartContent = new GUIContent("Execute On Start", "Automatically executes the trigger when on Start");
        GUIContent logWarningsContent = new GUIContent("Log Warnings", "Logs warning messages if the transition could not be triggered");

        void OnEnable()
        {
            stateMachine = serializedObject.FindProperty("stateMachine");
            type = serializedObject.FindProperty("type");
            key = serializedObject.FindProperty("key");
            timeMode = serializedObject.FindProperty("timeMode");
            delay = serializedObject.FindProperty("delay");
            executeOnStart = serializedObject.FindProperty("executeOnStart");
            logWarnings = serializedObject.FindProperty("logWarnings");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //State Machine
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("State Machine", GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(stateMachine, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            //Transition
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Transition", GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(type, GUIContent.none, GUILayout.Width(70));
            EditorGUILayout.PropertyField(key, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            //Time
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(delayContent, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(timeMode, GUIContent.none, GUILayout.Width(70));
            EditorGUILayout.PropertyField(delay, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            //Execute on start
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(executeOnStartContent, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(executeOnStart, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            //Log warnings
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(logWarningsContent, GUILayout.Width(LabelWidth));
            EditorGUILayout.PropertyField(logWarnings, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}