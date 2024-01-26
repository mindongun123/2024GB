using System;
using UnityEngine;

namespace MJGame
{
    public class SingletonComponent<T> : MonoBehaviour where T : UnityEngine.Object
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (SingletonComponent<T>.instance == null)
                {
                    SingletonComponent<T>.instance = UnityEngine.Object.FindObjectOfType<T>();
                }
                if (SingletonComponent<T>.instance == null)
                {
                    UnityEngine.Debug.LogWarningFormat("[SingletonComponent] Returning null instance for component of type {0}.", new object[]
                    {
                        typeof(T)
                    });
                }
                return SingletonComponent<T>.instance;
            }
        }

        protected virtual void Awake()
        {
            this.SetInstance();
        }

        public static bool Exists()
        {
            return SingletonComponent<T>.instance != null;
        }

        public bool SetInstance()
        {
            if (SingletonComponent<T>.instance != null && SingletonComponent<T>.instance != base.gameObject.GetComponent<T>())
            {
                UnityEngine.Debug.LogWarning("[SingletonComponent] Instance already set for type " + typeof(T));
                return false;
            }
            SingletonComponent<T>.instance = base.gameObject.GetComponent<T>();
            return true;
        }
    }
}