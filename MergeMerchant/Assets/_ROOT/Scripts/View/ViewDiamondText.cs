using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant
{

    public class ViewDiamondText : MonoBehaviour
    {
        public TextMeshProUGUI text;

        int _last = 0;


        private void OnEnable()
        {
            _last = ViewReward.Diamond;
            text.text = _last.ToString();
            ViewReward.OnDiamondChange += OnDiamondChange;
        }

        private void OnDisable()
        {
            ViewReward.OnDiamondChange -= OnDiamondChange;
        }

        private void OnDiamondChange(int _to)
        {
            StartCoroutine(IncreaseRewardIE(_last, _to, 1.0f));

            _last = _to;
        }

        IEnumerator IncreaseRewardIE(int from, int to, float duration)
        {
            float timeElapsed = 0.0f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;

                int value = (int)Mathf.Lerp(from, to, timeElapsed / duration);

                text.text = value.ToString();

                yield return null;
            }
        }

    }

}