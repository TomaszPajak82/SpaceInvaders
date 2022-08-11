﻿using SoftwareCore.Pool;
using SoftwareCore.States;
using SpaceInvaders.Health.Components;
using SpaceInvaders.Settings;
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class InitializationGameplaySubState : GameplaySubState
    {

        IGameSettings Settings { get; set; }

        IInstantiator Instantiator { get; set; }

        public IState NextState { get; set; }

        IGameplayStateObjectManager GameplayStateObjectManager { get; set; }

        public InitializationGameplaySubState(IGameSettings settings, IInstantiator instantiator, IGameplayStateObjectManager gameplayStateObjectManager) {
            Settings = settings;
            Instantiator = instantiator;

            this.GameplayStateObjectManager = gameplayStateObjectManager;
        }

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);

            this.GameplayStateObjectManager.Reset();

            Data.Instantiator = Instantiator;

            var positionAndOrientation = Data.PlayerArea.GetInitialPositionAndOrientation();

            Data.PlayerGO = PoolUtils.RetriveOrCreateObject(Data.PlayerPrefab, positionAndOrientation.position, positionAndOrientation.orientation, null,
                    x => Instantiator.InstantiatePrefab(x.prefab, x.position, x.rotation, x.parent)
                    );

            HealthComponent health = Data.PlayerGO.GetComponent<HealthComponent>();
            if(health != null) {
                health.Health = Settings.LivesCount;
                Data.LivesLeft = health.Health;
            } else {
                Data.LivesLeft = int.MaxValue;
            }

            Data.CurrentScore = 0;
            Data.CurrenWaveNumber = 0;

            stateManager.State = NextState;
        }



    }
}