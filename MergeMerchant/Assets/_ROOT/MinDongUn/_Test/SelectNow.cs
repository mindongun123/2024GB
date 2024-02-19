using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;

namespace Mindongun
{
    public class SelectNow : SingletonComponent<SelectNow>
    {

        [SerializeField] GameObject select;
        [SerializeField] Animation m_animation;

        public void SetPositionSelectNow(Vector2 pos)
        {
            if (!select.activeSelf)
            {
                select.SetActive(true);
            }
            select.transform.position = pos;
           
           
            if (m_animation.isPlaying)
            {
                m_animation.Stop();
                m_animation.Rewind();
            }
            m_animation.Play();





        }


    }
}