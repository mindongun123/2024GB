using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Ilumisoft.VisualStateMachine;
using MJGame.MergeMerchant.Lobby;
using UnityEngine;


namespace MJGame.MergeMerchant.House
{
    public class ViewItemDecorHouseController : SingletonComponent<ViewItemDecorHouse>
    {
        [SerializeField] ViewItemDecorHouse[] itemDecorHouses;
        [SerializeField] GameObject[] levelDecorHouse;
        List<int> lsIdDecorHouse;
        List<int> lsIdDecorBuy;
        public void GetListIdDecor()
        {
            lsIdDecorBuy = SingletonComponent<SaveLobbyGame>.Instance.ListDecorOpenBuy;

            lsIdDecorHouse = SingletonComponent<SaveLobbyGame>.Instance.ListIdDecorHouse;
        }

        public void SaveListIdDecor(int _idx)
        {
            lsIdDecorHouse.Add(_idx);
            SingletonComponent<SaveLobbyGame>.Instance.SaveListItemDecorHouse(lsIdDecorHouse);

            lsIdDecorBuy.Remove(_idx);
            SingletonComponent<SaveLobbyGame>.Instance.SaveListItemDecorOpenBuy(lsIdDecorBuy);

            CheckListDecorBuy();
        }


        private void OnEnable()
        {
            GetListIdDecor();

            SetIdViewItemDecorHouse();
            int _idxM = lsIdDecorHouse.Count > 0 ? lsIdDecorHouse.Max() : 0;

            SetLevelDecorHouse((_idxM + 1) / 15);

            SetItemDecorHouse();

            SetItemDecorOpenBuy();
        }

        private void SetIdViewItemDecorHouse()
        {
            for (int i = 0; i < itemDecorHouses.Length; i++)
            {
                itemDecorHouses[i].SetIdItemDecor(i);
            }
        }

        private void SetItemDecorOpenBuy()
        {
            foreach (var item in lsIdDecorBuy)
            {
                itemDecorHouses[item].gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Da duoc mo
        /// </summary>
        private void SetItemDecorHouse()
        {
            foreach (var item in lsIdDecorHouse)
            {
                itemDecorHouses[item].gameObject.SetActive(true);
                itemDecorHouses[item].EnableItemDecor();
            }
        }

        private void SetLevelDecorHouse(int _idx)
        {
            _idx = _idx > levelDecorHouse.Length - 1 ? levelDecorHouse.Length - 1 : _idx;

            for (int i = 1; i <= _idx; i++)
            {
                levelDecorHouse[i].SetActive(true);
                levelDecorHouse[i].transform.DOScaleY(1, 0f);
            }
        }

        private void CheckListDecorBuy()
        {
            if (lsIdDecorBuy.Count > 0) return;
            int _idx = lsIdDecorHouse.Max();

            if (_idx >= itemDecorHouses.Length - 1) return;// het co

            if ((_idx + 1) % 15 == 0)
            {
                UpdateLevelDecorHouse(_idx);
                return;
            }
            AddNewMiniLevelDecorOpen(_idx);
        }

        private void UpdateLevelDecorHouse(int _idx)
        {
            int _level = (_idx + 1) / 15;
            levelDecorHouse[_level].SetActive(true);
            levelDecorHouse[_level].transform.DOScaleY(1, 1f).SetEase(Ease.OutElastic).OnComplete(() =>
            {
                AddNewMiniLevelDecorOpen(_idx);

                SingletonComponent<ViewItemEnergyController>.Instance.AddTimeRemaining();
            });
        }

        private void AddNewMiniLevelDecorOpen(int _idx)
        {
            lsIdDecorBuy.Add(_idx + 1);
            lsIdDecorBuy.Add(_idx + 2);
            lsIdDecorBuy.Add(_idx + 3);
            SingletonComponent<SaveLobbyGame>.Instance.SaveListItemDecorOpenBuy(lsIdDecorBuy);

            SetItemDecorOpenBuy();
        }

        public StateMachine stateMachine;
        public void OpenViewGoldPlay()
        {
            stateMachine.Trigger("OpenGoldPlay");
        }
    }
}