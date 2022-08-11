using SoftwareCore.States;
using SpaceInvaders.Settings;
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class GameplayStateObjectInitializationGameplaySubState : GameplaySubState
    {

        IGameSettings Settings { get; set; }

        IInstantiator Instantiator { get; set; }

        IGameplayStateObjectManager GameplayStateObjectManager { get; set; }

        public IState NextState { get; set; }

        public IState EndOfGameState { get; set; }

        public GameplayStateObjectInitializationGameplaySubState(IGameplayStateObjectManager gameplayStateObjectManager) {
            GameplayStateObjectManager = gameplayStateObjectManager;
        }

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);

            bool endOfGame = true;

            Data.GameplayStateObject = GameplayStateObjectManager.GetNextGameplayStateObject(this.Data);

            if (Data.GameplayStateObject != null) {    

                if (Data.GameplayStateObject != null) {
                    Data.GameplayStateObject.Initialize(this.Data);
                    endOfGame = false;
                }

            }

            if (endOfGame) {
                stateManager.State = EndOfGameState;
            } else {
                stateManager.State = NextState;
            }
        }


    }
}