using System.Collections;
using System.Collections.Generic;
using MJGame;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;

public class ViewNewLevel : MonoBehaviour
{
    public TextMeshProUGUI txtNewLevel;
    private void OnEnable()
    {
        txtNewLevel.text = $"LEVEL {SingletonComponent<LevelGame>.Instance.Level}";
    }
}
