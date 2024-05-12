using UnityEngine;

namespace MidniteOilSoftware
{
    /// <summary>
    /// Optional component that can be added to a pooled object. It implements the ReturnedToPool()
    /// method which is a good place to perform any required cleanup when the object is returned
    /// to the object pool. 
    /// </summary>
    public class DespawnedPoolObject : MonoBehaviour, IDespawnedPoolObject
    {
        public void ReturnedToPool()
        {
            // tra ve vi tri ban dau
        }
    }
}
