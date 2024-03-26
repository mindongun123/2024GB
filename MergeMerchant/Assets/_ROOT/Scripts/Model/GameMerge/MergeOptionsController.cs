using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;


namespace MJGame.MergeMerchant.Merge
{
    public class MergeOptionsController : SingletonComponent<MergeOptionsController>
    {
        public TileBaseOptions[] tileBaseOptions;



        /// <summary>
        ///  lay ra vi tri cua TileBaseOptions tuong ung tren map
        /// </summary>
        /// <param name="kTileBase"></param>
        /// <returns></returns>
        public Vector2Int GetIdTileBaseOptions(TileBaseOptions kTileBase)
        {
            for (int i = 0; i < tileBaseOptions.Length; i++)
            {
                if (kTileBase == tileBaseOptions[i])
                {
                    return new Vector2Int(i % 7, i / 7);
                }
            }
            return Vector2Int.zero;
        }

        /// <summary>
        /// tra ve doi tuong tilebaseoptions co thu tu _idx
        /// </summary>
        /// <param name="_idx"></param>
        /// <returns></returns>
        public TileBaseOptions GetTileBaseOptions(int _idx)
        {
            return tileBaseOptions[_idx];
        }

        
    }
}
