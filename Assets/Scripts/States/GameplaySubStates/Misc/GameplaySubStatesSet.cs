using System.Collections;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class GameplaySubStatesSet
    {
        public InitializationGameplaySubState InitializationState { get; protected set; }

        public GameplayStateObjectInitializationGameplaySubState GameplayStateObjectInitializationState { get; protected set; }

        public PrePlayGameplaySubState PrePlayState { get; protected set; }

        public PlayGameplaySubState PlayState { get; protected set; }
        public RegisterHighScoreGameplaySubState RegisterHighScoreState { get; protected set; }

        public CleanupGameplaySubState CleanupState { get; protected set; }

        public GameplaySubStatesSet(
            InitializationGameplaySubState initializationState,
            GameplayStateObjectInitializationGameplaySubState gameplayStateObjectInitializationState,
            PrePlayGameplaySubState waveCreationState,
            PlayGameplaySubState playState,
            RegisterHighScoreGameplaySubState registerHighScoreState,
            CleanupGameplaySubState cleanupState

            ) {

            InitializationState = initializationState;
            GameplayStateObjectInitializationState = gameplayStateObjectInitializationState;
            PrePlayState = waveCreationState;
            PlayState = playState;
            RegisterHighScoreState = registerHighScoreState;
            CleanupState = cleanupState;

        }

        public void SetDataToStates(GameplaySubStatesData data) {
            InitializationState.Data = data;
            GameplayStateObjectInitializationState.Data = data;
            PrePlayState.Data = data;
            PlayState.Data = data;
            RegisterHighScoreState.Data = data;
            CleanupState.Data = data;
        }
    }
}