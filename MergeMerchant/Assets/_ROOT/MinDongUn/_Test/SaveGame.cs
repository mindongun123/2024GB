using System.Collections;
using System.Collections.Generic;
using MJGame;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Mindongun
{
    public class SaveGame : SingletonComponent<SaveGame>
    {

        private void OnEnable()
        {
            Setup();
        }

        private void Setup()
        {
            SingletonComponent<SaveGame>.Instance.LoadBoard();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Vector2Int">Vi tri trong Grid BFS</typeparam>
        /// <typeparam name="Vector2Int">x = trang thai (1/0) : y = ID neu x = 1 </typeparam>
        /// <returns></returns>
        [ShowInInspector]
        Dictionary<Vector2Int, Vector2Int> dicSaveBoard = new Dictionary<Vector2Int, Vector2Int>();


        [Button]
        public void SaveBoard()
        {
            for (int i = 0; i < ConstGame.COLUMN; i++)
            {
                for (int j = 0; j < ConstGame.ROW; j++)
                {
                    if (SingletonComponent<BFS>.Instance.grid[i, j] == 1)
                    {
                        int _idx = i + j * ConstGame.COLUMN;
                        Transform trs = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(_idx).transform.GetChild(0);
                        Options ops = trs.GetComponent<Options>();
                        dicSaveBoard[new Vector2Int(i, j)] = new Vector2Int(1, ops.ID);
                    }
                    else
                    {
                        dicSaveBoard[new Vector2Int(i, j)] = new Vector2Int(0, 0);
                    }
                }
            }

            print("Save Complete");
            ES3.Save<Dictionary<Vector2Int, Vector2Int>>(ConstGame.SAVE_BOARD, dicSaveBoard);
        }

        [SerializeField] GameObject OptionsObject;

        /// <summary>
        /// Khoi tao Game
        /// </summary>
        private Dictionary<Vector2Int, Vector2Int> InitBoard()
        {
            Dictionary<Vector2Int, Vector2Int> dic = new Dictionary<Vector2Int, Vector2Int>();


            for (int i = 0; i < ConstGame.COLUMN; i++)
            {
                for (int j = 0; j < ConstGame.ROW; j++)
                {
                    dic.Add(new Vector2Int(i, j), new Vector2Int(1, 1));
                }
            }
            return dic;
        }

        [Button]
        public void LoadBoard()
        {
            /// <summary>
            /// dic
            /// </summary>
            /// <typeparam name="Vector2Int">x,y la vi tri Tilebase tren board</typeparam>
            /// <typeparam name="Vector2Int">x -> co ton tai options hay khong, y -> ID options neu no ton tai trong Tilebase</typeparam>
            /// <returns></returns>
            Dictionary<Vector2Int, Vector2Int> dic = new Dictionary<Vector2Int, Vector2Int>();

            dic = ES3.Load<Dictionary<Vector2Int, Vector2Int>>(ConstGame.SAVE_BOARD, new Dictionary<Vector2Int, Vector2Int>());

            if (dic.Count <= 0)
            {
                dic = InitBoard();
            }
            foreach (var item in dic)
            {
                SingletonComponent<BFS>.Instance.grid[item.Key.x, item.Key.y] = item.Value.x;
                if (item.Value.x == 1)
                {
                    int _idx = item.Key.x + item.Key.y * ConstGame.COLUMN;
                    Transform trs = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(_idx).transform;
                    GameObject opsOb = Instantiate<GameObject>(OptionsObject, Vector2.zero, Quaternion.identity);
                    opsOb.transform.SetParent(trs);
                    Options ops = opsOb.GetComponent<Options>();
                    ops.Setting(item.Value.y);
                }
            }
        }

        private void OnDisable()
        {
            SingletonComponent<SaveGame>.Instance.SaveBoard();
        }
    }
}