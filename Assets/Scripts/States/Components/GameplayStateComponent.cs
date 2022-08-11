
using SoftwareCore.UI.Presenters.Components;
using SoftwareCore.States;
using SpaceInvaders.States;
using SpaceInvaders.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SpaceInvaders.States.GameplaySubStates;
using SpaceInvaders.Gameplay.Components;
using SoftwareCore.States.Components;

namespace SpaceInvaders.States.Components
{
    public class GameplayStateComponent : GameObjectsActivatingStateComponent
    {

        public StateComponent mainMenuState;

        public StateComponent gameEndedState;

        public GameObject playerPrefab;

        public PlayerAreaComponent playerArea;

        public EnemiesAreaBoundsComponent enemiesArea;


        public TextUIPresenterComponent waveTextUIBinder;

        public TextUIPresenterComponent scoreTextUIBinder;

        public TextUIPresenterComponent livesCountTextUIBinder;



        ISubStateManager SubStateManager { get; set; }

        GameplaySubStatesSet SubStatesSet { get; set; }

        GameplaySubStatesData SubStatesData { get; set; }

        [Inject]
        public void Construct(IGameSettings settings, ISubStateManager subStateManager, GameplaySubStatesSet subStatesSet, IInstantiator instantiator) {

            SubStateManager = subStateManager;

            SubStatesSet = subStatesSet;

            SubStatesData = new GameplaySubStatesData() {
                Settings = settings,
                PlayerPrefab = this.playerPrefab,
                PlayerArea = this.playerArea,
                EnemiesArea = this.enemiesArea
            };

            SubStatesSet.SetDataToStates(SubStatesData);

            SubStatesSet.InitializationState.NextState = SubStatesSet.GameplayStateObjectInitializationState;
            SubStatesSet.GameplayStateObjectInitializationState.NextState = SubStatesSet.PrePlayState;
            SubStatesSet.GameplayStateObjectInitializationState.EndOfGameState = SubStatesSet.RegisterHighScoreState;
            SubStatesSet.PrePlayState.NextState = SubStatesSet.PlayState;
            SubStatesSet.PlayState.WinState = SubStatesSet.GameplayStateObjectInitializationState;
            SubStatesSet.PlayState.LostState = SubStatesSet.RegisterHighScoreState;
            SubStatesSet.RegisterHighScoreState.NextState = SubStatesSet.CleanupState;
        }

        public void StartMainMenuState() {
            if (this.CurrentStateManager != null) {
                this.CurrentStateManager.State = mainMenuState;
            }
        }

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);

            SubStateManager.State = SubStatesSet.InitializationState;

        }


        protected override void OnOnExit() {
            base.OnOnExit();

            SubStateManager.State = SubStatesSet.CleanupState;
            SubStateManager.State = null;
        }

        protected override void OnOnUpdate(float dTime) {
            base.OnOnUpdate(dTime);

            SubStateManager.Update(dTime);

            UpdateUI();

            if (SubStateManager.State == null) { //no more sub states, sequence ended
                CurrentStateManager.State = gameEndedState;
            }
        }

        protected override void OnOnFixedUpdate(float dTime) {
            base.OnOnFixedUpdate(dTime);
            SubStateManager.FixedUpdate(dTime);
        }

        void UpdateUI() {

            if (waveTextUIBinder != null) {
                waveTextUIBinder.SetText(this.SubStatesData.CurrenWaveNumber.ToString());
            }

            if (scoreTextUIBinder != null) {
                scoreTextUIBinder.SetText(this.SubStatesData.CurrentScore.ToString());
            }

            if (livesCountTextUIBinder != null) {
                livesCountTextUIBinder.SetText(this.SubStatesData.LivesLeft.ToString());
            }
        }
    }

}