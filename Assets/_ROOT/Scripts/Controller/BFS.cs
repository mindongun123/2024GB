using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Merge
{
    public class BFS : SingletonComponent<BFS>
    {

        [ShowInInspector]
        public int[,] grid = new int[ROW, COLUMN];
        const int ROW = 7, COLUMN = 9;



        /// <summary>
        /// Tra ra vi tri trong gan A nhat
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public Vector2Int FindNearestEmptyPosition(Vector2Int start)
        {
            bool[,] visited = new bool[ROW, COLUMN];

            Queue<Vector2Int> queue = new Queue<Vector2Int>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                int row = current.x;
                int col = current.y;

                if (grid[row, col] == 0)
                {
                    return current;
                }

                // Kiểm tra các ô xung quanh
                foreach (var dir in new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(-1, -1), new Vector2Int(1, -1) })
                {
                    Vector2Int next = current + dir;
                    if (next.x >= 0 && next.x < ROW && next.y >= 0 && next.y < COLUMN && !visited[next.x, next.y])
                    {
                        visited[next.x, next.y] = true;
                        queue.Enqueue(next);
                    }
                }
            }

            Debug.Log("No empty position found!");
            return start;
        }


        public Vector2Int FindNearestPointOptionWithID(Vector2Int start, int _id)
        {

            bool[,] visited = new bool[ROW, COLUMN];

            Queue<Vector2Int> queue = new Queue<Vector2Int>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                int row = current.x;
                int col = current.y;

                if (grid[row, col] == 1)
                {
                    TileBaseOptions tbOps = SingletonComponent<MergeOptionsController>.Instance.GetTileBaseOptions(row + col * ConstGame.COLUMN);
                    Options ops = tbOps.transform.GetChild(0).GetComponent<Options>();
                    if (_id == ops.ID)
                    {
                        return current;
                    }
                }

                // Kiểm tra các ô xung quanh
                foreach (var dir in new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(-1, -1), new Vector2Int(1, -1) })
                {
                    Vector2Int next = current + dir;
                    if (next.x >= 0 && next.x < ROW && next.y >= 0 && next.y < COLUMN && !visited[next.x, next.y])
                    {
                        visited[next.x, next.y] = true;
                        queue.Enqueue(next);
                    }
                }
            }

            Debug.Log("No empty position found!");
            return start;
        }

        public void SetGridAtPosition(Vector2Int _ps, int _vl = 1)
        {
            grid[_ps.x, _ps.y] = _vl;
        }
    }
}
