using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace MJGame.MergeMerchant.Charactor
{

    public class SpineAnimation : MonoBehaviour
    {
        public SkeletonGraphic skeleton;

        public string idle_01 = "Idle_01";
        public string idle_02 = "Idle_02";
        public string idle_03 = "Idle_03";
        public string run_01 = "Run_01";
        public string run_02 = "Run_02";
        public string making_01 = "Making_01";
        public string making_02 = "Making_02";
        public string making_03 = "Making_03";
        public string making_04 = "Making_04";
        public string active_01 = "Active_01";
        public string active_02 = "Active_02";

        [SerializeField] List<string> stateAnimation = new List<string>();

        void Start()
        {
            stateAnimation = GetAnimationStates(skeleton);
        }

        private List<string> GetAnimationStates(SkeletonGraphic skeleton)
        {
            List<string> animationStates = new List<string>();

            if (skeleton != null && skeleton.Skeleton != null)
            {
                foreach (var animationStateData in skeleton.Skeleton.Data.Animations)
                {
                    animationStates.Add(animationStateData.Name);
                }
            }
            return animationStates;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PlayAnimation(idle_01);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                PlayAnimation(idle_02);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                PlayAnimation(idle_03);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                PlayAnimation(run_01);
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                PlayAnimation(run_02);
            }
            else if (Input.GetKeyUp(KeyCode.M))
            {
                PlayAnimation(making_01);
            }
            else if (Input.GetKeyUp(KeyCode.N))
            {
                PlayAnimation(making_02);
            }
            else if (Input.GetKeyUp(KeyCode.B))
            {
                PlayAnimation(making_03);
            }
            else if (Input.GetKeyUp(KeyCode.V))
            {
                PlayAnimation(making_04);
            }

            else if (Input.GetKeyUp(KeyCode.T))
            {
                PlayAnimation(active_01);
            }

            else if (Input.GetKeyUp(KeyCode.U))
            {
                PlayAnimation(active_02);
            }
        }

        void PlayAnimation(string animationName)
        {
            skeleton.AnimationState.SetAnimation(0, animationName, true);
        }

    }
}