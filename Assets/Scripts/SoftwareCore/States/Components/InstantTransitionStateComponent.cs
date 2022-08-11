using SoftwareCore.States;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SoftwareCore.States.Components
{
    public class InstantTransitionStateComponent : StateComponent
    {

        public StateComponent nextState;

        protected override void OnOnEnter(IStateManager stateManager) {
            stateManager.State = nextState;
        }

        protected override void OnOnExit() {

        }

        protected override void OnOnFixedUpdate(float dTime) {

        }

        protected override void OnOnUpdate(float dTime) {

        }

    }
}