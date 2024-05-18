// #if UNITY_FIREBASE


using System.Collections;
using System.Collections.Generic;
using ES3Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJGame.MergeMerchant.MiniGame
{
    public class ItemMiniGame : MonoBehaviour
    {
        int _id;
        [SerializeField] Sprite[] sprites;
        [SerializeField] Image image;
        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public void SetItem(int _id)
        {
            ID = _id;
            image.sprite = sprites[ID];
        }
        public void OnClickItem()
        {
            if (SingletonComponent<MiniGameController>.Instance.CheckIsStart())
            {
                SingletonComponent<MiniGameController>.Instance.CheckDone(ID, this);
            }
        }
        public void OnDestroyObject()
        {
            gameObject.SetActive(false);
        }
    }
}

// #endif