using System.Collections;
using System.Collections.Generic;
using MidniteOilSoftware;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant
{
    public class SpawnText : SingletonComponent<SpawnText>
    {
        [SerializeField]
        GameObject txtPrefabs;

        public void NewText(string _txt , float _time)
        {
            GameObject txt = ObjectPoolManager.SpawnGameObject(txtPrefabs);
            txt.GetComponent<TextConfig>().SetText(_txt, _time);
        }
    }
}
