using SoftwareCore.States;
using SpaceInvaders.Settings;
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class PrePlayGameplaySubState : GameplaySubState
    {

        public IState NextState { get; set; }

        IGameplayStateObjectSession GameplayStateObjectSession { get; set; }

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);
            GameplayStateObjectSession = Data.GameplayStateObject.StartPrePlaySession();
        }

        protected override void OnOnUpdate(float dTime) {
            base.OnOnUpdate(dTime);

            GameplayStateObjectSession.Update(dTime);

            MoveToNextStateWhenCompleted();
        }

        protected override void OnOnFixedUpdate(float dTime) {
            base.OnOnFixedUpdate(dTime);

            GameplayStateObjectSession.FixedUpdate(dTime);
        }

        void MoveToNextStateWhenCompleted() {
            if (GameplayStateObjectSession.IsCompleted) {
                this.CurrentStateManager.State = NextState;
            }
        }
    }

}