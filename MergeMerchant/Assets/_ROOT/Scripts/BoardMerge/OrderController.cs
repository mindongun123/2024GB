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
        public List<DTOrdrer> lsDTOrderSaveWhenExitBoard;
        // check id
        public Dictionary<int, int> dicCheck = new Dictionary<int, int>();

        private void OnEnable()
        {
            InitOder();

            LoadDataOrderSave();
        }

        private void OnDisable()
        {
            SaveDataOrder();
        }

        private void InitOder()
        {
            qeOrders = new Queue<Order>(lsOders);
        }

        public void OderActive(DTOrdrer kDTOder)
        {
            Order kOder = qeOrders.Dequeue();
            kOder.gameObject.SetActive(true);
            dicOrderShow[kOder] = kDTOder;

            SetBillOder(kOder, kDTOder);
        }

        /// <summary>
        ///  them hoa don vao du lieu
        /// </summary>
        public void SetBillOder(Order kOder, DTOrdrer kDTOder)
        {
            kOder.ShowSetupOderOnTop(kDTOder);
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
        /// <param name="kOder"></param>
        public void AddQueueOderWait(Order kOder)
        {
            dicOrderShow[kOder] = null;
            qeOrders.Enqueue(kOder);
        }

        #region IS BUY
        /// <summary>
        ///  co nguoi nao mua hang thi cu goi ham nay sau do truyen tham so vao
        /// </summary>
        /// <param name="kDTOrder"></param>
        [Button]
        public void AddCommentOrder(DTOrdrer kDTOrder)
        {
            if (IsAcceptOder())
            {
                // them yeu cau vua dat hang
                OderActive(kDTOrder);
                print("accept");
            }
            else
            {
                // het slot dat hang -> hen lan sau
                print("no accept");
            }
        }
        #endregion


        #region  Save Data Order

        [Button]
        public void LoadDataOrderSave()
        {
            // {"list":[{"_option":2,"_coin":0,"_idOptions":[12,3]},{"_option":2,"_coin":0,"_idOptions":[11,51]},{"_option":1,"_coin":0,"_idOptions":[44]}]}
            List<DTOrdrer> ls = MJGameSave.GetList(StaticGame.LIST_DATA_ORDER_SAVE, new List<DTOrdrer>());
            foreach (var item in ls)
            {
                AddCommentOrder(item);
            }
        }


        [Button]
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
        public void CheckCompleteOptionOrder(int _idOps)
        {
            if (dicCheck[_idOps] != 0)
            {
                
            }
        }
        #endregion
    }
}