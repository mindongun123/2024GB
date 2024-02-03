using System.Collections;
using System.Collections.Generic;
using MJGame;
using MJGame.MergeMerchant;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestDTOrder : SingletonComponent<TestDTOrder>
{
    public DTOrdrer dtorder;
    public DTOrdrer TestStartSetupOrder()
    {
        dtorder = new DTOrdrer(Random.Range(1, 3));
        return dtorder;
    }

}
