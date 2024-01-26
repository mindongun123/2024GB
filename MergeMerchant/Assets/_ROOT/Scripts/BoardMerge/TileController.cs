using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant.BoardMerge
{
    public class TileController : SingletonComponent<TileController>
    {

        [Header("Setup Tile")]
        [Tooltip("Amount Tile in board")] public Tile[] lsTile;

        private Tile kTileStart, kTileLast;

        [ShowInInspector]
        [Tooltip("Queue Save Tile When ID Default")] Queue<Tile> qeTileDefault = new Queue<Tile>();

        private int _numberClick = 0;
        public int NumberClick
        {
            set => _numberClick = value;
            get => _numberClick % 2;
        }


        private void OnEnable()
        {

            if (IsLoadDataBoard())
            {
                LoadDataBoard();
                LoadDataQueueTileDefault();
                return;
            }
            SetupTile();

        }

        /// <summary>
        ///  Set tile: Action when Data load  null  or new player 
        ///  Random.Range(0, SingletonComponent<DTPlayerPref>.Instance.OptionCurrent + 1) : Chon loai sinh ra
        ///  Random.Range(0, 5) : chon id sinh ra = id sinh ra + 10 * loai sinh
        /// </summary>
        private void SetupTile()
        {
            foreach (var item in lsTile)
            {
                item.SetTile(Random.Range(0, SingletonComponent<DTPlayerPref>.Instance.OptionCurrent + 1) * 10 + Random.Range(1, 5));
            }
        }


        public void SetOnClick(Tile kTile)
        {
            if (NumberClick == 0)
            {
                kTileStart = kTile;
            }
            else
            {
                kTileLast = kTile;

                CheckID();
            }
            NumberClick = NumberClick + 1;
        }


        private void CheckID()
        {
            if (kTileStart.ID == kTileLast.ID)
            {
                kTileStart.MergeTileComplete();
                kTileLast.MergeTileComplete(true);

                qeTileDefault.Enqueue(kTileStart);// add tile null vao queue de khi can mua se ban vao day 
                return;
            }
            kTileStart.MergeNoComplete();
            kTileLast.MergeNoComplete();
        }


        /// <summary>
        /// Buy item and add to board
        /// </summary>
        [Button]
        private void BuyTile()
        {
            if (qeTileDefault.Count == 0)
            {
                print("queue null - no buy");
                return;
            }
            Tile kTile = qeTileDefault.Dequeue();

            kTile.SetTile(Random.Range(0, SingletonComponent<DTPlayerPref>.Instance.OptionCurrent + 1) * 10 + Random.Range(1, 5));
        }



        private void OnDisable()
        {
            SaveDataBoard();
            SaveDataQueueTileDefault();
        }



        /// <summary>
        /// Save Data Item In Board
        /// </summary>
        /// <summary>
        /// Save And Load Data Item To Board
        /// </summary>

        [Button]
        private void SaveDataBoard()
        {
            Dictionary<int, int> lsDataItem = new Dictionary<int, int>();
            for (int i = 0; i < lsTile.Length; i++)
            {
                lsDataItem.Add(i, lsTile[i].ID);
            }
            ES3.Save<Dictionary<int, int>>(StaticGame.DATA_BOARD, lsDataItem);
        }

        /// <summary>
        /// Load data item save when player exit board   
        /// </summary>
        [Button]
        private void LoadDataBoard()
        {
            Dictionary<int, int> lsDataItem = ES3.Load<Dictionary<int, int>>(StaticGame.DATA_BOARD);

            for (int i = 0; i < lsTile.Length; i++)
            {
                lsTile[i].SetTile(lsDataItem[i]);
            }
        }

        /// <summary>
        /// check  xem co the duoc load data cho board khong 
        /// </summary>
        /// <returns> true -> load / false -> chua co data -> nguoi choi moi</returns>
        private bool IsLoadDataBoard()
        {
            if (ES3.KeyExists(StaticGame.DATA_BOARD)) return true;
            return false;
        }




        #region Sua phan load Queue


        /// <summary>
        /// Save Queue Tile Default When On Disable 
        /// </summary>
        private void SaveDataQueueTileDefault()
        {
            if (qeTileDefault == null)
            {
                qeTileDefault = new Queue<Tile>();
            }
            ES3.Save<Queue<Tile>>(StaticGame.QUEUE_TILE_DEFAULT, qeTileDefault);
        }

        /// <summary>
        /// Load Queue Tile Default When On Enable 
        /// </summary>
        private void LoadDataQueueTileDefault()
        {
            qeTileDefault = new Queue<Tile>();
            qeTileDefault = ES3.Load<Queue<Tile>>(StaticGame.QUEUE_TILE_DEFAULT);
        }
        #endregion
    }
}