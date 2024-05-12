namespace Ilumisoft.VolumeControl.Core
{
    using UnityEngine;

    /// <summary>
    /// Base class of all singleton MonoBehaviours
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        protected static T instance = null;

        /// <summary>
        /// Gets the active instance of the singleton
        /// </summary>
        public static T Instance => instance;

        /// <summary>
        /// Creates the instance of the singleton
        /// </summary>
        protected virtual void Awake()
        {
            //Dont allow muliple instance of a singleton
            if (instance != null)
            {
                Debug.LogError(string.Format("Multiple instances of {0} are not allowed", typeof(T)));

                Destroy(this);

                return;
            }

            instance = (T)this;
        }

        /// <summary>
        /// Resets the static instance when the object gets destroyed. 
        /// This is necessary because static fields are not resolved by the garbage collector.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}