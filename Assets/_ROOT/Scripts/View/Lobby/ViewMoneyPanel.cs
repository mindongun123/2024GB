using System.Collections;
using System.Collections.Generic;
using Ilumisoft.VisualStateMachine;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewMoneyPanel : MonoBehaviour
    {
        [SerializeField]
        StateMachine stateMachine;

        public void OnClickOpenShop()
        {
            stateMachine.Trigger("OpenShop");
        }
    }
}