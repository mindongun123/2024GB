using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.MergeMerchant.Merge
{
    public class ViewBoardGameMerge : MonoBehaviour
    {
        [SerializeField] GameObject[] sealObj;

        private void Start()
        {
            CheckOpenSeal();
        }

        private int GetIndexSealOpen
        {
            get => PlayerPrefs.GetInt(ConstGame.INDEX_SEAL_OPEN, -1);
            set => PlayerPrefs.SetInt(ConstGame.INDEX_SEAL_OPEN, value);
        }

        private void CheckOpenSeal()
        {
            for (int i = 0; i <= GetIndexSealOpen; i++)
            {
                sealObj[i].SetActive(false);
            }
        }
    }
}