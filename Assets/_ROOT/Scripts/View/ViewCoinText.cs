using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant
{

    public class ViewCoinText : MonoBehaviour
    {
        public TextMeshProUGUI text;

          int _last = 0;


        private void OnEnable()
        {
            _last = ViewReward.Coin;
            text.text = _last.ToString();
            ViewReward.OnCoinChanged += OnCoinChanged;
        }

        private void OnDisable()
        { 
            ViewReward.OnCoinChanged -= OnCoinChanged;
        }


        private void OnCoinChanged(int _to)
        {
            StartCoroutine(IncreaseScoreIE(_last, _to, 1.0f));

            _last = _to;
        }

        IEnumerator IncreaseScoreIE(int from, int to, float duration)
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