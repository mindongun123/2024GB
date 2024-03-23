using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MidniteOilSoftware;
using MJGame.MergeMerchant.Lobby;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace MJGame.MergeMerchant.Charactor
{
    public enum CustomerStatus
    {
        order, wait, complete
    }

    public enum TypeCharactor
    {
        c1, c2, c3, c4, c5
    }

    public class Customer : DespawnAfterDelay, IDespawnedPoolObject
    {
        public UnityAction eventMoveCharactor;
        [SerializeField] GameObject wait;
        [SerializeField] GameObject order;
        [SerializeField] RectTransform rect;
        CUSTOMER kCustomer;
        [SerializeField, Range(10, 200)] float _speed;
        [SerializeField] ViewMessage viewMessage;

        [SerializeField] TypeCharactor typeCharactor;

        public void SetCustomer(CUSTOMER kCus)
        {
            kCustomer = kCus;
            eventMoveCharactor += StateAnimationTest;
            eventMoveCharactor?.Invoke();
            ChangeButton();
        }

        public void ChangeButton()
        {
            wait.SetActive(kCustomer.customerStatus == CustomerStatus.wait);
            order.SetActive(kCustomer.customerStatus == CustomerStatus.order);
            if (kCustomer.customerStatus == CustomerStatus.wait)
            {
                viewMessage.ChangeTypeMessage(TypeMessage.wait);
            }
        }

        public void Despawn()
        {
            SingletonComponent<CustomerController>.Instance.RemoveCustomer(this);
            ObjectPoolManager.DespawnGameObject(gameObject);
        }

        public void ReturnedToPool()
        {
            kCustomer = null;
        }


        private void OnDisable()
        {
            kCustomer._pos = transform.GetComponent<RectTransform>().localPosition;
            eventMoveCharactor -= StateAnimationTest;
        }

        public void OnClickAddProduct()
        {
            List<CUSTOMER> lsCus = SingletonComponent<SaveLobbyGame>.Instance.ListCustomerOrder;
            if (lsCus.Count >= 3 || kCustomer.customerStatus != CustomerStatus.order) return;

            CUSTOMER cus = new CUSTOMER((int)typeCharactor);
            lsCus.Add(cus);
            SingletonComponent<SaveLobbyGame>.Instance.ListCustomerOrder = lsCus;

            kCustomer.customerStatus = CustomerStatus.wait;
            ChangeButton();
            print("Complete + so luong order: " + lsCus.Count);
        }

        public void StateAnimationTest()
        {
            StartCoroutine(Move(Random.Range(5f, 20f)));
        }

        private IEnumerator Move(float _time)
        {
            if (kCustomer.customerStatus == CustomerStatus.order)
            {
                yield return new WaitForSeconds(_time);
                float _pos = Random.Range(-300, 500f);
                rect.DOAnchorPosX(_pos, _speed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
                  {
                      StartCoroutine(Move(Random.Range(8f, 20f)));
                  });
            }
            else if (kCustomer.customerStatus == CustomerStatus.complete)
            {
                rect.DOAnchorPosX(700, _speed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
                                {
                                    Despawn();
                                });
            }
        }

    }
}