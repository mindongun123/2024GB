using System.Collections;
using System.Collections.Generic;
using MJGame;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant.Merge
{
    public class ViewSpawnBasket : MonoBehaviour
    {
        [Button]
        public void OnClickUpdateBasket()
        {
            SingletonComponent<SaveGameMerge>.Instance.UpdateBasket();
        }

    }
}
