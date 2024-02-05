using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public class OrderController : SingletonComponent<OrderController>
    {

        [SerializeField] Order[] lsOders;
        [ShowInInspector]
        private Queue<Order> qeOrders = new Queue<Order>();// danh sach dang cho doi -> khong quan tam
        public Dictionary<Order, DTOrdrer> dicOrderShow = new Dictionary<Order, DTOrdrer>();// danh sach dang hien thi
        public List<DTOrdrer> lsDTOrderSaveWhenExitBoard;// list luu cac order dang con lai


        private void OnEnable()
        {
            InitOrder();

            LoadDataOrderSave();
        }

        private void OnDisable()
        {
            SaveDataOrder();
        }

        private void InitOrder()
        {
            qeOrders = new Queue<Order>(lsOders);
        }

        public void OrderActive(DTOrdrer kDTOrder)
        {
            Order kOrder = qeOrders.Dequeue();
            kOrder.gameObject.SetActive(true);
            kOrder.CompleteSlot(kDTOrder._isComplete);

            dicOrderShow[kOrder] = kDTOrder;

            kOrder.SetupOrderSlot(kDTOrder);
        }




        /// <summary>
        /// Kiem tra xem con co the dat hang duoc nua khong
        /// </summary>
        /// <returns>duoc dat hang/ khong duoc dat hang</returns>
        private bool IsAcceptOder()
        {
            if (qeOrders.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// them vao danh sach cho  -> neu co nguoi dat thi hien len
        /// </summary>
        /// <param name="kOrder"></param>
        public void AddOrderWaitToQueue(Order kOrder)// hoan thanh mua ban
        {
            dicOrderShow.Remove(kOrder);
            qeOrders.Enqueue(kOrder);
        }

        #region Khach dat hang

        public void AddOrder(DTOrdrer kDTOrder)
        {
            if (IsAcceptOder())
            {
                OrderActive(kDTOrder);
            }
            else
            {
                print("no accept");
            }
        }
        #endregion


        #region  Save Data Order

        public void LoadDataOrderSave()
        {
            // {"list":[{"_coin":15,"_idSprite":31,"_isComplete":false},{"_coin":50,"_idSprite":28,"_isComplete":false}]}
            List<DTOrdrer> ls = MJGameSave.GetList(StaticGame.LIST_DATA_ORDER_SAVE, new List<DTOrdrer>());
            foreach (var item in ls)
            {
                AddOrder(item);
            }
        }


        public void SaveDataOrder()
        {
            foreach (var item in dicOrderShow.Values)
            {
                if (item != null)
                {
                    lsDTOrderSaveWhenExitBoard.Add(item);
                }
            }
            MJGameSave.SetList(StaticGame.LIST_DATA_ORDER_SAVE, lsDTOrderSaveWhenExitBoard);
        }
        #endregion



        #region Kiem tra hoan thanh Option trong Order

        [Button]
        public void CheckCompleteOptionOrder(int _id)
        {
            foreach (var item in dicOrderShow)
            {
                if (item.Value != null && _id == item.Value._idSprite && !item.Value._isComplete)
                {

                    item.Value._isComplete = true;
                    item.Key.CompleteSlot(true);

                    return;
                }
            }
        }

        #endregion
    }
}