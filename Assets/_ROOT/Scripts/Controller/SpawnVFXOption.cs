using System.Collections;
using System.Collections.Generic;
using MidniteOilSoftware;
using MJGame;
using UnityEngine;

public class SpawnVFXOption : SingletonComponent<SpawnVFXOption>
{
    [SerializeField] GameObject vfxPrefabs;
    [SerializeField] GameObject parent;
    public void NewVFXOption(Transform trn)
    {
        GameObject vfx = ObjectPoolManager.SpawnGameObject(vfxPrefabs, trn.position, Quaternion.identity);
        vfx.transform.SetParent(parent.transform);
        vfx.transform.localScale = Vector3.one * 0.45f;
    }
}
