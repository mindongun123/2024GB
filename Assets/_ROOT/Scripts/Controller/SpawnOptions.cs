using System.Collections;
using System.Collections.Generic;
using MJGame;
using MJGame.MergeMerchant.Lobby;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Merge
{
    public class SpawnOptions : SingletonComponent<SpawnOptions>
    {

        [SerializeField] GameObject OptionObject;
        [SerializeField] MergeOptionsController mergeOptionsController;
        [SerializeField] SaveGameMerge saveGameMerge;
        private TileBaseOptions kTileBaseOptionSelect;

        public TileBaseOptions TileBaseOptionsSelect
        {
            set => kTileBaseOptionSelect = value;
            get => kTileBaseOptionSelect;
        }

        public void LoadBasket(int _idx)
        {
            TileBaseOptionsSelect = mergeOptionsController.GetTileBaseOptions(_idx);
            TileBaseOptionsSelect.transform.GetChild(0).GetComponent<Options>().SetBasketSave();
        }




        public bool CreateNewOptions()
        {
            Vector2Int _ps = mergeOptionsController.GetIdTileBaseOptions(TileBaseOptionsSelect);
            Vector2Int _target = SingletonComponent<BFS>.Instance.FindNearestEmptyPosition(_ps);
            if (_target == _ps)
            {
                print("khong con vi tri thoa man");
                return false;
            }
            //Audio
            SingletonComponent<AudioController>.Instance.AudioOnClickBasketPlay();
            //
            TileBaseOptions tileBaseOptions = mergeOptionsController.GetTileBaseOptions(_target.x + _target.y * ConstGame.COLUMN);
            GameObject opsOb = Instantiate<GameObject>(OptionObject, Vector2.zero, Quaternion.identity);
            opsOb.transform.SetParent(tileBaseOptions.transform);

            Options ops = opsOb.GetComponent<Options>();

            ops.AnimationMergeComplete();

            // sinh ra options co id trong khoang
            int _idxId = Random.Range(0, SingletonComponent<SaveLobbyGame>.Instance.GetListIdOptionSpawn().Count);
            ops.Setting(SingletonComponent<SaveLobbyGame>.Instance.GetListIdOptionSpawn()[_idxId]);

            //cap nhat number id
            saveGameMerge.UpdateNumberID(ops.ID);

            SingletonComponent<BFS>.Instance.SetGridAtPosition(_target);
            return true;
        }

    }
}
