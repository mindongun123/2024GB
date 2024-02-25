using System.Collections;
using System.Collections.Generic;
using MJGame;
using Sirenix.OdinInspector;
using UnityEngine;


namespace MJGame.MergeMerchant.Merge
{
    public class SaveGameMerge : SingletonComponent<SaveGameMerge>
    {

        private void OnEnable()
        {
            LoadBoard();
        }


        #region SAVE BOARD
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Vector2Int">Vi tri trong Grid BFS</typeparam>
        /// <typeparam name="Vector2Int">x = trang thai (1/0) : y = ID neu x = 1 </typeparam>
        /// <returns></returns>
        [ShowInInspector]
        Dictionary<Vector2Int, Vector2Int> dicSaveBoard = new Dictionary<Vector2Int, Vector2Int>();


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
                    if (j < 2 || j > 5)
                    {
                        dic.Add(new Vector2Int(i, j), new Vector2Int(1, 0));
                    }
                    else
                    {
                        dic.Add(new Vector2Int(i, j), new Vector2Int(1, 1));
                    }
                }
            }
            UpdateNumberID(1, 27);


            /// <summary>
            ///  Khoi tao vi tri Basket dau tien
            /// </summary>
            Vector2Int kPosBasketInit = new Vector2Int(5, 5);
            IdTileBaseBasket = kPosBasketInit.x + kPosBasketInit.y * ConstGame.COLUMN;
            dic[kPosBasketInit] = new Vector2Int(1, IdBasket);


            return dic;
        }

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

            SingletonComponent<SpawnOptions>.Instance.LoadBasket(IdTileBaseBasket);
        }
        #endregion


        #region  SAVE ID BASKET
        public int IdTileBaseBasket
        {
            get => ES3.Load(ConstGame.BASKET_CURRENT, 20);
            set => ES3.Save(ConstGame.BASKET_CURRENT, value);
        }
        public int IdBasket
        {
            get => ES3.Load(ConstGame.ID_BASKET, ConstGame.ID_BASKET_DEFAULT);
            set => ES3.Save(ConstGame.ID_BASKET, value < 69 ? value : 68);
        }
        private int SaveIdGridBasketInBoard()
        {
            Vector2Int kPostionBasket = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(SingletonComponent<SpawnOptions>.Instance.TileBaseOptionsSelect);
            return kPostionBasket.x + kPostionBasket.y * ConstGame.COLUMN;
        }
        private void SaveBasket()
        {
            IdTileBaseBasket = SaveIdGridBasketInBoard();
        }
        #endregion
        public void SaveGameMergeComplete()
        {
            SaveBoard();
            SaveBasket();
            SaveTopMerge();
        }

        #region Top Merge - Order Product

        public void SaveTopMerge()
        {
            SingletonComponent<OrderController>.Instance.SaveDictionaryOrderProduct();
        }
        #endregion


        #region Save Number Id Option in Board
        public void SaveNumberID(int _id)
        {
            UpdateNumberID(_id);//add 1
            UpdateNumberID(_id - 1, -2);// sub 2
        }
        public int GetNumberId(int _id)
        {
            return PlayerPrefs.GetInt(_id.ToString(), 0);
        }

        public void UpdateNumberID(int _id, int _nb = 1)
        {
            PlayerPrefs.SetInt(_id.ToString(), GetNumberId(_id) + _nb);
            SingletonComponent<OrderController>.Instance.CheckOrderProductComplete();
        }
        #endregion


        private void OnDisable()
        {
            SaveGameMergeComplete();
        }
    }
}