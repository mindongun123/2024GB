using System.Collections.Generic;
using Ilumisoft.VisualStateMachine;
using MJGame.MergeMerchant.House;
using MJGame.MergeMerchant.Lobby;
using UnityEngine;


namespace MJGame.MergeMerchant.Merge
{
    public class LevelGame : SingletonComponent<LevelGame>
    {
        [SerializeField] StateMachine stateMachine;

        public int[] arr3 = { 4, 5, 6, 11, 12, 13 }, arr7 = { 2, 3, 9, 10 }, arr11 = { 0, 1, 7, 8 }, arr16 = { 42, 43, 44, 45, 49, 50, 51, 52, 56, 57, 58, 59 }, arr19 = { 46, 47, 48, 53, 54, 55, 60, 61, 62 };


        public int Level
        {
            get => PlayerPrefs.GetInt(ConstGame.LEVEL, 1);
            set => PlayerPrefs.SetInt(ConstGame.LEVEL, value);
        }


        public void UpdateLevel()
        {
            Level += 1;

            ConfigNotice.SaveNotifyViewBasket();

            ConfigNotice.SaveNotifyLevelPassReward(2);

            SingletonComponent<ButtonLevel>.Instance.ShowLevel();
            stateMachine.Trigger("OpenNewLevel");

            SingletonComponent<SaveLobbyGame>.Instance.NumberSpin += 3;
            ConfigNotice.eventNoticeRoulette?.Invoke();
            ConfigNotice.eventUnlockRoulette?.Invoke(Level);

            SingletonComponent<ViewItemRewardFloorController>.Instance.AddItemRewardFloor();

            ChangeOptionWhenOpenSeal(Level);

        }

        private int GetIndexSealOpen
        {
            get => PlayerPrefs.GetInt(ConstGame.INDEX_SEAL_OPEN, -1);
            set => PlayerPrefs.SetInt(ConstGame.INDEX_SEAL_OPEN, value);
        }
        public void ChangeOptionWhenOpenSeal(int _level)
        {
            switch (_level)
            {
                case 3:
                    SetIdOptionInArrayOpen(arr3);
                    GetIndexSealOpen = 0;
                    break;
                case 7:
                    SetIdOptionInArrayOpen(arr7);
                    GetIndexSealOpen = 1;
                    break;
                case 11:
                    SetIdOptionInArrayOpen(arr11);
                    GetIndexSealOpen = 2;
                    break;
                case 16:
                    SetIdOptionInArrayOpen(arr16);
                    GetIndexSealOpen = 3;
                    break;
                case 19:
                    SetIdOptionInArrayOpen(arr19);
                    GetIndexSealOpen = 4;
                    break;
            }
        }

        private void SetIdOptionInArrayOpen(int[] arr)
        {
            Dictionary<Vector2Int, Vector2Int> dicSaveBoard = ES3.Load<Dictionary<Vector2Int, Vector2Int>>(ConstGame.SAVE_BOARD, new Dictionary<Vector2Int, Vector2Int>());
            List<int> ls = SingletonComponent<SaveLobbyGame>.Instance.GetListIdOptionSpawn();

            foreach (var item in arr)
            {
                Vector2Int vts = ConvertToVectorInGrid(item);
                int _id = ls[Random.Range(0, ls.Count)];
                dicSaveBoard[vts] = new Vector2Int(1, _id);
                PlayerPrefs.SetInt(_id.ToString(), PlayerPrefs.GetInt(_id.ToString()) + 1);
            }

            ES3.Save<Dictionary<Vector2Int, Vector2Int>>(ConstGame.SAVE_BOARD, dicSaveBoard);
        }

        private Vector2Int ConvertToVectorInGrid(int _idx)
        {
            return new Vector2Int(_idx % 7, _idx / 7);
        }
    }
}
