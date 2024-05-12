// using System.Collections;
// using System.Collections.Generic;
// using MJGame.MergeMerchant.Firebase;
// using MJGame.MergeMerchant.System;
// using UnityEngine;

// public class NetworkStart : CheckNetwork
// {
//     [SerializeField] FirebaseAuthManager firebaseAuthManager;
//     [SerializeField] GameObject panelNoInternet;

//     public override void UpdateInternetConnect()
//     {
//         if (isInternetLost && Application.internetReachability != NetworkReachability.NotReachable)
//         {
//             isInternetLost = false;
//             isConnected = true;
//             panelNoInternet.SetActive(false);

//             firebaseAuthManager.GetFirebaseInit();
//         }
//         else if (!isInternetLost && Application.internetReachability == NetworkReachability.NotReachable)
//         {
//             isInternetLost = true;
//             isConnected = false;
//             panelNoInternet.SetActive(true);
//             Debug.Log("Đã mất kết nối Internet.");
//         }
//     }

//     public override void GetAuth()
//     {
//         firebaseAuthManager.GetFirebaseInit();
//     }
// }
