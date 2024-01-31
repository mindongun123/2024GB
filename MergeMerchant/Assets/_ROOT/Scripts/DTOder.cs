using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MJGame.MergeMerchant
{
    [CreateAssetMenu(menuName = "Data/SO/DTOder", fileName = "DTOderTEST")]
    public class DTOder : ScriptableObject
    {
        public int _amount;
        public int[] _idTile;// ID sprite san pham

        public Sprite[] sprites;
    }
}