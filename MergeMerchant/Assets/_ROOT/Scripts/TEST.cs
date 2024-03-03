using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant;
using Sirenix.OdinInspector;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [Button]
    public void ButtonAddCoin()
    {
        ViewReward.AddCoin(10);
        print("ädd coin");
    }
    [Button]
    public void ButtonAddDiamond()
    {
        ViewReward.AddDiamond(10);
        print("ädd diamond");
    }
    [Button]
    public void ButtonAddEnergy()
    {
        ViewReward.AddEnergy(10);
        print("ädd energy");
    }
}
