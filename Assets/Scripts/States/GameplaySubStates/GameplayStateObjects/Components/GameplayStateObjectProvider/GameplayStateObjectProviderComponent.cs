using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{
    public abstract class GameplayStateObjectProviderComponent : MonoBehaviour
    {

        public IGameplayStateObject GetGameplayStateObject() {
            return OnGetGameplayStateObject();
        }

        protected abstract IGameplayStateObject OnGetGameplayStateObject();
    }
}