using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    private static readonly Dictionary<float, WaitForSeconds> WaitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWaitForSeconds(float seconds)
    {
        if (WaitForSeconds.ContainsKey(seconds)) return WaitForSeconds[seconds];
        var waitForSeconds = new WaitForSeconds(seconds);
        WaitForSeconds.Add(seconds, waitForSeconds);
        return WaitForSeconds[seconds];
    }
}
