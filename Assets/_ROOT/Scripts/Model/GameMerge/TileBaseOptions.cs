using DG.Tweening;
using MJGame.MergeMerchant.Lobby;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.Merge
{
    public class TileBaseOptions : MonoBehaviour, IDropHandler
    {
        [SerializeField] GridLayoutGroup gridLayoutGroup;
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            Options ops = dropped.GetComponent<Options>();

            if (transform.childCount == 0)
            {
                ops.parentAfterDrag = transform;

                ops.AnimationMergeComplete();
                // set trang thai
                Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(this);
                SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps);

                ops.image.raycastTarget = true;
            }
            else
            {
                if (ops.ID == transform.GetChild(0).GetComponent<Options>().ID)
                {
                    if (ViewTutorials.IsStartGame == 2)
                    {
                        ViewTutorials.eventTutorialGameMerge?.Invoke();
                    }

                    SingletonComponent<SpawnVFXOption>.Instance.NewVFXOption(transform);

                    Destroy(transform.GetChild(0).gameObject);
                    ops.parentAfterDrag = transform;

                    int _idcurrent = ops.ID + 1;

                    /// kiem tra Order Product 
                    SingletonComponent<SaveGameMerge>.Instance.SaveNumberID(_idcurrent);

                    SingletonComponent<SaveLobbyGame>.Instance.CheckIdOptionMax(_idcurrent);

                    Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(this);
                    SingletonComponent<BFS>.Instance.SetGridAtPosition(_ps);

                    ops.Setting(_idcurrent, _ps);

                    ops.image.raycastTarget = true;
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
                    trsTarget.gridLayoutGroup.enabled = false;


                    Transform trsChild = transform.GetChild(0);
                    trsChild.GetComponent<Options>().image.raycastTarget = false;

                    ops.parentAfterDrag = transform;

                    trsChild.SetParent(trsTarget.transform);

                    trsChild.GetComponent<Options>().CheckOptionBasket();

                    trsChild.DOMove(trsTarget.transform.position, 1000f).OnComplete(() =>
                    {
                        trsChild.GetComponent<Options>().image.raycastTarget = true;
                        trsTarget.gridLayoutGroup.enabled = true;
                    }).SetSpeedBased();
                    ops.image.raycastTarget = true;

                }

                ops.AnimationMergeComplete();

            }

        }

    }
}