using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using UnityEngine;


[Serializable]
public class DateTimeData
{
    public int _day;
    public int _month;
    public int _year;

    public int _hour;
    public int _min;
    public int _sec;
}


[Serializable]
public class TIME_REMAINING
{
    public Vector2Int upTime = new Vector2Int(1000, 100);// thoi gian giam di -> cost
    public Vector2Int upEnergy = new Vector2Int(20, 10);// nang luong nhan them -> cost
    public string _start;
    public int _remaining;
    public int _energy = 100;


    public TIME_REMAINING()
    {
        _start = DateTime.Now.ToString();
        _remaining = ConstGame.TIME_DEFAULT;
        _energy = 100;
        upTime = new Vector2Int(1000, 100);
        upEnergy = new Vector2Int(20, 10);
    }

    public void Reset()
    {
        _start = DateTime.Now.ToString();
        _remaining = ConstGame.TIME_DEFAULT;
        _energy = 100;
        upTime = new Vector2Int(1000, 100);
        upEnergy = new Vector2Int(20, 10);
    }

    public void UpdateTime()
    {
        _remaining = _remaining - upTime.x;
        upTime.y = upTime.y + 10;
        _start = DateTime.Now.ToString();
    }

    public void UpdateEnergy()
    {
        _energy = _energy + upEnergy.x;
        upEnergy.y = upEnergy.y + 10;
    }
}