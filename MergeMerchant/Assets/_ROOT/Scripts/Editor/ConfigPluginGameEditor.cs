using UnityEngine;
using UnityEditor;
using System.IO;


public class MJGameConfigES3_ToolsWindows : EditorWindow
{
    [MenuItem("MJGame/MJGame Clear Data ES3", false, 200)]
    public static void MJGameClearDataES3()
    {
         System.IO.DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);

        foreach (FileInfo file in di.GetFiles())
            file.Delete();
        foreach (DirectoryInfo dir in di.GetDirectories())
            dir.Delete(true);

        PlayerPrefs.DeleteAll();
        Debug.Log("Complete");
    }
}
