using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace GIE
{
    public class GetItem_3D : MonoBehaviour
    {
        public Transform mTransform;
        public bool mIsInUse = false;
        public Rigidbody mRigibody;
        System.Action mArriveAction;
        Transform mToWhere;

        public void PlayEffect(Vector3 from_where, Transform to_where, System.Action item_arrive_action)
        {
            mIsInUse = true;
            gameObject.SetActive(true);

            mTransform.position = from_where;
            mToWhere = to_where;
            mArriveAction = item_arrive_action;


            JumpAway();
        }

        void JumpAway()
        {
            float x = Random.Range(GetItemEffect_3D.mInstance.mJumpRadius_X.x, GetItemEffect_3D.mInstance.mJumpRadius_X.y);
            float y = Random.Range(GetItemEffect_3D.mInstance.mJumpRadius_Y.x, GetItemEffect_3D.mInstance.mJumpRadius_Y.y);
            float z = Random.Range(GetItemEffect_3D.mInstance.mJumpRadius_X.x, GetItemEffect_3D.mInstance.mJumpRadius_Z.y);
            
            float height = Random.Range(GetItemEffect_3D.mInstance.mJumpForce.x, GetItemEffect_3D.mInstance.mJumpForce.y);

            mRigibody.isKinematic = false;
            mRigibody.AddForce(new Vector3(x,y,z) * height, ForceMode.Force);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(GetItemEffect_3D.mInstance.mJumpToFlyDuration);
            sequence.AppendCallback(() =>
            {
                float fly_duration = Vector3.Distance(mToWhere.position, mTransform.position + transform.position) / GetItemEffect_3D.mInstance.mJumpFlySpeed;
                mTransform.DOMove(mToWhere.position, fly_duration).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    if (mArriveAction != null) mArriveAction();

                    mIsInUse = false;
                    gameObject.SetActive(false);
                    mRigibody.isKinematic = true;
                });
            });
        }

        public Vector3 mAxe = Vector3.up;
        public float mSpeed = 10;

        void Update()
        {
            transform.RotateAround(transform.localPosition, mAxe, mSpeed);
        }
    }
}
