using SoftwareCore.States;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.States.Components
{
    public class GameObjectsActivatingStateComponent : StateComponent
    {

        public GameObject[] gameObjectsToActivate;

        void SetGameObjectsActivation(bool active) {
            foreach (var go in gameObjectsToActivate) {
                if (go != null) {
                    go.SetActive(active);
                }
            }
        }

        protected override void OnOnEnter(IStateManager stateManager) {
            SetGameObjectsActivation(true);
        }

        protected override void OnOnExit() {
            SetGameObjectsActivation(false);
        }

        protected override void OnOnUpdate(float dTime) {
        }

        protected override void OnOnFixedUpdate(float dTime) {
        }

    }
}
