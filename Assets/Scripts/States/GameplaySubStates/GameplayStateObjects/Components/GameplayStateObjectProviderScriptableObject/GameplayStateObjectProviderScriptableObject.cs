using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{

    public abstract class GameplayStateObjectProviderScriptableObject : ScriptableObject
    {

        public IGameplayStateObject GetGameplayStateObject() {
            return OnGetGameplayStateObject();
        }

        protected abstract IGameplayStateObject OnGetGameplayStateObject();

    }
}
