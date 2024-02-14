using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;

namespace Mindongun
{
    public class HintNow : SingletonComponent<HintNow>
    {

        [SerializeField] GameObject hint;

        public void SetPositionHint(Vector2 pos)
        {
            if (!hint.activeSelf)
            {
                hint.SetActive(true);
            }
            hint.transform.position = pos;
        }

    }
}