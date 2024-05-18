// #if UNITY_FIREBASE

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ilumisoft.VisualStateMachine;
using MJGame.MergeMerchant.Firebase;
using MJGame.MergeMerchant.Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


namespace MJGame.MergeMerchant.MiniGame
{
    public enum MiniGameStatus
    {
        wait, play, end
    }

    public class MiniGameController : SingletonComponent<MiniGameController>
    {

        public UnityAction eventHealth;

        [SerializeField] StateMachine stateMachine;



        private void OnEnable()
        {
            eventHealth += ShowHealth;
        }

        private void OnDisable()
        {
            eventHealth -= ShowHealth;
        }

        [SerializeField] ViewPanelMiniGame viewPanelMiniGame;
        public int _time = 60;
        public int _health = 3;
        private int _idCheckCurrent = 0;
        public int _number = 20;

        public Dictionary<int, int> dictItemCheck = new Dictionary<int, int>(0);
        public MiniGameStatus miniGameStatus;
        public ItemMiniGame[] itemMiniGames;
        [SerializeField] TextMeshProUGUI txtHealth;
        [SerializeField] ItemMiniGame itemCurrent;
        [SerializeField] ViewShowTime viewShowTime;
        [SerializeField] GameObject miniBoard;

        private void Start()
        {
            StartCoroutine(SetIdItemStart(2));
        }

        private void ShowHealth()
        {
            _health--;
            txtHealth.text = _health.ToString();
            if (_health <= 0)
            {
                miniGameStatus = MiniGameStatus.end;
                viewPanelMiniGame.Lose();
                return;
            }
        }

        public void OnClickStart()
        {
            miniGameStatus = MiniGameStatus.play;
            TextMessage("<color=yellow>Bat dau!</color>", 0.5f);
            viewShowTime.TextTime(_time);
        }

        [SerializeField] LoadDataMiniGame loadDataMiniGame;

        IEnumerator SetIdItemStart(float _t)
        {
            yield return new WaitForSeconds(_t);

            List<int> listIdCheck = loadDataMiniGame.lsId;
            _number = loadDataMiniGame._number;
            _time = _number;

            int _count = listIdCheck.Count;
            for (int i = 0; i < _number; i++)
            {
                itemMiniGames[i].gameObject.SetActive(true);
                int rd = Random.Range(0, _count);
                int sp = listIdCheck[rd];
                if (dictItemCheck.ContainsKey(sp))
                {
                    dictItemCheck[sp] = dictItemCheck[sp] + 1;
                }
                else
                {
                    dictItemCheck[sp] = 1;
                }
                itemMiniGames[i].SetItem(sp);
            }
            viewShowTime.SetTimeStart(_time);
            SetIdCheck();
            miniBoard.SetActive(true);
        }

        public void SetIdCheck()
        {
            int rd = Random.Range(0, dictItemCheck.Count);
            if (dictItemCheck.Count <= 0)
            {
                miniGameStatus = MiniGameStatus.end;
                viewPanelMiniGame.Win();
                return;
            }
            _idCheckCurrent = dictItemCheck.Keys.ToList()[rd];
            itemCurrent.SetItem(_idCheckCurrent);
        }

        public void CheckDone(int _id, ItemMiniGame itemMiniGame)
        {
            if (_id == _idCheckCurrent)
            {
                dictItemCheck[_id]--;
                if (dictItemCheck[_id] <= 0)
                {
                    dictItemCheck.Remove(_id);
                    SetIdCheck();
                }
                itemMiniGame.OnDestroyObject();
            }
            else
            {
                TextMessage("<color=red>Khong dung</color>", 0.5f);
                eventHealth?.Invoke();
            }
        }

        public bool CheckIsStart()
        {
            if (miniGameStatus != MiniGameStatus.play)
            {
                TextMessage("<color=yellow>Chua duoc Click, hay nhan \n<color=white>Start</color></color>", 1f);
                return false;
            }
            return true;
        }
        public void TextMessage(string _msg, float _time)
        {
            SingletonComponent<SpawnText>.Instance.NewText(_msg, _time);
        }

        public int ScoreWinminiGame()
        {
            int _score = _number * _health + ConfigTime.timeEvent;
            UpdateData(_score);
            return _score;
        }
        public int ScoreLoseMiniGame()
        {
            int _score = -(_number * (3 - _health) + (_number - ConfigTime.timeEvent));
            UpdateData(_score);
            return _score;
        }

        private void UpdateData(int _idx)
        {
            stateMachine.Trigger("OpenEventEnd");
            loadDataMiniGame.SetUserData(_idx);
        }
    }
}

// #endif