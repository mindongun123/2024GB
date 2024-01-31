using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    public class OderController : SingletonComponent<OderController>
    {

        [SerializeField] Oder[] lsOders;
        [ShowInInspector]
        private Queue<Oder> qeOders = new Queue<Oder>();

        public Dictionary<Oder, DTOder> dicBillOder = new Dictionary<Oder, DTOder>();// ghi lai du lieu duoc dat hang

        private void Start()
        {
            InitOder();
        }

        private void InitOder()
        {
            qeOders = new Queue<Oder>(lsOders);
        }

        public void OderActive(DTOder kDTOder)
        {
            Oder kOder = qeOders.Dequeue();
            kOder.gameObject.SetActive(true);

            SetBillOder(kOder, kDTOder);
        }

        /// <summary>
        ///  them hoa don vao du lieu
        /// </summary>
        public void SetBillOder(Oder kOder, DTOder kDTOder)
        {
            dicBillOder.Add(kOder, kDTOder);
            kOder.SetupOder(kDTOder);
        }



        /// <summary>
        /// Kiem tra xem con co the dat hang duoc nua khong
        /// </summary>
        /// <returns>duoc dat hang/ khong duoc dat hang</returns>
        private bool IsAcceptOder()
        {
            if (qeOders.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// them vao danh sach cho  -> neu co nguoi dat thi hien len
        /// </summary>
        /// <param name="kOder"></param>
        public void AddQueueOderWait(Oder kOder)
        {
            qeOders.Enqueue(kOder);
        }

        #region TEST IS BUY

        [Button("Add New oder")]
        /// <summary>
        /// Co them don hang moi
        /// </summary>
        /// <param name="kDTOder">Du lieu don hang</param>
        public void AddCommentOder(DTOder kDTOder)
        {
            if (IsAcceptOder())
            {
                // them yeu cau vua dat hang
                OderActive(kDTOder);
                print("accept");
            }
            else
            {
                // het slot dat hang -> hen lan sau
                print("no accept");
            }
        }
        #endregion
    }
}