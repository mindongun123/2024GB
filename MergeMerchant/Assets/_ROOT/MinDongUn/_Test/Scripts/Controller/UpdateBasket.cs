using System.Collections;
using System.Collections.Generic;
using MJGame;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant.Merge
{
    public class UpdateBasket : MonoBehaviour
    {

        /// <summary>
        /// Duoc click khi do se co UI hien len -> dong thoi luu game roi
        /// </summary>
        [Button]
        public void OnClickUpBasket()
        {
            SingletonComponent<SaveGame>.Instance.IdBasket = SingletonComponent<SaveGame>.Instance.IdBasket + 1;
            print("update basket complete");
        }
    }
}
