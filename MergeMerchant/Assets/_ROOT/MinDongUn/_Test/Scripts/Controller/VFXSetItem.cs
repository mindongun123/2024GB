using System.Collections;
using System.Collections.Generic;
using GIE;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public class VFXSetItem : MonoBehaviour
    {
        [SerializeField]
        GetItemEffect getItemEffect;
        [SerializeField] Sprite[] _sprites;
  
        [Button]
        /// <summary>
        /// Hieu ung khi Order Option Complete -> Tim vi tri Option gan nhat thoa man de xoa  -> Position ->particle 
        /// </summary>
        /// <param name="_target">La vi tri minh Order Complete Click</param>
        /// <param name="_id">Id cua Option xoa</param>
        public void SetOptionTarget(RectTransform _target, Vector2 _start, int _id = 1)
        {
            getItemEffect.SetItemLastInListGetItem(_sprites[_id], _target);
            // Test
            OnClickItemOptionVFX(SingletonComponent<SelectNow>.Instance.GetPositionObjectSelect/*Vi tri option_id xoa*/);
        }
        
        private void OnClickItemOptionVFX(Vector2 _start, NameItem kNameItem = NameItem.option, GetItemEffectType _type = GetItemEffectType.Explostion_First)
        {
            GetItemEffect.mInstance.GetItem(kNameItem.ToString(), 1, new Vector3(_start.x, _start.y, 0), null, _type);
        }
    }
}
