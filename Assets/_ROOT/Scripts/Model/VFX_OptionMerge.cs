using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using MidniteOilSoftware;
using UnityEngine;


public class VFX_OptionMerge : MonoBehaviour
{
    [SerializeField] ParticleImage p1, p2;
    private void OnEnable()
    {
        p1.Play();
        p2.Play();
        SetupStart(3);
    }

    public void SetupStart(float _time)
    {
        StartCoroutine(IEDestroy());
        IEnumerator IEDestroy()
        {
            yield return new WaitForSeconds(_time);
            ObjectPoolManager.DespawnGameObject(this.gameObject);
        }
    }
}
