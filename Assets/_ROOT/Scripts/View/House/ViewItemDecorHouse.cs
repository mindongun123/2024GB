using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MJGame.MergeMerchant.House
{
    public class ViewItemDecorHouse : MonoBehaviour
    {
        GameObject item;
        GameObject button;
        private void OnEnable()
        {
            item = gameObject.transform.GetChild(0).gameObject;
            button = gameObject.transform.GetChild(1).gameObject;
        }

        int _id;
        public void SetIdItemDecor(int _id)
        {
            this._id = _id;
        }

        public void EnableButtonBuy(bool _is)
        {
            button.SetActive(_is);
        }

        public void EnableItemDecor()
        {
            item.transform.DOScale(Vector3.one, 0).SetEase(Ease.OutElastic);
            EnableButtonBuy(false);
        }

        public void ScaleItemDecor(Vector3 _scale, float _time = 2)
        {
            SingletonComponent<ViewItemDecorHouseController>.Instance.SaveListIdDecor(_id);
            item.transform.DOScale(_scale, _time).SetEase(Ease.OutElastic);
        }

    }
}