using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MidniteOilSoftware;
using MJGame.MergeMerchant.Lobby;
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

        private void ChangeButton()
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
            int _nub = SingletonComponent<SaveLobbyGame>.Instance.NumberProductCurrent;
            if (_nub >= 4 || kCustomer.customerStatus != CustomerStatus.order) return;

            List<CUSTOMER> lsCus = SingletonComponent<SaveLobbyGame>.Instance.ListCustomerOrder;
            CUSTOMER cus = new CUSTOMER((int)typeCharactor);
            lsCus.Add(cus);
            SingletonComponent<SaveLobbyGame>.Instance.ListCustomerOrder = lsCus;
            SingletonComponent<SaveLobbyGame>.Instance.NumberProductCurrent = _nub + 1;

            kCustomer.customerStatus = CustomerStatus.wait;
            ChangeButton();
        }

        public void StateAnimationTest()
        {
            StartCoroutine(MoveIE(Random.Range(5f, 20f)));
        }

        private IEnumerator MoveIE(float _time)
        {
            if (kCustomer.customerStatus == CustomerStatus.order)
            {
                yield return new WaitForSeconds(_time);
                float _pos = Random.Range(-300, 500f);
                rect.DOAnchorPosX(_pos, _speed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
                  {
                      StartCoroutine(MoveIE(Random.Range(8f, 20f)));
                  });
            }
            else if (kCustomer.customerStatus == CustomerStatus.wait)
            {
                yield return new WaitForSeconds(_time);
                float _pos = Random.Range(-300, 100f);
                rect.DOAnchorPosX(_pos, _speed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
                  {
                      StartCoroutine(MoveIE(Random.Range(10f, 20f)));
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