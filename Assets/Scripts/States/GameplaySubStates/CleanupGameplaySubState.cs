using SoftwareCore.States;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class CleanupGameplaySubState : GameplaySubState
    {

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);

            Data.Cleanup();

            this.CurrentStateManager.State = null;
        }
    }

}