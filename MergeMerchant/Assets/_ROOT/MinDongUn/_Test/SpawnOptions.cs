using System.Collections;
using System.Collections.Generic;
using MJGame;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mindongun
{
    public class SpawnOptions : SingletonComponent<SpawnOptions>
    {

        [SerializeField] GameObject OptionObject;
        [ShowInInspector]
        private TileBaseOptions kTileBaseOptionSelect;
        public TileBaseOptions TileBaseOptionsSelect
        {
            set => kTileBaseOptionSelect = value;
            get => kTileBaseOptionSelect;
        }

        public void LoadBasket(int _idx)
        {
            TileBaseOptionsSelect = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(_idx);
            TileBaseOptionsSelect.transform.GetChild(0).GetComponent<Options>().SetBasketSave();
        }

        public void CreateNewOptions()
        {
            Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(TileBaseOptionsSelect);
            Vector2Int _target = SingletonComponent<BFS>.Instance.FindNearestEmptyPosition(_ps);
            if (_target == _ps)
            {
                print("khong con vi tri thoa man");
                return;
            }
            TileBaseOptions tileBaseOptions = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(_target.x + _target.y * ConstGame.COLUMN);
            GameObject opsOb = Instantiate<GameObject>(OptionObject, Vector2.zero, Quaternion.identity);
            opsOb.transform.SetParent(tileBaseOptions.transform);

            Options ops = opsOb.GetComponent<Options>();

            // sinh ra options co id trong khoang
            ops.Setting(Random.Range(1, 5));
            SingletonComponent<BFS>.Instance.SetGridAtPosition(_target);
        }
    }
}
