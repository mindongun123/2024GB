using UnityEngine;

namespace MidniteOilSoftware
{
    /// <summary>
    /// Optional interface for performing any special actions when an
    /// object is retrieved from the object pool.
    /// </summary>
    public interface IRetrievedPoolObject
    {
        /// <summary>
        /// Called when the object is retrieved from the object pool. This
        /// is a good play to perform any special actions before activating
        /// the retrieved object.
        /// </summary>
        /// <param name="prefab">The prefab for this object. This should contain sane default values.</param>
        void RetrievedFromPool(GameObject prefab);
    }
}
