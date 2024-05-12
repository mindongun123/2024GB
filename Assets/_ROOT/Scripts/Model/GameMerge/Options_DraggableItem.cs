using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;
using UnityEngine.EventSystems;



namespace MJGame.MergeMerchant.Merge
{
    public partial class Options
    {
        public Transform parentAfterDrag;
        public Transform parentAfterDragLast;

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            parentAfterDragLast = transform.parent;
            //cho TileBaseOptions tuong ung ban dau set trang thai = 0
            // vi tri obj nay tuong ung tren grid duoc set = 0
            Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(transform.parent.GetComponent<TileBaseOptions>());
            SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps, 0);

            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parentAfterDrag);

            if (parentAfterDrag == parentAfterDragLast)
            {
                image.raycastTarget = true;
                Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(transform.parent.GetComponent<TileBaseOptions>());
                SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps);
            }

            SingletonComponent<SelectNow>.Instance.SetPositionSelectNow(parentAfterDrag.position);

            SingletonComponent<ViewInformationWhenSellect>.Instance.ShowInformationText(_isBasket, ID, this, parentAfterDrag);

            SingletonComponent<AudioController>.Instance.AudioDragMergeOptiosPlay();



            CheckOptionBasket();
        }

        /// <summary>
        ///  Kiem tra Option Basket hay khong
        /// </summary>
        public void CheckOptionBasket()
        {
            if (_isBasket)
            {
                SingletonComponent<SpawnOptions>.Instance.TileBaseOptionsSelect = transform.parent.GetComponent<TileBaseOptions>();
            }
        }
    }
}
