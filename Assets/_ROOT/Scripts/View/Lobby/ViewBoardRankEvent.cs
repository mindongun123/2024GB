using System;
using System.Collections;
using System.Collections.Generic;
using MJGame.MergeMerchant.Merge;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewBoardRankEvent : SingletonComponent<ViewBoardRankEvent>
    {
    //     private string email;

    //     [SerializeField]
    //     ViewItemEvent[] viewItemEvent;

    //     [SerializeField] ViewItemEvent viewItemEventPlayer;

    //     private void Start()
    //     {
    //         LoadAccountPlayer();
    //     }

    //     public void LoadAccountPlayer()
    //     {
    //         email = PlayerPrefs.GetString(ConstGame.EMAIL, "22222@gmail.com");
    //     }

    //     public void SetAccountPlayer(int _coin, int coinMax, int _stt, string _email)
    //     {
    //         viewItemEventPlayer.SetViewItem(_coin, coinMax, _stt, _email, true);
    //     }

    //     private string KeyAccountPlayer()
    //     {
    //         return email.Replace(".", "_");
    //     }

    //     private string KeyAccountList(ACCOUNT ac)
    //     {
    //         return ac._email.Replace(".", "_");
    //     }

    //     private bool IsAccount(string k2)
    //     {
    //         return email.Equals(k2);
    //     }

    //     public void LoadRank(List<ACCOUNT> ls)
    //     {
    //         int length = ls.Count > 10 ? 10 : ls.Count;
    //         int coinMax = ls[0]._coin;

    //         for (int i = 0; i < length; i++)
    //         {
    //             string k2 = KeyAccountList(ls[i]);
    //             if (IsAccount(k2))
    //             {
    //                 SetAccountPlayer(ls[i]._coin, coinMax, i, ls[i]._email);
    //                 viewItemEvent[i].SetViewItem(ls[i]._coin, coinMax, i, ls[i]._email, true);
    //             }
    //             else
    //             {
    //                 viewItemEvent[i].SetViewItem(ls[i]._coin, coinMax, i, ls[i]._email);
    //             }

    //         }
    //         for (int i = length; i < 10; i++)
    //         {
    //             viewItemEvent[i].gameObject.SetActive(false);
    //         }
    //     }
    }

}
