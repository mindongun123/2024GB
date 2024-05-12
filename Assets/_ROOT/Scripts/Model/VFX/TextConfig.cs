using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MidniteOilSoftware;
using TMPro;
using UnityEngine;

public class TextConfig : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI txt;

    [SerializeField] RectTransform child;
    public void SetText(string _txt, float _time)
    {
        child.localPosition= Vector2.zero;
        txt.text = _txt;

        child.DOAnchorPosY(child.localPosition.y + 230, _time);

        StartCoroutine(IEDestroy());
        IEnumerator IEDestroy()
        {
            yield return new WaitForSeconds(_time);
            child.DOKill();
            ObjectPoolManager.DespawnGameObject(this.gameObject);
        }
    }
}
