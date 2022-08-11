
using SoftwareCore.States.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.States.Components
{
    public class MainMenuStateComponent : GameObjectsActivatingStateComponent
    {

        public StateComponent gameplayState;

        public StateComponent highScoreState;


        [Inject]
        public void Construct() {
        }


        public void StartGameplayState() {
            if (this.CurrentStateManager != null) {
                this.CurrentStateManager.State = gameplayState;
            }
        }

        public void StartHighScoreState() {
            if (this.CurrentStateManager != null) {
                this.CurrentStateManager.State = highScoreState;
            }
        }

    }
}
