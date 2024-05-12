namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    [UnityEditor.CustomEditor(typeof(StateInspectorHelper))]
    public class StateInspector : UnityEditor.Editor
    {
        //Serialized Properties
        private SerializedObject serializedStateMachineObject = null;

        private SerializedProperty serializedStateProperty = null;
        private SerializedProperty stateNameProperty = null;
        private SerializedProperty onEnterStateProperty = null;
        private SerializedProperty onExitStateProperty = null;
        private SerializedProperty onUpdateStateProperty = null;

        private GUIContent guiContentID = new GUIContent("ID", "A unique ID that can be used to identify the state");

        /// <summary>
        /// Loads the currently selected state and serializes its events
        /// </summary>
        public void OnEnable()
        {
            //Cache the helper
            var inspectorHelper = this.target as StateInspectorHelper;

            var stateMachine = inspectorHelper.StateMachine;
            var graph = inspectorHelper.Graph;

            if (graph != null)
            {
                this.serializedStateMachineObject = new SerializedObject(stateMachine);

                var stateArray = this.serializedStateMachineObject.FindProperty("graph").FindPropertyRelative("states");

                for (int i = 0; i < stateArray.arraySize; i++)
                {
                    this.stateNameProperty = stateArray.GetArrayElementAtIndex(i).FindPropertyRelative("id");

                    if (this.stateNameProperty.stringValue == inspectorHelper.StateID)
                    {
                        this.serializedStateProperty = stateArray.GetArrayElementAtIndex(i);

                        this.onEnterStateProperty = this.serializedStateProperty.FindPropertyRelative("onEnterState");
                        this.onExitStateProperty = this.serializedStateProperty.FindPropertyRelative("onExitState");
                        this.onUpdateStateProperty = this.serializedStateProperty.FindPropertyRelative("onUpdateState");

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

        /// <summary>
        /// Draws the currently selected state
        /// </summary>
        public override void OnInspectorGUI()
        {
            //Has the inspected state been deleted?
            if (this.serializedStateMachineObject == null || this.serializedStateMachineObject.targetObject == null)
            {
                return;
            }

            var inspectorHelper = this.target as StateInspectorHelper;
            var graph = (serializedStateMachineObject.targetObject as StateMachine).GetStateMachineGraph();

            if (graph.HasNode(inspectorHelper.StateID))
            {
                //Update the serialized objects
                this.serializedStateMachineObject.Update();

                EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);

                //Draw the property fields
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(this.onEnterStateProperty);
                EditorGUILayout.PropertyField(this.onUpdateStateProperty);
                EditorGUILayout.PropertyField(this.onExitStateProperty);

                EditorGUI.EndDisabledGroup();

                //Apply all changes
                this.serializedStateMachineObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// Draws a label field to edit the name of the selected state and a ping button
        /// to find it in the hierarchy
        /// </summary>
        protected override void OnHeaderGUI()
        {
            if (this.serializedStateMachineObject == null || this.serializedStateMachineObject.targetObject == null)
            {
                return;
            }

            var inspectorHelper = this.target as StateInspectorHelper;
            var stateMachine = (serializedStateMachineObject.targetObject as StateMachine);
            var graph = stateMachine.GetStateMachineGraph();

            if(graph.TryGetNode(inspectorHelper.StateID, out Node node))
            {
                if (node is State state)
                {
                    bool disabled = EditorApplication.isPlaying || PrefabUtility.IsPartOfAnyPrefab(stateMachine);
                    EditorGUI.BeginDisabledGroup(disabled);
                    EditorGUILayout.Space();

                    string id = state.ID;

                    EditorGUI.BeginChangeCheck();
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUIUtility.labelWidth = 20;
                            id = EditorGUILayout.DelayedTextField(guiContentID, id);
                            EditorGUIUtility.labelWidth = 0;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (stateMachine.TryRenameState(state.ID, id))
                        {
                            inspectorHelper.Inspect(stateMachine, graph, state);
                        }
                    }

                    EditorGUILayout.Space();

                    var rect = EditorGUILayout.BeginHorizontal();
                    {
                        Handles.color = Color.black;
                        Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    EditorGUI.EndDisabledGroup();
                }
            }
        }
    }
}