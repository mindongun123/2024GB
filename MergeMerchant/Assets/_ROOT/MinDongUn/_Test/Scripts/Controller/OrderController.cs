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
        public void DeleteOrderProductFromDictionary(OrderProduct kOrder)
        {
            dicOrderProduct.Remove(kOrder);
            qeOrderProductWait.Enqueue(kOrder);
            kOrder.gameObject.SetActive(false);
            print("Delete Complete");
        }


        // [Button]
        public void SaveDictionaryOrderProduct()
        {
            List<Slot> ls = dicOrderProduct.Values.ToList();
            MJGameSave.SetList<Slot>(ConstGame.SAVE_ORDER_SLOT, ls);
            // print(" Save complete ");
        }


        // [Button]
        public void LoadDictionaryOrderProduct()
        {
            List<Slot> ls = MJGameSave.GetList<Slot>(ConstGame.SAVE_ORDER_SLOT, new List<Slot>());
            foreach (var item in ls)
            {
                AddOrderProductToDictionary(item);
            }
            // print(" Load complete ");
        }




        #region Check Order 
        /// <summary>
        /// kiem tra xem _id duoc dua vao co trong Dictionary Order Product khong
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool IsOptionNewInDictionaryOrderProduct(int _id)
        {
            CheckWhenMergeOptionNew(_id);
            CheckWhenMergeOptionOld(_id - 1);
            return true;
        }

        /// <summary>
        /// khi tao ra option moi
        /// </summary>
        /// <param name="_id"></param>
        private bool CheckWhenMergeOptionNew(int _id)
        {
            foreach (var item in dicOrderProduct)
            {
                if (!item.Value._complete && _id == item.Value._id)
                {
                    item.Key.Complete();
                    print($"Co {item.Key.gameObject.name} thoa man option new");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// kiem tra ca nhung option cu xem co chua de xoa di trang thai complate
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        private bool CheckWhenMergeOptionOld(int _id)
        {
            int _numberOld = 0;
            foreach (var item in dicOrderProduct)
            {
                if (item.Value._complete && _id == item.Value._id)
                {
                    item.Key.Complete(false);
                    print($"Co {item.Key.gameObject.name} thoa man option old");
                    _numberOld = _numberOld + 1;
                }
                if (_numberOld == 2)
                {
                    return true;
                }
            }
            return false;
        }


        public void Check()
        {
            for (int i = 0; i < ConstGame.COLUMN; i++)
            {
                for (int j = 0; j < ConstGame.ROW; j++)
                {
                    if(SingletonComponent<BFS>.Instance.LoadStatusGrid(i, j)==1)
                    {
                        //Lay TileBase de lay ID
                        
                    }
                }
            }
        }

        #endregion
    }

}