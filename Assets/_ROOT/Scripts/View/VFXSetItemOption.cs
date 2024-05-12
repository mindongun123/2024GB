using System.Collections;
using System.Collections.Generic;
using GIE;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    /// <summary>
    /// Danh cho Option
    /// </summary>
    public class VFXSetItemOption : SingletonComponent<VFXSetItemOption>
    {
        [SerializeField]
        GetItemEffect getItemEffect;
        [SerializeField] Sprite[] _sprites;
        public void VFXSetOptionTarget(RectTransform _target, Vector2 _start, int _id = 1)
        {
            getItemEffect.SetItemLastInListGetItem(_sprites[_id], _target);
            OnClickItemOptionVFX(_start);
        }

        private void OnClickItemOptionVFX(Vector2 _start, NameItem kNameItem = NameItem.option, GetItemEffectType _type = GetItemEffectType.FlyAway)
        {
            getItemEffect.GetItem(kNameItem.ToString(), 1, new Vector3(_start.x, _start.y, 0), null, _type);
        }

      
    }
}
