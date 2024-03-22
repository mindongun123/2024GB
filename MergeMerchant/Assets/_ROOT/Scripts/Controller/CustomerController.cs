using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MidniteOilSoftware;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Merge;
using MJGame.MergeMerchant.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MJGame.MergeMerchant.Charactor
{
    public class CustomerController : MonoBehaviour
    {
        public GameObject[] prefabsCustomer;

        [ShowInInspector]
        public Dictionary<Customer, CUSTOMER> dictCustomer = new Dictionary<Customer, CUSTOMER>();

        [ShowInInspector]
        public List<Slot> lsSlotComplete = new List<Slot>();
        private void OnEnable()
        {
            LoadCustomer();
            StartCoroutine(CreateNewCustomer(2f));
        }


        IEnumerator CreateNewCustomer(float _time)
        {
            if (dictCustomer.Count < 5)
            {
                CUSTOMER cus = new CUSTOMER(Random.Range(0, prefabsCustomer.Length), new Vector3(Random.Range(0, 5), Random.Range(0, 5)));
                SpawnCustomer(cus, cus._idx);
                yield return new WaitForSeconds(_time);
                StartCoroutine(CreateNewCustomer(2f));
            }
        }

        public void LoadCustomer()
        {
            List<CUSTOMER> listCustomer = MJGameSave.GetList(ConstGame.LIST_CUSTOMER, new List<CUSTOMER>());
            lsSlotComplete = MJGameSave.GetList(ConstGame.LIST_SLOT_COMPLETE, new List<Slot>());

            foreach (var item in listCustomer)
            {
                if (lsSlotComplete.Contains(item.slot))
                {
                    print("yes");
                }
                else
                {
                    SpawnCustomer(item, item._idx);
                }
            }
            lsSlotComplete = new List<Slot>();
        }
 
        public void SpawnCustomer(CUSTOMER cus, int _idx)
        {
            GameObject obj = ObjectPoolManager.SpawnGameObject(prefabsCustomer[_idx], prefabsCustomer[_idx].transform.position, Quaternion.identity);
            Customer customer = obj.GetComponent<Customer>();
            customer.SetCustomer(cus);
            obj.gameObject.transform.position = cus._pos;
            dictCustomer.Add(customer, cus);
        }

        public void RemoveCustomer(Customer kCus)
        {
            dictCustomer.Remove(kCus);

        }

        private void OnDisable()
        {
            SaveCustomer();
        }

        public void SaveCustomer()
        {
            foreach (var item in dictCustomer)
            {
                if (item.Key != null)
                    item.Value._pos = item.Key.gameObject.transform.position;
            }
            List<CUSTOMER> lsCustomer = dictCustomer.Values.ToList();
            MJGameSave.SetList(ConstGame.LIST_CUSTOMER, lsCustomer);
        }

    }
}