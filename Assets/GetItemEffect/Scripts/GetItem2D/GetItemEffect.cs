using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;
using MJGame.MergeMerchant;
using MJGame;

namespace GIE
{
    public enum GetItemEffectType
    {
        Explostion_First,
        JumpAway_First,
        FlyAway,
    }

    public class GetItemEffect : SingletonComponent<GetItemEffect>
    {
        // public static GetItemEffect mInstance;

        // GetItemEffect()
        // {
        //     mInstance = this;
        // }

        // void OnDestroy()
        // {
        //     mInstance = null;
        // }

        public GetItem CreateItem(int index)
        {
            GetItem item = Instantiate(mGetItem[index].mItemTemplate, mGetItem[index].mItemTemplate.transform.parent);
            item.gameObject.SetActive(false);

            mGetItem[index].mItems.Add(item);

            return item;
        }

        void Start()
        {

            for (int i = 0; i < mGetItem.Count; i++)
            {
                mGetItem[i].mItemTemplate.gameObject.SetActive(false);

                mGetItem[i].mItems.Clear();

                for (int j = 0; j < mGetItem[i].mCacheNumber; j++)
                    CreateItem(i);
            }
        }


        public void GetItem(string item_name, int item_number, RectTransform from_where, RectTransform to_where = null, GetItemEffectType effect_type = GetItemEffectType.Explostion_First, System.Action item_arrive_action = null)
        {
            GetItem(item_name, item_number, from_where.position, to_where, effect_type, item_arrive_action);
        }
        public void GetItem(string item_name, int item_number, Vector3 from_where, RectTransform to_where = null, GetItemEffectType effect_type = GetItemEffectType.Explostion_First, System.Action item_arrive_action = null)
        {
            for (int i = 0; i < mGetItem.Count; i++)
                if (mGetItem[i].mItemName.Equals(item_name) == true)
                    GetItem(mGetItem[i], i, item_number, from_where, to_where, effect_type, item_arrive_action);
        }


        public void GetItem(Item item_config, int index, int item_number, Vector3 from_where, RectTransform to_where, GetItemEffectType effect_type, System.Action item_arrive_action)
        {
            int use_count = 0;
            for (int i = 0; i < item_config.mItems.Count; i++)
            {
                if (item_config.mItems[i].mIsInUse == false && use_count < item_number)
                {
                    use_count++;
                    item_config.mItems[i].PlayEffect(from_where, to_where == null ? item_config.mItemToWhere : to_where, effect_type, item_arrive_action);
                }
            }
        }

        [Tooltip("Random selection in the range X and Y")]
        public Vector2 mExplostionRadius = new Vector2(0.1f, 0.15f);
        [Tooltip("Spread speed")]
        public float mExplostionSpeed = 0.5f;
        [Tooltip("The speed of the flight towards the target point")]
        public float mExplostionFlySpeed = 2.5f;

        [Tooltip("The jumping lateral range is random between X and Y")]
        public Vector2 mJumpRadius = new Vector2(0.05f, 0.2f);
        [Tooltip("The height of the bounce is random between X and Y")]
        public Vector2 mJumpHeight = new Vector2(0.1f, 0.25f);
        [Tooltip("Wait time after bounce")]
        public float mJumpToFlyDuration = 0.3f;
        [Tooltip("The speed of the bounce")]
        public float mJumpSpeed = 0.4f;
        [Tooltip("The speed of the flight towards the target point")]
        public float mJumpFlySpeed = 2.5f;


        [Tooltip("Random selection in the range X and Y")]
        public Vector2 mFlyRadius = new Vector2(0.2f, 0.4f);
        [Tooltip("The speed of the flight towards the target point")]
        public float mFlySpeed = 1.5f;

        public List<Item> mGetItem = new List<Item>();


        public void SetItemLastInListGetItem(Sprite _sprite, RectTransform _target)
        {
            foreach (var item in mGetItem[mGetItem.Count - 1].mItems)
            {
                item.GetComponent<Image>().sprite = _sprite;
            }
            mGetItem[mGetItem.Count - 1].mItemToWhere = _target;
        }


    }
    [Serializable]
    public class Item
    {
        public string mItemName = "";
        public GetItem mItemTemplate;
        public RectTransform mItemToWhere;
        public int mCacheNumber = 20;
        public List<GetItem> mItems;
    }
}

