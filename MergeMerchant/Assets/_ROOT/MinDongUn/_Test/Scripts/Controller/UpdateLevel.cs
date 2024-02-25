using System.Collections;
using System.Collections.Generic;
using MJGame.Library.Utility;
using Sirenix.Reflection.Editor;
using UnityEngine;



namespace MJGame.MergeMerchant.Merge
{
    public class UpdateLevel : SingletonComponent<UpdateLevel>
    {
        public int[] arr3 = { 4, 5, 6, 11, 12, 13 }, arr7 = { 2, 3, 9, 10 }, arr11 = { 0, 1, 7, 8 }, arr16 = { 42, 43, 44, 45, 49, 50, 51, 52, 56, 57, 58, 59 }, arr19 = { 46, 47, 48, 53, 54, 55, 60, 61, 62 };

        public int Level
        {
            get => ES3.Load<int>(ConstGame.LEVEL, 1);
            set => ES3.Save<int>(ConstGame.LEVEL, value);
        }
        
        public void UpLevel()
        {
            Level = Level + 1;
            switch (Level)
            {
                case 3:
                    ArrayLevel(arr3);
                    break;
                case 7:
                    ArrayLevel(arr7);
                    break;
                case 11:
                    ArrayLevel(arr11);
                    break;
                case 16:
                    ArrayLevel(arr16);
                    break;
                case 19:
                    ArrayLevel(arr19);
                    break;
                default:
                    break;
            }
        }

        private void ArrayLevel(int[] arr)
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
