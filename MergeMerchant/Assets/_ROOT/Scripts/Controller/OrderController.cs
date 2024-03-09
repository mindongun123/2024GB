using System.Collections.Generic;
using MJGame.Library.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using MJGame.MergeMerchant.Utility;
using System.Linq;
using System.Collections;

namespace MJGame.MergeMerchant.Merge
{

    public class OrderController : SingletonComponent<OrderController>
    {

        [SerializeField] List<OrderProduct> ls = new List<OrderProduct>();
        // [ShowInInspector]
        public Dictionary<OrderProduct, Slot> dicOrderProduct = new Dictionary<OrderProduct, Slot>();
        // [ShowInInspector]
        public Queue<OrderProduct> qeOrderProductWait = new Queue<OrderProduct>();


        [SerializeField] BFS bfs;
        private void Start()
        {
            qeOrderProductWait = new Queue<OrderProduct>(ls);

            LoadDictionaryOrderProduct();
        }

        public void AddOrderProductToDictionary(Slot kSlot)
        {
            if (qeOrderProductWait.Count == 0)
            {
                print("Khong con vi tri de dat hang");
                return;
            }
            OrderProduct order = qeOrderProductWait.Dequeue();
            order.gameObject.SetActive(true);
            order.SetSlotOrder(kSlot);
            dicOrderProduct[order] = kSlot;
            // cap nhat complete cho cac order product
            CheckOrderProductComplete();
        }

        [Button]
        /// <summary>
        /// them moi Order Product Slot
        /// </summary>
        /// <param name="kSlot"></param>
        public void CreateNewOrderProductSlot(SlotData slot)
        {
            // SlotData sl = new SlotData();
            // sl.InitSlot();
            // AddOrderProductToDictionary(new Slot(sl));

            AddOrderProductToDictionary(new Slot(slot));
        }


        public void DeleteOrderProductFromDictionary(OrderProduct kOrder, int _id)
        {
            dicOrderProduct.Remove(kOrder);
            qeOrderProductWait.Enqueue(kOrder);

            StartCoroutine(DelayIE(1f, kOrder));

            RemoveOptionToOrderProduct(_id, kOrder);
        }

        private IEnumerator DelayIE(float _time, OrderProduct kOrder)
        {
            yield return new WaitForSeconds(_time - 0.2f);
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(kOrder.transform.position + new Vector3(0, 100, 0), kOrder.slot._coin / 10);
            yield return new WaitForSeconds(0.2f);
            /// VFX - Reward
            kOrder.gameObject.SetActive(false);
        }

        public void SaveDictionaryOrderProduct()
        {
            List<Slot> ls = dicOrderProduct.Values.ToList();
            MJGameSave.SetList<Slot>(ConstGame.SAVE_ORDER_SLOT, ls);
        }

        public void LoadDictionaryOrderProduct()
        {
            List<Slot> ls = MJGameSave.GetList<Slot>(ConstGame.SAVE_ORDER_SLOT, new List<Slot>());
            foreach (var item in ls)
            {
                AddOrderProductToDictionary(item);
            }
        }


        #region Check Order Product Complete ?
        public void CheckOrderProductComplete()
        {
            foreach (var item in dicOrderProduct)
            {
                if (GetNumberId(item.Key.slot._id) != 0)
                {
                    item.Key.Complete();
                }
                else
                {
                    item.Key.Complete(false);
                }
            }
        }


        #endregion

        #region ID in board

        private void RemoveOptionToOrderProduct(int _id, OrderProduct kOrder = null)
        {
            Vector2Int vt = bfs.FindNearestPointOptionWithID(Vector2Int.zero, _id);

            GameObject ops = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(vt.x + vt.y * ConstGame.COLUMN).transform.GetChild(0).gameObject;

            /// VFX - Option
            SingletonComponent<VFXSetItemOption>.Instance.VFXSetOptionTarget(kOrder.GetComponent<RectTransform>(), ops.transform.position, _id);

            Destroy(ops);
            bfs.SetGridAtPosition(vt, 0);
        }

        /// <summary>
        /// truong hop nay chi dung khi nguoi choi vua mua san pham moi --> sau do vao thi san pham da co tren board san roi --> nen can merge --> khi nguoi choi di chuyen thi hi vong no khong bi xoa 
        /// </summary>
        /// <param name="_id"></param>
        public void EnableCheckOption(int _id)
        {
            Vector2Int vt = bfs.FindNearestPointOptionWithID(Vector2Int.zero, _id);
            Options ops = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(vt.x + vt.y * ConstGame.COLUMN).transform.GetChild(0).gameObject.GetComponent<Options>();
            // Bat  Cai Check len  --> truong hop nay chi 
        }

        public int GetNumberId(int _id)
        {
            return PlayerPrefs.GetInt(_id.ToString(), 0);
        }

        #endregion
    }

}