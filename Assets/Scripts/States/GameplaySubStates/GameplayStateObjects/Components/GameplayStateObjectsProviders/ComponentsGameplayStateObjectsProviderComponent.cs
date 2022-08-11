using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{
    public class ComponentsGameplayStateObjectsProviderComponent : GameplayStateObjectsProviderComponent
    {
        public GameplayStateObjectProviderComponent[] gameplayStateObjectComponents;


        protected override IGameplayStateObject[] OnGetGameplayStateObjects() {
            return gameplayStateObjectComponents.Select(x => x?.GetGameplayStateObject()).Where(y => y != null).ToArray();
        }

    }
}