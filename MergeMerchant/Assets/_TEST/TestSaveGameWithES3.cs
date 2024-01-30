using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestSaveGameWithES3 : MonoBehaviour
{
    [ShowInInspector]
    public Queue<GameObject> qe = new Queue<GameObject>();
    public GameObject ts;

    [Button]
    public void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            qe.Enqueue(ts);
        }
    }



    [Button]
    public void Save()
    {
        ES3.Save<Queue<GameObject>>("test", qe);
    }

    [Button]
    public void Load()
    {
        qe = ES3.Load<Queue<GameObject>>("test", new Queue<GameObject>());
    }


    [Button]
    public void SaveObject()
    {
        string savePath = "playerData.json"; 
        // Lưu đối tượng PlayerData vào Easy Save 3
        ES3.Save<GameObject>(savePath, ts);
    }
    [Button]
    public void LoadObject()
    {
        string savePath = "playerData.json"; 

        if (ES3.FileExists(savePath))
        {
            // Tải đối tượng PlayerData từ Easy Save 3
            GameObject loadedPlayer = ES3.Load<GameObject>(savePath);

            Debug.Log("Loaded Player Name: " + loadedPlayer.name);
        }
        else
        {
            Debug.LogWarning("Save file not found at path: " + savePath);
        }
    }

}