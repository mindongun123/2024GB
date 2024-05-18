// #if UNITY_FIREBAE
using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using System;
using MJGame.MergeMerchant.Firebase;

public class Account : MonoBehaviour
{
    DatabaseReference reference;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void AddNewData(string name, int value, string email)
    {
        Data newData = new Data(name, value, email);

        Dictionary<string, object> dataValues = newData.ToDictionary();

        reference.Child("users").Push().SetValueAsync(dataValues).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error adding data: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Data added successfully.");
            }
        });
    }
}
 

// #endif