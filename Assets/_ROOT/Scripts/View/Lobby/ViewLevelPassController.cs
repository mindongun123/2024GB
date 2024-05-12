using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant.Lobby
{
    public class ViewLevelPassController : MonoBehaviour
    {
        [SerializeField] List<LEVEL_DATA_REWARD> listLevelPassReward;
        [SerializeField] ViewItemLevelPass[] viewItemLevels;

        private void OnEnable()
        {
            listLevelPassReward = GetList();
            if (listLevelPassReward.Count <= 0)
            {
                SetListLevelPassReward();
            }
            ShowListLevelPassReward();
        }
        public void SetListLevelPassReward()
        {
            LEVEL_REWARD lr = new LEVEL_REWARD();
            for (int i = 0; i < viewItemLevels.Length; i++)
            {
                lr.Init();
                LEVEL_DATA_REWARD level = new LEVEL_DATA_REWARD(lr);
                level._level = i + 1;
                listLevelPassReward.Add(level);
            }
        }

        public void ShowListLevelPassReward()
        {
            for (int i = 0; i < viewItemLevels.Length; i++)
            {
                viewItemLevels[i].SetStart(listLevelPassReward[i]);
            }
        }


        public List<LEVEL_DATA_REWARD> GetList()
        {
            return MJGameSave.GetList<LEVEL_DATA_REWARD>(ConstGame.LIST_LEVEL_PASS_REWARD, new List<LEVEL_DATA_REWARD>());
        }

        public void SaveList()
        {
            MJGameSave.SetList<LEVEL_DATA_REWARD>(ConstGame.LIST_LEVEL_PASS_REWARD, listLevelPassReward);
        }
        private void OnDisable()
        {
            SaveList();
        }

    }
}