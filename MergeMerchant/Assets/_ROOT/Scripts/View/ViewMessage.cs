using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace MJGame.MergeMerchant
{

    public enum TypeMessage
    {
        greeting, status, move, order, wait
    }
    public class ViewMessage : MonoBehaviour
    {
        public TextMeshProUGUI txt;

        [SerializeField] TypeMessage typeMessage;
        [Range(0, 60f)] public float _startT, _endT;

        private void OnEnable()
        {
            StartCoroutine(TimeChangeMessage(Random.Range(_startT, _endT)));
        }

        IEnumerator TimeChangeMessage(float _time)
        {
            txt.text = STRING.GetString(typeMessage);
            yield return new WaitForSeconds(_time);
            StartCoroutine(TimeChangeMessage(Random.Range(_startT, _endT)));
        }
    }
}