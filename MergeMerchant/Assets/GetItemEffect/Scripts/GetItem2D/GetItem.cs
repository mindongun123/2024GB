using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace GIE
{

    public class GetItem : MonoBehaviour
    {
        public RectTransform mRect;
        public bool mIsInUse = false;

        System.Action mArriveAction;
        RectTransform mToWhere;

        public void PlayEffect(Vector3 from_where, RectTransform to_where, GetItemEffectType effect_type, System.Action item_arrive_action)
        {
            mIsInUse = true;
            gameObject.SetActive(true);

            mRect.position = from_where;
            mToWhere = to_where;
            mArriveAction = item_arrive_action;


            if (effect_type == GetItemEffectType.Explostion_First)
                Explostion();
            else if (effect_type == GetItemEffectType.JumpAway_First)
                JumpAway();
            else if (effect_type == GetItemEffectType.FlyAway)
                FlyAway();
        }

        
        void Explostion()
        {
            float angle = Random.Range(0f , 360f );
            float radius = Random.Range( GetItemEffect.mInstance.mExplostionRadius.x, GetItemEffect.mInstance.mExplostionRadius.y) * Screen.width;
            Vector3 exp_position = new Vector3(radius * Mathf.Sin(angle), radius * Mathf.Cos(angle), 0);

            float exp_duration = radius / (GetItemEffect.mInstance.mExplostionSpeed * Screen.width);

            float fly_duration = Vector3.Distance(mToWhere.position, exp_position) / (GetItemEffect.mInstance.mExplostionFlySpeed * Screen.width);

            Sequence sequence = DOTween.Sequence();
            sequence.Append( mRect.DOMove( mRect.position + exp_position, exp_duration).SetEase(Ease.OutCubic) );
            sequence.Append(mRect.DOMove(mToWhere.position, fly_duration).SetEase(Ease.InCubic) );
            sequence.AppendCallback( ()=>
            {
                if(mArriveAction != null) mArriveAction();

                mIsInUse = false;
                gameObject.SetActive(false);
            } );
        }


        void JumpAway()
        {
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(GetItemEffect.mInstance.mJumpRadius.x, GetItemEffect.mInstance.mJumpRadius.y) * Screen.width;
            float height = Random.Range(GetItemEffect.mInstance.mJumpHeight.x, GetItemEffect.mInstance.mJumpHeight.y) * Screen.width;

            Vector3 jump_position = new Vector3(angle < 180 ? radius : - radius, 0 , 0);
            float jump_duration = height / (GetItemEffect.mInstance.mJumpSpeed * Screen.width);

            float fly_duration = Vector3.Distance(mToWhere.position, mRect.position + jump_position) / (GetItemEffect.mInstance.mJumpFlySpeed * Screen.width);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(mRect.DOJump(mRect.position + jump_position, height,1, jump_duration) );
            sequence.AppendInterval(GetItemEffect.mInstance.mJumpToFlyDuration);
            sequence.Append(mRect.DOMove(mToWhere.position, fly_duration).SetEase(Ease.InCubic));
            sequence.AppendCallback(() =>
            {
                if (mArriveAction != null) mArriveAction();

                mIsInUse = false;
                gameObject.SetActive(false);
            });
        }


        void FlyAway()
        {
            float angle = Random.Range(0f, 360f);
            float radius = Random.Range(GetItemEffect.mInstance.mFlyRadius.x, GetItemEffect.mInstance.mFlyRadius.y) * Screen.width;
            Vector3 exp_position = mRect.position +  new Vector3(radius * Mathf.Sin(angle), radius * Mathf.Cos(angle), 0);

            Vector3[] pos_list = new Vector3[] { mRect.position,  exp_position ,  mToWhere.position };
            float duration = (Vector3.Distance(mRect.position, exp_position) + Vector3.Distance(mToWhere.position, exp_position)) / (GetItemEffect.mInstance.mFlySpeed * Screen.width);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(mRect.DOPath(pos_list, duration, PathType.CatmullRom).SetEase(Ease.Linear) );
            sequence.AppendCallback(() =>
            {
                if (mArriveAction != null) mArriveAction();

                mIsInUse = false;
                gameObject.SetActive(false);
            });
        }




    }

}
