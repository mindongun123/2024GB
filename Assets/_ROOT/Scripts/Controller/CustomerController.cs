using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MidniteOilSoftware;
using MJGame.MergeMerchant.Lobby;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MJGame.MergeMerchant.Charactor
{
    public class CustomerController : MonoBehaviour
    {
        public GameObject[] prefabsCustomer;
        public RectTransform parent;

        public Dictionary<Customer, CUSTOMER> dictCustomer = new Dictionary<Customer, CUSTOMER>();

        private void OnEnable()
        {
            LoadCustomer();
            StartCoroutine(CreateNewCustomerIE(2f));
        }

        IEnumerator CreateNewCustomerIE(float _time)
        {
            if (dictCustomer.Count < 4)
            {
                CUSTOMER cus = new CUSTOMER(Random.Range(0, prefabsCustomer.Length));
                SpawnCustomer(cus, cus._idx);
                yield return new WaitForSeconds(_time);
                StartCoroutine(CreateNewCustomerIE(2f));
            }
        }

        public void LoadCustomer()
        {
            List<CUSTOMER> listCustomer = SingletonComponent<SaveLobbyGame>.Instance.ListCustomerOrder;

            foreach (var item in listCustomer)
            {
                SpawnCustomer(item, item._idx);
            }
        }

        public void SpawnCustomer(CUSTOMER cus, int _idx)
        {
            GameObject obj = ObjectPoolManager.SpawnGameObject(prefabsCustomer[_idx], prefabsCustomer[_idx].transform.localPosition, Quaternion.identity);
            Customer customer = obj.GetComponent<Customer>();
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.SetParent(parent);
            rect.localScale = new Vector3(0.8f, 0.8f, 1);
            customer.SetCustomer(cus);
            rect.localPosition = cus._pos;
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
                    item.Value._pos = item.Key.GetComponent<RectTransform>().localPosition;
            }
            List<CUSTOMER> lsCustomer = dictCustomer.Values.ToList();
            SingletonComponent<SaveLobbyGame>.Instance.ListCustomerOrder = lsCustomer;
        }

    }
}