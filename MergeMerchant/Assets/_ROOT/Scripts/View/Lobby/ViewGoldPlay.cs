using System.Collections;
using System.Collections.Generic;
using Ilumisoft.VisualStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MJGame.MergeMerchant.Lobby
{
    public class ViewGoldPlay : MonoBehaviour
    {
        [SerializeField] GameObject viewGoldPlay;
        [SerializeField]
        StateMachine stateMachine;
        [Button]
        public void OnClickOpenViewGoldPlay()
        {
            stateMachine.Trigger("OpenGoldPlay");
        }
    }
}