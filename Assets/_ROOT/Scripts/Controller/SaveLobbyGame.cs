using System;
using System.Collections.Generic;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Merge;
using UnityEngine;
using Random = UnityEngine.Random;


namespace MJGame.MergeMerchant.Lobby
{
    public class SaveLobbyGame : SingletonComponent<SaveLobbyGame>
    {
        #region  Date Time
        public string DateTimeDailyOld
        {
            set => PlayerPrefs.SetString(ConstGame.DAY_DAILY_OLD, value);
            get => PlayerPrefs.GetString(ConstGame.DAY_DAILY_OLD, DateTime.MinValue.ToString());
        }

        public void CompleteDaily()
        {
            DayDailyNext += 1;
            DateTimeDailyOld = DateTime.Now.ToString();
        }

        public int DayDailyNext
        {
            set => PlayerPrefs.SetInt(ConstGame.DAY_DAILY_NEXT, value);
            get => PlayerPrefs.GetInt(ConstGame.DAY_DAILY_NEXT, 1);
        }


        public int TimeCountDown
        {
            set => PlayerPrefs.SetInt(ConstGame.TIME_COUNT_DOWN, value);
            get => PlayerPrefs.GetInt(ConstGame.TIME_COUNT_DOWN, 0);
        }

        public string TimeDailyEnergy
        {
            set => PlayerPrefs.SetString(ConstGame.TIME_DAILY_ENERGY, value);
            get => PlayerPrefs.GetString(ConstGame.TIME_DAILY_ENERGY, DateTime.Now.AddSeconds(55).AddMinutes(29).ToString());

        }
        #endregion

        #region View Item Option   
        public List<int> ListIdOptionMax
        {
            set => MJGameSave.SetList(ConstGame.LIST_ID_MAX, value);
            get => MJGameSave.GetList(ConstGame.LIST_ID_MAX, new List<int>() { 3, -1, -1, -1, -1, -1 });
        }

        public List<Vector4> GetListViewOption()
        {
            return MJGameSave.GetList<Vector4>(ConstGame.LIST_VIEW_OPTION, new List<Vector4>() { new Vector4(1, 1, 1, 5), new Vector4(2, 1, 1, 5), new Vector4(3, 1, 0, 3) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_vt4">x: id, y: complete, z: diamond/coin, w: reward</param>
        public void SetListViewOption(Vector4 _vt4)
        {
            List<Vector4> _ls = GetListViewOption();
            _ls.Add(_vt4);
            MJGameSave.SetList<Vector4>(ConstGame.LIST_VIEW_OPTION, _ls);
        }

        public void ChangeListViewOptionComplete(int x)
        {
            List<Vector4> _ls = GetListViewOption();
            for (int i = 0; i < _ls.Count; i++)
            {
                int _id = (int)_ls[i].x;
                if (_id == x)
                {
                    _ls[i] = new Vector4(x, 0, 0, 0);
                    break;
                }
            }
            MJGameSave.SetList<Vector4>(ConstGame.LIST_VIEW_OPTION, _ls);
        }

        public void CheckIdOptionMax(int _id)
        {
            List<int> ls = ListIdOptionMax;
            int _lv = (int)(_id / 10.001f);
            int _md = (_id - _lv * 10) % 11;
            if (_md > ls[_lv])
            {
                ls[_lv] = _md;
                ListIdOptionMax = ls;
                SetListViewOption(new Vector4(_id, 1, Random.Range(0, 2), Random.Range(1, 10)));
                SingletonComponent<SpawnText>.Instance.NewText("<color=orange>new option</color>", 1.3f);
                ConfigNotice.SaveNotifyViewOption();
            }
        }

        public void SetUpdateViewOptionWhenUpdateBasket()
        {
            int IdBasket = PlayerPrefs.GetInt(ConstGame.ID_BASKET, ConstGame.ID_BASKET_DEFAULT);
            int _id = IdBasket % 10;
            CheckIdOptionMax((_id - 1) * 10 + 1);
            CheckIdOptionMax((_id - 1) * 10 + 2);
            CheckIdOptionMax((_id - 1) * 10 + 3);
        }
        #endregion

        #region View Basket
        private void AddIdOptionSpawn()
        {
            int IdBasket = PlayerPrefs.GetInt(ConstGame.ID_BASKET, ConstGame.ID_BASKET_DEFAULT);
            List<int> lsIdSpawn = MJGameSave.GetList(ConstGame.LIST_ID_OPTION_SPAWN, new List<int> { 1, 2, 3 });
            lsIdSpawn.Add((IdBasket % 10 - 1) * 10 + 1);
            lsIdSpawn.Add((IdBasket % 10 - 1) * 10 + 2);
            lsIdSpawn.Add((IdBasket % 10 - 1) * 10 + 3);
            SetListIdOptionSpawn(lsIdSpawn);
        }
        public void SetListIdOptionSpawn(List<int> _ls)
        {
            MJGameSave.SetList(ConstGame.LIST_ID_OPTION_SPAWN, _ls);
        }

        public void UpdateBasket()
        {
            int IdBasket = PlayerPrefs.GetInt(ConstGame.ID_BASKET, ConstGame.ID_BASKET_DEFAULT);
            PlayerPrefs.SetInt(ConstGame.ID_BASKET, IdBasket + 1);
            AddIdOptionSpawn();
        }

        #endregion

        #region List ID option spawn
        public List<int> GetListIdOptionSpawn()
        {
            return MJGameSave.GetList(ConstGame.LIST_ID_OPTION_SPAWN, new List<int> { 1, 2, 3 });
        }

        #endregion


        #region EXP

        public void SaveExp(float _value)
        {
            PlayerPrefs.SetFloat(ConstGame.EXP_CURRENT, _value);
        }

        public float GetExp()
        {
            return PlayerPrefs.GetFloat(ConstGame.EXP_CURRENT, 0);
        }
        #endregion



        #region Item Decor House
        public List<int> ListIdDecorHouse
        {
            get => MJGameSave.GetList<int>(ConstGame.ITEM_DECOR_HOUSE, new List<int>());
        }

        public void SaveListItemDecorHouse(List<int> _ls)
        {
            MJGameSave.SetList<int>(ConstGame.ITEM_DECOR_HOUSE, _ls);
        }

        public List<int> ListDecorOpenBuy
        {
            get => MJGameSave.GetList<int>(ConstGame.ITEM_DECOR_BUY, new List<int>() { 0, 1, 2 });
        }

        public void SaveListItemDecorOpenBuy(List<int> _ls)
        {
            MJGameSave.SetList<int>(ConstGame.ITEM_DECOR_BUY, _ls);
        }
        #endregion

        #region SPIN

        public int NumberSpin
        {
            set => PlayerPrefs.SetInt(ConstGame.NUMBER_SPIN, value);
            get => PlayerPrefs.GetInt(ConstGame.NUMBER_SPIN, 0);
        }

        #endregion


        #region LIST CUSTOMER ORDER

        public List<CUSTOMER> ListCustomerOrder
        {
            set => MJGameSave.SetList(ConstGame.LIST_CUSTOMER, value);
            get => MJGameSave.GetList(ConstGame.LIST_CUSTOMER, new List<CUSTOMER>());
        }

        #endregion

        #region Number Order Product Current
        public int NumberProductCurrent
        {
            set => PlayerPrefs.SetInt(ConstGame.NUMBER_ORDER_PRODUCT, value);
            get => PlayerPrefs.GetInt(ConstGame.NUMBER_ORDER_PRODUCT, 0);
        }
        #endregion



    }
}