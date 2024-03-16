using System.Collections;
using System.Collections.Generic;
using Ilumisoft.VisualStateMachine;
using MJGame.Library.Utility;
using MJGame.MergeMerchant.Lobby;
using MJGame.MergeMerchant.Merge;
using UnityEngine;

namespace MJGame.MergeMerchant.House
{
    public class ViewItemEnergyController : SingletonComponent<ViewItemEnergyController>
    {
        [SerializeField]
        ViewItemEnergy[] viewItemEnergies;

        List<TIME_REMAINING> listTimeRemaining = new List<TIME_REMAINING>();
        private void OnEnable()
        {
            listTimeRemaining = MJGameSave.GetList(ConstGame.TIME_ENERGY_REMAINING, new List<TIME_REMAINING>() { new TIME_REMAINING() });

            SetUpItemEnegry();
        }

        public void SetUpItemEnegry()
        {
            for (int i = 0; i < listTimeRemaining.Count; i++)
            {
                viewItemEnergies[i].SetTimeRemaining(listTimeRemaining[i]);
            }
        }

        public void AddTimeRemaining()
        {
            listTimeRemaining.Add(new TIME_REMAINING());

            SetUpItemEnegry();
        }

        private void OnDisable()
        {
            MJGameSave.SetList(ConstGame.TIME_ENERGY_REMAINING, listTimeRemaining);
        }
        [SerializeField] ViewTimeRemaining viewTimeRemaining;
        [SerializeField] StateMachine stateMachine;
        public void OpenViewTimeRemaining(TIME_REMAINING kTime)
        {
            stateMachine.Trigger("OpenRemaining");
            viewTimeRemaining.SetTimeRemaining(kTime);
        }
    }
}