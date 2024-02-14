using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;
using UnityEngine.EventSystems;



namespace Mindongun
{
    public partial class Options
    {
        public Transform parentAfterDrag;
        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;

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
            
            image.raycastTarget = true;

            SingletonComponent<HintNow>.Instance.SetPositionHint(parentAfterDrag.position);
        }
    }
}
