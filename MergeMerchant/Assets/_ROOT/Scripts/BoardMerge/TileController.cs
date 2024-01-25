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

        private int _numberClick = 0;

        public int NumberClick
        {
            set => _numberClick = value;
            get => _numberClick % 2;
        }

        private void Start()
        {
            SetupTile();
        }

        public void SetupTile()
        {
            foreach (var item in lsTile)
            {
                item.SetTile(Random.Range(StaticGame.TILE_MIN, SingletonComponent<DTPlayerPref>.Instance.TileCurrent + 1));
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

        [ShowInInspector]
        Queue<Tile> qeTileDefault = new Queue<Tile>();

        [Button]
        private void BuyTile()
        {
            if (qeTileDefault.Count == 0)
            {
                print("queue null - no buy");
                return;
            }
            Tile kTile = qeTileDefault.Dequeue();
            kTile.SetTile(Random.Range(StaticGame.TILE_MIN, SingletonComponent<DTPlayerPref>.Instance.TileCurrent + 1));
        }

    }
}