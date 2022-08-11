using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{

    public class GameplayStateObjectsSourceComponent : MonoBehaviour, IGameplayStateObjectsSource
    {

        public GameplayStateObjectsProviderComponent[] gameplayStateObjectsProviders;

        IGameplayStateObjectManager GamplayStateObjectManager { get; set; }

        [Inject]
        public void Construct(IGameplayStateObjectManager gameplayStateObjectManager) {
            GamplayStateObjectManager = gameplayStateObjectManager;

            GamplayStateObjectManager.SetGameplayStateObjectsSource(this);
        }

        public IEnumerable<IGameplayStateObject> GetGameplayStateObjects() {
            return gameplayStateObjectsProviders.SelectMany(x => x.GetGameplayStateObjects());
        }
    }

}
