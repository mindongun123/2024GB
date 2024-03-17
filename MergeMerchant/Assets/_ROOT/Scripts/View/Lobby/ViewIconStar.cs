using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewIconStar : MonoBehaviour
    {
        private void OnEnable()
        {
        }

        public RectTransform rect;
        [Button]
        public void MoveAddStar()
        {
            Vector2 pos = rect.localPosition;
            rect.DOAnchorPosX(pos.x - 120, 1f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(2f, () =>
                {
                    rect.DOAnchorPosX(pos.x, 1f);
                }).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });
        }
    }
}
