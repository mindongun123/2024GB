using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindongun
{
    public class TileBaseOptions : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                GameObject dropped = eventData.pointerDrag;
                Options ops = dropped.GetComponent<Options>();
                ops.parentAfterDrag = transform;

                // set trang thai
                Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(this);
                SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps);
            }
            else
            {
                GameObject dropped = eventData.pointerDrag;
                Options ops = dropped.GetComponent<Options>();
                if (ops.ID == transform.GetChild(0).GetComponent<Options>().ID)
                {
                    Destroy(transform.GetChild(0).gameObject);
                    ops.parentAfterDrag = transform;
                    ops.Setting(ops.ID + 1);

                    Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(this);
                    SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps);
                }
                else
                {

                    // vi tri obj nay tuong ung tren grid duoc set = 1
                    Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(this);
                    SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps);

                    // chuyen phan tu con ban dau cua tilebaseoptions nay den vi tri gan nhat co tilebaseoptions = 0
                    Vector2Int _pslast = SingletonComponent<BFS>.Instance.FindNearestEmptyPosition(_ps);
                    SingletonComponent<BFS>.Instance.SetGridAtPosition(_pslast);
                    int _idx = _pslast.x + _pslast.y * 7;

                    transform.GetChild(0).SetParent(SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(_idx).transform);

                    // set phan tu moi
                    ops.parentAfterDrag = transform;
                }
            }
        }

    }
}