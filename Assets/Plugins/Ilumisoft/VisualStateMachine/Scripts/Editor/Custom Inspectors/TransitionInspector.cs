namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TransitionInspectorHelper))]
    public class TransitionInspector : UnityEditor.Editor
    {
        private readonly int LabelWidth = 80;

        //Serialized Objects
        private SerializedObject serializedStateMachineObject = null;

        //Serialized Properties 
        private SerializedProperty onEnterTransitionProperty = null;
        private SerializedProperty onExitTransitionProperty = null;
        private SerializedProperty timeMode = null;
        private SerializedProperty delay = null;

        private GUIContent guiContentLabel= new GUIContent("Label", "A label for the transition. This does not need to be unique.");
        private GUIContent guiContentID = new GUIContent("ID", "A unique ID that can be used to identify the transition");
        private GUIContent guiContentDuration = new GUIContent("Duration", "The time in seconds the transition takes to finish");

        private void OnEnable()
        {
            //Cache the helper
            var inspectorHelper = this.target as TransitionInspectorHelper;

            var stateMachine = inspectorHelper.StateMachine;

            if (stateMachine != null)
            {
                this.serializedStateMachineObject = new SerializedObject(stateMachine);

                var stateArray = this.serializedStateMachineObject.FindProperty("graph").FindPropertyRelative("transitions");

                for (int i = 0; i < stateArray.arraySize; i++)
                {
                    var nameProperty = stateArray.GetArrayElementAtIndex(i).FindPropertyRelative("id");

                    if (nameProperty.stringValue == inspectorHelper.TransitionID)
                    {
                        var elementProperty = stateArray.GetArrayElementAtIndex(i);

                        this.onEnterTransitionProperty = elementProperty.FindPropertyRelative("onEnterTransition");
                        this.onExitTransitionProperty = elementProperty.FindPropertyRelative("onExitTransition");
                        this.timeMode = elementProperty.FindPropertyRelative("timeMode");
                        this.delay = elementProperty.FindPropertyRelative("duration");

                        return;
                    }
                }
            }

            Selection.activeObject = null;
        }

        public void Reload()
        {
            OnEnable();
            Repaint();
        }

        public override void OnInspectorGUI()
        {
            //Has the inspected state been deleted?
            if (this.serializedStateMachineObject == null || this.serializedStateMachineObject.targetObject == null)
            {
                return;
            }

            var inspectorHelper = this.target as TransitionInspectorHelper;
            var stateMachine = serializedStateMachineObject.targetObject as StateMachine;
            var graph = stateMachine.GetStateMachineGraph();

            if (inspectorHelper == null || graph == null)
            {
                return;
            }
            
            if (graph.HasTransition(inspectorHelper.TransitionID))
            {
                //Update the serialized objects
                this.serializedStateMachineObject.Update();

                EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
                {   
                    //Time
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(guiContentDuration, GUILayout.Width(LabelWidth));
                    EditorGUILayout.PropertyField(timeMode, GUIContent.none, GUILayout.Width(70));
                    EditorGUILayout.PropertyField(delay, GUIContent.none);
                    EditorGUILayout.EndHorizontal();

                    //Draw the property fields
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(this.onEnterTransitionProperty);
                    EditorGUILayout.PropertyField(this.onExitTransitionProperty);
                }
                EditorGUI.EndDisabledGroup();

                //Apply all changes
                this.serializedStateMachineObject.ApplyModifiedProperties();
            }
        }

        protected override void OnHeaderGUI()
        {
            if (this.serializedStateMachineObject == null || this.serializedStateMachineObject.targetObject == null)
            {
                return;
            }

            var inspectorHelper = this.target as TransitionInspectorHelper;
            var stateMachine = serializedStateMachineObject.targetObject as StateMachine;
            var graph = stateMachine.GetStateMachineGraph();

            if (inspectorHelper == null || graph == null)
            {
                return;
            }

            var transition = inspectorHelper.Transition;

            if (transition != null)
            {
                EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying || PrefabUtility.IsPartOfAnyPrefab(stateMachine));
                {
                    EditorGUILayout.Space();

                    DrawRenameField(stateMachine, transition);

                    EditorGUILayout.Space();

                    DrawHorizontalDivider();

                    EditorGUILayout.Space();
                }
                EditorGUI.EndDisabledGroup();
            }
        }

        private void DrawHorizontalDivider()
        {
            var rect = EditorGUILayout.BeginHorizontal();
            {
                Handles.color = Color.black;
                Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));

            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawRenameField(StateMachine stateMachine, Transition transition)
        {
            string label = transition.Label;

            EditorGUI.BeginChangeCheck();
            {
                EditorGUIUtility.labelWidth = 40;
                label = EditorGUILayout.DelayedTextField(guiContentLabel, label);
                EditorGUIUtility.labelWidth = 0;
            }
            if (EditorGUI.EndChangeCheck())
            {
                transition.Label = label;
            }

            string id = transition.ID;

            EditorGUI.BeginChangeCheck();
            {
                EditorGUIUtility.labelWidth = 40;
                id = EditorGUILayout.DelayedTextField(guiContentID, id);
                EditorGUIUtility.labelWidth = 0;
            }
            if (EditorGUI.EndChangeCheck())
            {
                if (stateMachine.TryRenameTransition(transition, id))
                {
                    TransitionInspectorHelper.Instance.Inspect(stateMachine, transition);
                }
            }
        }
    }
}