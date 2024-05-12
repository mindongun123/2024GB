using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using UnityEngine;

namespace MJGame.MergeMerchant.Merge
{
    public partial class Options
    {

        public Animator m_animatorBasket;
        public Animation m_animationMergeComplete;
        public void AnimatorBasket()
        {
            m_animatorBasket.SetBool("Basket", true);
        }
        public void AnimationMergeComplete()
        {
            if (_isBasket) return;
            m_animationMergeComplete.Play();
        }

    }
}
