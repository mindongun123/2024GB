using System.Collections;
using System.Collections.Generic;
using MJGame;
using MJGame.MergeMerchant;
using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;

public class ViewNewLevel : MonoBehaviour
{
    public TextMeshProUGUI txtNewLevel;
    private void OnEnable()
    {
        SingletonComponent<AudioController>.Instance.AudioUpdateLevelPlay();
        txtNewLevel.text = $"LEVEL {SingletonComponent<LevelGame>.Instance.Level}";
    }
}
