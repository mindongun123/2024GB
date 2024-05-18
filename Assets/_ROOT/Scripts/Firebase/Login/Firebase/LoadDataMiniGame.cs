// #if UNITY_FIREBASE


using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Firebase
{
    public class LoadDataMiniGame : LoadDataEvent
    {
        
        public void SaveMiniGameListToFirebase(List<int> miniGameList, int _number)
        {
            int[] miniGameArray = miniGameList.ToArray();

            reference.Child("minigame").SetValueAsync(miniGameArray).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Dữ liệu MiniGame đã được lưu trữ thành công trên Firebase.");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Đã xảy ra lỗi khi lưu trữ dữ liệu MiniGame trên Firebase: " + task.Exception);
                }
            });

            reference.Child("number").SetValueAsync(_number).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Dữ liệu MiniGame đã được lưu trữ thành công trên Firebase.");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Đã xảy ra lỗi khi lưu trữ dữ liệu MiniGame trên Firebase: " + task.Exception);
                }
            });
        }
    }
}


// #endif