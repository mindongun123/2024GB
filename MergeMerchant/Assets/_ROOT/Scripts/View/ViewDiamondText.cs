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
            print("in d");
            ViewReward.OnDiamondChange += OnDiamondChange;
            _last = ViewReward.Diamond;
            text.text = _last.ToString();
        }

        private void OnDisable()
        {
            print("out d");
            // ViewReward.OnCoinChanged -= OnDiamondChange;
        }

        private void OnDiamondChange(int _to)
        {
            StartCoroutine(IncreaseReward(_last, _to, 1.0f));

            _last = _to;
        }

        IEnumerator IncreaseReward(int from, int to, float duration)
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