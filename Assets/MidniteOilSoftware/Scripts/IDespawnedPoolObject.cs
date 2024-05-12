namespace MidniteOilSoftware
{
    /// <summary>
    /// Optional interface that can be implemented to perform cleanup actions when
    /// and object is returned to the object pool.
    /// </summary>
    public interface IDespawnedPoolObject
    {
        /// <summary>
        /// Called when the object is returned to the object pool. This provides an
        /// opportunity to perform any necessary cleanup (e.g. reset object state).
        /// </summary>
        void ReturnedToPool();
    }
}
