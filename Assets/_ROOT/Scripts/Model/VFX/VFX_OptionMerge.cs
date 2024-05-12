using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using MidniteOilSoftware;
using UnityEngine;


public class VFX_OptionMerge : MonoBehaviour
{
    [SerializeField] ParticleImage p1, p2;

    public void StartVFX(float _time)
    {
        StartCoroutine(IEDestroy(_time));
    }
    IEnumerator IEDestroy(float _time)
    {
        p1.Play();
        p2.Play();
        yield return new WaitForSeconds(_time);
        ObjectPoolManager.DespawnGameObject(this.gameObject);
    }
}
