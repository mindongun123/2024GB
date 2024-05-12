using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Event")]
public class EVENT : ScriptableObject
{
    public Vector2Int[] date;

    public Vector2Int GetDay(int _day)
    {
        return date[_day];
    }
}
