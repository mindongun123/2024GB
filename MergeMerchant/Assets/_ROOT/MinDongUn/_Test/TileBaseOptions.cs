using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MJGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mindongun
{
    public class TileBaseOptions : MonoBehaviour, IDropHandler
    {
        [SerializeField] GridLayoutGroup gridLayoutGroup;
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                GameObject dropped = eventData.pointerDrag;
                Options ops = dropped.GetComponent<Options>();
                ops.parentAfterDrag = transform;

                ops.AnimationMergeComplete();

                // set trang thai
                Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(this);
                SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps);
            }
            else
            {
                GameObject dropped = eventData.pointerDrag;
                Options ops = dropped.GetComponent<Options>();

                ops.AnimationMergeComplete();

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

                    TileBaseOptions trsTarget = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(_idx);

                    Transform trsChild = transform.GetChild(0);
                    ops.parentAfterDrag = transform;

                    trsTarget.gridLayoutGroup.enabled = false;

                    trsChild.SetParent(trsTarget.transform);
                    trsChild.GetComponent<Options>().CheckOptionBasket();


                    trsChild.DOMove(trsTarget.transform.position, 1000f).OnComplete(() =>
                    {
                        trsTarget.gridLayoutGroup.enabled = true;
                    }).SetSpeedBased();
                }
            }
        }

    }
}