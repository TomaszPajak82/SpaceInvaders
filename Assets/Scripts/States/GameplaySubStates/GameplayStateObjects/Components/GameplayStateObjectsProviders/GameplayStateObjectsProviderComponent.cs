using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{
    public abstract class GameplayStateObjectsProviderComponent : MonoBehaviour
    {

        public IGameplayStateObject[] GetGameplayStateObjects() {
            return OnGetGameplayStateObjects();
        }

        protected abstract IGameplayStateObject[] OnGetGameplayStateObjects();
    }
}
