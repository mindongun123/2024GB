using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;

namespace Mindongun
{
    public class SelectNow : SingletonComponent<SelectNow>
    {

        [SerializeField] GameObject hint;
        [SerializeField] Animation m_animation;

        public void SetPositionSelectNow(Vector2 pos)
        {
            if (!hint.activeSelf)
            {
                hint.SetActive(true);
            }
            hint.transform.position = pos;
            m_animation.Play();
        }


    }
}