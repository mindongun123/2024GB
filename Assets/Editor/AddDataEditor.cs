// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// namespace MJGame.Editor
// {
//     using UnityEditor;

//     [CustomEditor(typeof(AddData))]
//     public class AddDataEditor : Editor
//     {
//         public override void OnInspectorGUI()
//         {
//             base.OnInspectorGUI();
//             AddData addData = (AddData)target;
//             if (GUILayout.Button("New User"))
//             {
//                 addData.NewUser();
//             }

//             if (GUILayout.Button("GetDataFromFirebase"))
//             {
//                 addData.GetDataFromFirebase();
//             }

//             if (GUILayout.Button("Sort User"))
//             {
//                 addData.SortUser();
//             }
//         }


//     }




//     [CustomEditor(typeof(TestData))]
//     public class TestDataEditor : Editor
//     {
//         public override void OnInspectorGUI()
//         {
//             base.OnInspectorGUI();
//             TestData testData = (TestData)target;
//             if (GUILayout.Button("Load"))
//             {
//                 testData.Load();
//             }
//             if (GUILayout.Button("Check"))
//             {
//                 testData.Check();
//             }
//             if (GUILayout.Button("Save"))
//             {
//                 testData.Save();
//             }
//             if (GUILayout.Button("Save Firebase"))
//             {
//                 testData.Save();
//             }
//         }
//     }
// }