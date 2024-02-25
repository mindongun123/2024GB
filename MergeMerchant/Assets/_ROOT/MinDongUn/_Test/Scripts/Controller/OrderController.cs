using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using MJGame.MergeMerchant.Utility;
using System.Linq;

namespace MJGame.MergeMerchant.Merge
{

    public class OrderController : SingletonComponent<OrderController>
    {

        [SerializeField] List<OrderProduct> ls = new List<OrderProduct>();
        [ShowInInspector]
        public Dictionary<OrderProduct, Slot> dicOrderProduct = new Dictionary<OrderProduct, Slot>();
        [ShowInInspector]
        public Queue<OrderProduct> qeOrderProductWait = new Queue<OrderProduct>();
        private void Start()
        {
            qeOrderProductWait = new Queue<OrderProduct>(ls);

            LoadDictionaryOrderProduct();
        }

        /// <summary>
        /// Sau khi co nguoi den Order Product thi cu nhet yeu cau cua ho vao day --> Thanh cong
        /// </summary>
        /// <param name="kSlot"></param>
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
            AddOrderProductToDictionary(new Slot(slot));
        }

        // [Button]
        public void DeleteOrderProductFromDictionary(OrderProduct kOrder, int _id)
        {
            dicOrderProduct.Remove(kOrder);
            qeOrderProductWait.Enqueue(kOrder);
            kOrder.gameObject.SetActive(false);
            print("Delete Complete");

            RemoveOptionToOrderProduct(_id);
        }


        // [Button]
        public void SaveDictionaryOrderProduct()
        {
            List<Slot> ls = dicOrderProduct.Values.ToList();
            MJGameSave.SetList<Slot>(ConstGame.SAVE_ORDER_SLOT, ls);
        }


        // [Button]
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

        #region Save ID in board

        public void RemoveOptionToOrderProduct(int _id)
        {
            Vector2Int vt = SingletonComponent<BFS>.Instance.FindNearestPointOptionWithID(Vector2Int.zero, _id);

            Destroy(SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(vt.x + vt.y * ConstGame.COLUMN).transform.GetChild(0).gameObject);

            SingletonComponent<BFS>.Instance.SetGridAtPosition(vt, 0);
        }

        /// <summary>
        /// truong hop nay chi dung khi nguoi choi vua mua san pham moi --> sau do vao thi san pham da co tren board san roi --> nen can merge --> khi nguoi choi di chuyen thi hi vong no khong bi xoa 
        /// </summary>
        /// <param name="_id"></param>
        public void EnableCheckOption(int _id)
        {
            Vector2Int vt = SingletonComponent<BFS>.Instance.FindNearestPointOptionWithID(Vector2Int.zero, _id);
            Options ops = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(vt.x + vt.y * ConstGame.COLUMN).transform.GetChild(0).gameObject.GetComponent<Options>();
            // Bat  Cai Check len  --> truong hop nay chi 
        }

        /// <summary>
        /// lay ra so luong co ID can lay
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public int GetNumberId(int _id)
        {
            return PlayerPrefs.GetInt(_id.ToString(), 0);
        }

        #endregion
    }

}