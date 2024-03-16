using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Lobby;
using Sirenix.OdinInspector;
using Sirenix.Reflection.Editor;
using UnityEngine;



namespace MJGame.MergeMerchant.Merge
{
    public class LevelGame : SingletonComponent<LevelGame>
    {
        public int Level
        {
            get => PlayerPrefs.GetInt(ConstGame.LEVEL, 1);
            set => PlayerPrefs.SetInt(ConstGame.LEVEL, value);
        }

        // public int[] arr3 = { 4, 5, 6, 11, 12, 13 }, arr7 = { 2, 3, 9, 10 }, arr11 = { 0, 1, 7, 8 }, arr16 = { 42, 43, 44, 45, 49, 50, 51, 52, 56, 57, 58, 59 }, arr19 = { 46, 47, 48, 53, 54, 55, 60, 61, 62 };
        // [SerializeField] GameObject sealObj3, sealObj7, sealObj11, sealObj16, sealObj19;

        // public void UpLevel()
        // {
        //     Level = Level + 1;
        //     switch (Level)
        //     {
        //         case 3:
        //             SetIdOptionInArrayOpen(arr3);
        //             sealObj3.SetActive(false);
        //             break;
        //         case 7:
        //             sealObj7.SetActive(false);
        //             SetIdOptionInArrayOpen(arr7);
        //             break;
        //         case 11:
        //             sealObj11.SetActive(false);
        //             SetIdOptionInArrayOpen(arr11);
        //             break;
        //         case 16:
        //             sealObj16.SetActive(false);
        //             SetIdOptionInArrayOpen(arr16);
        //             break;
        //         case 19:
        //             sealObj19.SetActive(false);
        //             SetIdOptionInArrayOpen(arr19);
        //             break;
        //         default:
        //             break;
        //     }
        // }

        public void UpdateLevel()
        {
            Level += 1;
            ConfigNotice.SaveNotifyViewBasket();
            ConfigNotice.SaveNotifyLevelPassReward(2);
            SingletonComponent<ButtonLevel>.Instance.ShowLevel();
        }



        private void SetIdOptionInArrayOpen(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                TileBaseOptions tbops = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(arr[i]);
                //xet lai ID cho cac Option khi bang duoc mo 
                tbops.transform.GetChild(0).GetComponent<Options>().Setting(0);
            }
        }
    }
}
