using UnityEditor;
using UnityEngine;
using System;
using SpaceInvaders.HighScore;
using SoftwareCore.States;
using SpaceInvaders.LastGameData;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class RegisterHighScoreGameplaySubState : GameplaySubState
    {

        public IState NextState { get; set; }

        IHighScoreRepository HighScoreRepository { get; set; }

        LastGameDataInfo LastGameDataInfo { get; set; }

        public RegisterHighScoreGameplaySubState(IHighScoreRepository highScoreRepository,LastGameDataInfo lastGameDataInfo) {
            HighScoreRepository = highScoreRepository;
            LastGameDataInfo = lastGameDataInfo;
        }

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);

            HighScoreInfo highScore = new HighScoreInfo(Data.CurrentScore, Data.CurrenWaveNumber - 1, DateTime.Now);

            HighScoreRepository.Add(highScore);

            LastGameDataInfo.HighScore = highScore;

            this.CurrentStateManager.State = NextState;
        }

    }
}