namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;

    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var instances = Resources.FindObjectsOfTypeAll<T>();

                    if(instances.Length>0)
                    {
                        return instances[0];
                    }

                    instance = CreateInstance<T>();
                }

                return instance;
            }
        }

        protected void OnDisable()
        {
            instance = null;
        }
    }
}