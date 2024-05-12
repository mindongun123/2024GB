using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIE
{

    public class GetItemEffect_3D : MonoBehaviour
    {
        public static GetItemEffect_3D mInstance;

        GetItemEffect_3D()
        {
            mInstance = this;
        }

        void OnDestroy()
        {
            mInstance = null;
        }

        public GetItem_3D CreateItem(int index)
        {
            GetItem_3D item = Instantiate(mGetItem[index].mItemTemplate, mGetItem[index].mItemTemplate.transform.parent);
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


        public void GetItem(string item_name, int item_number, Transform from_where, Transform to_where = null, System.Action item_arrive_action = null)
        {
            GetItem(item_name, item_number, from_where.position, to_where, item_arrive_action);
        }
        public void GetItem(string item_name, int item_number, Vector3 from_where, Transform to_where = null, System.Action item_arrive_action = null)
        {
            for (int i = 0; i < mGetItem.Count; i++)
                if (mGetItem[i].mItemName.Equals(item_name) == true)
                    GetItem(mGetItem[i], i, item_number, from_where, to_where, item_arrive_action);
        }


        public void GetItem(Item_3D item_config, int index, int item_number, Vector3 from_where, Transform to_where, System.Action item_arrive_action)
        {
            int use_count = 0;
            for (int i = 0; i < item_config.mItems.Count; i++)
            {
                if (item_config.mItems[i].mIsInUse == false && use_count < item_number)
                {
                    use_count++;
                    item_config.mItems[i].PlayEffect(from_where, to_where == null ? item_config.mItemToWhere : to_where,item_arrive_action);
                }
            }
        }

        public Vector3 mJumpRadius_X = new Vector2(-1f, 1f);
        public Vector3 mJumpRadius_Y = new Vector2(2f, 3f);
        public Vector3 mJumpRadius_Z = new Vector2(0f, 2f);
        public Vector2 mJumpForce = new Vector2(150f, 200f);
        public float mJumpToFlyDuration = 3f;
        public float mJumpFlySpeed = 10f;

        public List<Item_3D> mGetItem = new List<Item_3D>();
    }
    [Serializable]
    public class Item_3D
    {
        public string mItemName = "";
        public GetItem_3D mItemTemplate;
        public RectTransform mItemToWhere;
        public int mCacheNumber = 20;
        public List<GetItem_3D> mItems;
    }
}