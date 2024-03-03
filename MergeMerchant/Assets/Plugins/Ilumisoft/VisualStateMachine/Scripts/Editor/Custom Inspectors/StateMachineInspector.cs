namespace Ilumisoft.VisualStateMachine.Editor
{
    using Ilumisoft.VisualStateMachine.Editor.Extensions;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(StateMachine))]
    public class StateMachineInspector : Editor
    {
        //Foldout values
        private bool showStates = false;
        private bool showTransitions = false;
        private string transitionSearchFilter = string.Empty;
        private string stateSearchFilter = string.Empty;

        //Reference to the inspected state machine
        private StateMachine stateMachine = null;

        private Graph graph = null;

        //GUI Contents of the buttons
        private GUIContent selectButtonContent = new GUIContent("Select");
        private GUIContent openButtonContent = new GUIContent("Open");

        private GUIContent stateListFoldoutContent = new GUIContent("States", "All states of the state machine");

        private void OnEnable()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;

            UpdateCache();
        }

        private void OnDisable()
        {
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
        }

        /// <summary>
        /// Gets all states and transitions of the inspected State Machine
        /// </summary>
        private void UpdateCache()
        {
            this.stateMachine = (StateMachine)this.target;

            if (this.stateMachine != null)
            {
                this.graph = this.stateMachine.GetStateMachineGraph();
            }
        }

        /// <summary>
        /// Updates the cache and triggers a repaint when the hierarchy has changed
        /// </summary>
        private void OnHierarchyChanged()
        {
            UpdateCache();

            Repaint();
        }

        public override void OnInspectorGUI()
        {
            if (this.stateMachine == null)
            {
                return;
            }

            this.serializedObject.Update();

            GUILayoutUtils.VerticalSpace(8);

            DrawGraphButton();

            GUILayoutUtils.VerticalSpace(4);

            DrawStateList();
            DrawTransitionList();

            this.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws the button to open the State Machine Graph Editor
        /// </summary>
        private void DrawGraphButton()
        {
            GUILayoutUtils.HorizontalGroup(() =>
            {
                EditorGUILayout.LabelField("Graph Editor");

                if (GUILayout.Button(this.openButtonContent, EditorStyles.miniButton, GUILayout.Width(50)))
                {
                    var castedTarget = (this.target as StateMachine);

                    EditorWindowCommands.OpenStateMachineGraph(castedTarget);
                }
            });
        }

        /// <summary>
        /// Draws a list of all states of the state machine
        /// </summary>
        private void DrawStateList()
        {
            this.showStates = EditorGUILayout.Foldout(this.showStates, this.stateListFoldoutContent);

            if (this.showStates)
            {
                GUILayoutUtils.Ident(() =>
                {
                    this.stateSearchFilter = EditorGUILayout.TextField("Search", this.stateSearchFilter);

                    EditorGUILayout.Space();

                    string filterKeyword = this.stateSearchFilter.ToLower();

                    foreach (var node in this.graph.Nodes)
                    {
                        if (node is State state)
                        {
                            GUILayoutUtils.HorizontalGroup(() =>
                            {
                                if (state.ID.ToLower().Contains(filterKeyword))
                                {
                                    EditorGUILayout.LabelField(state.ID);

                                    if (GUILayout.Button(this.selectButtonContent, EditorStyles.miniButton, GUILayout.Width(50)))
                                    {
                                        EditorWindowCommands.OpenStateMachineGraph(this.stateMachine).SelectState(state);
                                    }
                                }
                            });
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Daws a list of all transitions of the state machine
        /// </summary>
        private void DrawTransitionList()
        {
            this.showTransitions = EditorGUILayout.Foldout(this.showTransitions, "Transitions");

            if (this.showTransitions)
            {
                GUILayoutUtils.Ident(() =>
                {
                    this.transitionSearchFilter = EditorGUILayout.TextField("Search", this.transitionSearchFilter);

                    EditorGUILayout.Space();

                    string filterKeyword = this.transitionSearchFilter.ToLower();

                    foreach (var transition in this.graph.Transitions)
                    {
                        GUILayoutUtils.HorizontalGroup(() =>
                        {
                            if (transition != null && transition.ID.ToLower().Contains(filterKeyword))
                            {
                                EditorGUILayout.LabelField(transition.ID);

                                if (GUILayout.Button(this.selectButtonContent, EditorStyles.miniButton, GUILayout.Width(50)))
                                {
                                    EditorWindowCommands.OpenStateMachineGraph(this.stateMachine).SelectTransition(transition);
                                }
                            }
                        });
                    }
                });
            }
        }
    }
}