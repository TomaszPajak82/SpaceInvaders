
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{
    public class ScriptableObjectsGameplayStateObjectsProviderComponent : GameplayStateObjectsProviderComponent
    {
        public GameplayStateObjectProviderScriptableObject[] gameplayStateObjectsSO;

        protected override IGameplayStateObject[] OnGetGameplayStateObjects() {
            return gameplayStateObjectsSO.Select(x => x?.GetGameplayStateObject()).Where(y => y != null).ToArray();
        }

    }
}