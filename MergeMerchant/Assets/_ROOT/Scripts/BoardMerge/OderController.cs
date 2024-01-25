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

        private void Start()
        {
            InitOder();
        }

        private void InitOder()
        {
            qeOders = new Queue<Oder>(lsOders);
        }

        public void OderActive(Oder kOder)
        {
            kOder.gameObject.SetActive(true);
        }

        private bool IsAcceptOder()
        {
            if (qeOders.Count > 0)
            {
                OderActive(qeOders.Dequeue());
                return true;
            }
            return false;
        }

        public void AddQueue(Oder kOder)
        {
            qeOders.Enqueue(kOder);
        }

        #region TEST IS BUY

        [Button("Add Oder")]
        public void AddCommentOder()
        {
            if (IsAcceptOder())
            {
                print("accept");
            }
            else
            {
                print("no accept");
            }
        }
        #endregion
    }
}