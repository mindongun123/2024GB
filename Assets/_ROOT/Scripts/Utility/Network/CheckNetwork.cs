using MJGame.MergeMerchant.Firebase;
using UnityEngine;
using UnityEngine.Networking;

namespace MJGame.MergeMerchant.System
{
    public class CheckNetwork : SingletonComponent<CheckNetwork>
    {
        public bool isConnected;
        public bool isInternetLost;

        public bool _connectFirebaseComplete = false;
        private void Start()
        {
            CheckInternetConnection();
        }
        private void Update()
        {
            UpdateInternetConnect();
        }

        public virtual void UpdateInternetConnect()
        {
            if (isInternetLost && Application.internetReachability != NetworkReachability.NotReachable)
            {
                isInternetLost = false;
                isConnected = true;
                if (!_connectFirebaseComplete)
                {
#if UNITY_FIREBASE
                    SingletonComponent<LoadDataEvent>.Instance.GetFirebaseInit();
#endif
                }
            }
            else if (!isInternetLost && Application.internetReachability == NetworkReachability.NotReachable)
            {
                isInternetLost = true;
                isConnected = false;
                Debug.Log("Đã mất kết nối Internet.");
            }
        }

        private void CheckInternetConnection()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                isConnected = true;
                GetAuth();
            }
            else
            {
                isConnected = false;
                Debug.Log("Không có kết nối Internet.");
            }
        }

        public virtual void GetAuth()
        {

#if UNITY_FIREBAE
            SingletonComponent<LoadDataEvent>.Instance.GetFirebaseInit();
#endif
        }

        void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                CheckInternetConnection();
            }
        }
    }
}