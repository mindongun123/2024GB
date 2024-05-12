#if UNITY_FIREBAE
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MJGame.MergeMerchant.Firebase;
using MJGame.MergeMerchant.Merge;
using UnityEngine;


namespace MJGame.MergeMerchant.Lobby
{
    public class ViewLoadBoardEvent : MonoBehaviour
    {
        public LoadDataEvent loadDataEvent;
        [SerializeField] ViewItemEvent[] viewItemEvents;
        [SerializeField] ViewItemEvent itemKeyAvatar;

        [SerializeField] GameObject board;

        private void OnEnable()
        {
            StartCoroutine(Delay());

            IEnumerator Delay()
            {
                yield return new WaitForSeconds(1.5f);
                board.SetActive(true);
                OnLoadDataBoard();
            }
        }
        public void OnLoadDataBoard()
        {
            string emailKey = PlayerPrefs.GetString(ConstGame.EMAIL, "");
            List<Data> data = new List<Data>();

            data = loadDataEvent.listRank;
            int valueMax = data[0].value;
            int n = data.Count < 10 ? data.Count : 10;

            bool _isRank = false;

            for (int i = 0; i < n; i++)
            {
                if (emailKey.Equals(data[i].email))
                {
                    viewItemEvents[i].SetViewItem(data[i].value, valueMax, i, data[i].name, true);
                    itemKeyAvatar.SetViewItem(data[i].value, valueMax, i, data[i].name, true);
                    _isRank = true;
                }
                else
                {
                    viewItemEvents[i].SetViewItem(data[i].value, valueMax, i, data[i].name, false);
                }
            }
            for (int i = n; i < 10; i++)
            {
                viewItemEvents[i].gameObject.SetActive(false);
            }

            if (!_isRank && !emailKey.Equals(""))
            {
                var result = data.Where(d => d.email == emailKey).ToList();
                itemKeyAvatar.SetViewItem(result[0].value, valueMax, 10, result[0].name, true);
            }
        }
    }
}
#endif
