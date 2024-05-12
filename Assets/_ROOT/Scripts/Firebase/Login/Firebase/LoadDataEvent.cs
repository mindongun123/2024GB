#if UNITY_FIREBASE
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using MJGame.MergeMerchant.System;
using Ilumisoft.VisualStateMachine;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;

namespace MJGame.MergeMerchant.Firebase
{

    public class LoadDataEvent : SingletonComponent<LoadDataEvent>
    {
 

        [SerializeField] StateMachine stateMachine;
        public DatabaseReference reference;
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public Data data;
        [Header("Rank")]
        public List<Data> listRank;

        [Header("MiniGame")]
        public List<int> lsId = new List<int>();
        public int _number;

        public bool _initComplete = false;

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
                               Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                           }
                       });
        }

        public void InitializeFirebase()
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            auth = FirebaseAuth.DefaultInstance;
            SingletonComponent<CheckNetwork>.Instance._connectFirebaseComplete = true;
            LoadDataUser();
            LoadDataMiniGame();
        }

        public void LoadDataUser()
        {
            reference.Child("users").ValueChanged += LoadAccount;
        }

        public void LoadDataBoardRank()
        {
            if (SingletonComponent<CheckNetwork>.Instance.isConnected)
            {
                LoadDataUser();
                stateMachine.Trigger("OpenEvent");
            }
            else
            {
                print("no internet");
            }
        }


        public void LoadAccount(object sender, ValueChangedEventArgs args)
        {
            reference.Child("users").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Error getting data: " + task.Exception);
                    return;
                }

                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    if (snapshot != null && snapshot.Exists)
                    {
                        var sortedSnapshots = snapshot.Children.OrderByDescending(childSnapshot =>
                        {
                            var valueSnapshot = childSnapshot.Child("value");
                            return valueSnapshot != null ? Convert.ToInt32(valueSnapshot.Value) : 0;
                        });

                        listRank = new List<Data>();

                        foreach (var childSnapshot in sortedSnapshots)
                        {
                            var nameSnapshot = childSnapshot.Child("name");
                            var valueSnapshot = childSnapshot.Child("value");
                            var emailSnapshot = childSnapshot.Child("email");

                            if (nameSnapshot != null && valueSnapshot != null && emailSnapshot != null)
                            {
                                var name = nameSnapshot.Value.ToString();
                                var value = Convert.ToInt32(valueSnapshot.Value);
                                var email = emailSnapshot.Value.ToString();
                                if (auth.CurrentUser != null && email.ToLower().Equals(auth.CurrentUser.Email))
                                {
                                    data = new Data(name, value, email);
                                }

                                listRank.Add(new Data(name, value, email));
                            }
                        }

                        if (data == null)
                        {
                            Debug.Log("No data found for the current user.");
                        }
                    }
                    else
                    {
                        Debug.Log("No data found.");
                    }
                }
            });
        }

        public void LoadDataMiniGame()
        {
            reference.Child("minigame").ValueChanged += HandleMiniGameLoadListId;
            reference.Child("number").ValueChanged += HandleMiniGameLoadNumber;
        }

        public void HandleMiniGameLoadListId(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError("Đã xảy ra lỗi khi tải dữ liệu MiniGame từ Firebase: " + args.DatabaseError.Message);
                return;
            }

            if (args.Snapshot != null && args.Snapshot.Exists)
            {
                var miniGameArray = ((List<object>)args.Snapshot.Value).ConvertAll(Convert.ToInt32);

                foreach (var item in miniGameArray)
                {
                    lsId.Add(item);
                }
                Debug.Log("Load Data Id Mini Game thanh cong");
            }
            else
            {
                Debug.Log("Không tìm thấy dữ liệu MiniGame trong Firebase.");
            }
        }

        public void HandleMiniGameLoadNumber(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError("Đã xảy ra lỗi khi tải dữ liệu MiniGame từ Firebase: " + args.DatabaseError.Message);
                return;
            }

            if (args.Snapshot != null && args.Snapshot.Exists)
            {
                _number = Convert.ToInt32(args.Snapshot.Value);
                // Debug.Log("Dữ liệu MiniGame đã tải từ Firebase: " + _number);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy dữ liệu MiniGame trong Firebase.");
            }
        }

        public void SetUserData(int _value)
        {
            data.value += _value;

            data.value = data.value > 0 ? data.value : 0;

            DatabaseReference usersRef = reference.Child("users");
            string emailKey = PlayerPrefs.GetString(ConstGame.EMAIL, "");
            Query query = usersRef.OrderByChild("email").EqualTo(emailKey);

            print(emailKey);
            print(query.ToString());

            
            query.GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Error querying data: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;


                    if (snapshot.HasChildren)
                    {

                        foreach (DataSnapshot childSnapshot in snapshot.Children)
                        {
                            string userId = childSnapshot.Key;

                            Dictionary<string, object> updateData = data.ToDictionary();

                            usersRef.Child(userId).UpdateChildrenAsync(updateData).ContinueWith(updateTask =>
                            {
                                if (updateTask.IsFaulted)
                                {
                                    Debug.LogError("Error updating data: " + updateTask.Exception);
                                }
                                else if (updateTask.IsCompleted)
                                {
                                    Debug.Log("Data updated successfully.");
                                }
                            });
                        }
                    }
                    else
                    {
                        Debug.Log("No data found with the specified email.");
                    }
                }
            });
        }
    }
}

#endif