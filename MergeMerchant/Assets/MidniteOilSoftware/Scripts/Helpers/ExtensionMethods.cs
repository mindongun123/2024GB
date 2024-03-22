using UnityEngine;

public static class ExtensionMethods 
{
    /// <summary>
    /// Extension method to strip "(Clone)" from the end of a GameObject name.
    /// </summary>
    /// <param name="go">The <see cref="GameObject"/> from which to strip "(Clone)" from the name.</param>
    /// <returns>The modified name of the specified GameObject.</returns>
    public static string BaseName(this GameObject go)
    {
        if (go == null) return string.Empty;
        return go.name.Replace("(Clone)", string.Empty) ?? string.Empty;
    }
}
