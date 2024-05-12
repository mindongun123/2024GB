#if UNITY_FIREBASE

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;
using TMPro;
using MJGame.MergeMerchant.Merge;
using MJGame.MergeMerchant.System;

namespace MJGame.MergeMerchant.Firebase
{
    public class FirebaseAuthManager : MonoBehaviour
    {
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser user;
        DatabaseReference reference;

        public void GetFirebaseInit()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
                       {
                           dependencyStatus = task.Result;

                           if (dependencyStatus == DependencyStatus.Available)
                           {
                               InitializeFirebase();
                           }
                           else
                           {
                               Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
                           }
                       });
        }

        private void InitializeFirebase()
        {
            auth = FirebaseAuth.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        }


        [Button]
        public void LogOut()
        {
            auth.SignOut();
        }

        public IEnumerator RegisterAsync(string name, string email, string password)
        {

            if (!SingletonComponent<CheckNetwork>.Instance.isConnected) yield return null;
            if (name == "")
            {
                SingletonComponent<SpawnText>.Instance.NewText("<color=red>User Name is empty</color>", 1f);
                SingletonComponent<ViewLogin>.Instance.LoadingEnable();
            }
            else if (email == "")
            {

                SingletonComponent<SpawnText>.Instance.NewText("<color=red>email field is empty</color>", 1f);
                SingletonComponent<ViewLogin>.Instance.LoadingEnable();
            }
            else
            {
                var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

                yield return new WaitUntil(() => registerTask.IsCompleted);

                if (registerTask.Exception != null)
                {

                    FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;

                    string failedMessage = "Registration Failed! Becuase ";

                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Registration Failed";
                            break;
                    }

                    SingletonComponent<SpawnText>.Instance.NewText($"<color=yellow>{failedMessage}</color>", 1f);

                    SingletonComponent<ViewLogin>.Instance.LoadingEnable();
                }
                else
                {
                    Data data = new Data(name, 0, email);
                    PlayerPrefs.SetString(ConstGame.EMAIL, data.email);
                    SaveUserData(data);
                    SingletonComponent<ViewLogin>.Instance.LoadScene();
                }
            }
        }
        private void SaveUserData(Data data)
        {
            if (data != null)
            {

                Dictionary<string, object> dataValues = data.ToDictionary();

                reference.Child("users").Push().SetValueAsync(dataValues).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError("Error adding data: " + task.Exception);
                    }
                    else if (task.IsCompleted)
                    {
                        // Debug.Log("Data added successfully.");
                    }
                });
            }
        }

    }

    [Serializable]
    public class Data
    {
        public string name;
        public int value;
        public string email;
        public Data(string name, int value, string email)
        {
            this.name = name;
            this.value = value;
            this.email = email;
        }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["name"] = name;
            result["value"] = value;
            result["email"] = email;
            return result;
        }

    }

}

#endif