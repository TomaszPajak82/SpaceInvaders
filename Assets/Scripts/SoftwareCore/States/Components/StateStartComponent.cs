using SoftwareCore.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

namespace SoftwareCore.States.Components
{
    public class StateStartComponent : MonoBehaviour
    {
        public StateComponent state;

        IStateManager TargetStateManager { get; set; }

        [Inject]
        public void Construct(IStateManager stateManager) {
            TargetStateManager = stateManager;
        }

        private void Start() {
            TargetStateManager.State = state;
        }

    }
}
