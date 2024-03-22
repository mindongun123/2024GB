using UnityEngine;

public static class LoadPersistentObjects
{
    /// <summary>
    /// This will load the Managers prefab from the Resources folder, instantiate it
    /// and run DontDestroyOnLoadOnIt. The Managers prefab contains the ObjectPoolManager
    /// as a child so this effectively loads the ObjectPoolManager when the game starts
    /// and ensures that it exists across all scenes.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        var resource = Resources.Load("Managers");
        if (resource != null)
        {
            Object.DontDestroyOnLoad(Object.Instantiate(resource));
        }
    }
}
