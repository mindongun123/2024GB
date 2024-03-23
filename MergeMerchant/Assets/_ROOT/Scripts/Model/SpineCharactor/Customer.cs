using System.Collections;
using System.Collections.Generic;
using MidniteOilSoftware;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Lobby;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Charactor
{
    public enum CustomerStatus
    {
        order, wait, complete
    }


    public class Customer : DespawnAfterDelay, IDespawnedPoolObject
    {
        [SerializeField] GameObject wait;
        [SerializeField] GameObject order;
        CUSTOMER kCustomer;

        public void SetCustomer(CUSTOMER kCus)
        {
            kCustomer = kCus;
        }

        public void Despawn()
        {
            SingletonComponent<CustomerController>.Instance.RemoveCustomer(this);
            ObjectPoolManager.DespawnGameObject(gameObject);
        }

        public void ReturnedToPool()
        {
        }

        private void OnDisable()
        {
            kCustomer._pos = transform.position;
        }

        [Button]
        public void OnClickAddProduct()
        {
            if (SingletonComponent<SaveLobbyGame>.Instance.ListCustomerOrder.Count >= 3 || kCustomer.customerStatus != CustomerStatus.order) return;
            kCustomer.customerStatus = CustomerStatus.wait;
        }

    }
}