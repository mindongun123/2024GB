using System.Collections;
using System.Collections.Generic;
using GIE;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant.Merge
{
    public enum NameItem
    {
        coin, diamond, energy, exp, option
    }


    public class VFXParticleItem : MonoBehaviour
    {
        [SerializeField] GetItemEffect getItemEffect;
        [Button]
        public void OnClickItemVFX(Vector2 _start, int _number = 1, NameItem kNameItem = NameItem.coin, GetItemEffectType _type = GetItemEffectType.Explostion_First)
        {
            getItemEffect.GetItem(kNameItem.ToString(), _number, new Vector3(_start.x, _start.y, 0), null, _type);
        }


       
    }
}
