using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Merge;
using UnityEngine;

namespace MJGame.MergeMerchant.House
{
    public class ViewItemRewardFloorController : SingletonComponent<ViewItemRewardFloorController>
    {
        [SerializeField] ViewItemRewardFloor[] viewItemRewardFloors;

        List<string> listItemRewardFloors = new List<string>();

        private void OnEnable()
        {
            GetListItemRewardFloor();
            SetRewardFloor();
        }

        private void GetListItemRewardFloor()
        {
            listItemRewardFloors = MJGameSave.GetList<string>(ConstGame.LIST_ITEM_REWARD_FLOOR, new List<string>() { DateTime.Now.ToString() });
        }
        public void SetRewardFloor()
        {
            for (int i = 0; i < listItemRewardFloors.Count; i++)
            {
                viewItemRewardFloors[i].SetRewardFloor(i, listItemRewardFloors[i]);
            }
        }
        public void ChangeListItemRewardFloor(int _id)
        {
            listItemRewardFloors[_id] = DateTime.Now.ToString();

            SaveListItemRewardFloor();
            viewItemRewardFloors[_id].SetRewardFloor(_id, listItemRewardFloors[_id]);
        }

        public void SaveListItemRewardFloor()
        {
            MJGameSave.SetList(ConstGame.LIST_ITEM_REWARD_FLOOR, listItemRewardFloors);
        }

        public void AddItemRewardFloor()
        {
            if (listItemRewardFloors.Count >= viewItemRewardFloors.Length) return;
            listItemRewardFloors.Add(DateTime.Now.ToString());
            SaveListItemRewardFloor();
            viewItemRewardFloors[listItemRewardFloors.Count - 1].SetRewardFloor(listItemRewardFloors.Count - 1, DateTime.Now.ToString());
        }

        private void OnDisable()
        {
            SaveListItemRewardFloor();
        }
    }
}