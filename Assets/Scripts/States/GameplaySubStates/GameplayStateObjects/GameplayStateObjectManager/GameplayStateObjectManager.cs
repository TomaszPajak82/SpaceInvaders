using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    public class GameplayStateObjectManager : IGameplayStateObjectManager
    {
        public IGameplayStateObjectsSource GameplayStateObjectsSource { get; protected set; }

        IEnumerator<IGameplayStateObject> GameplayStateObjects { get; set; }

        public IGameplayStateObject GetNextGameplayStateObject(GameplaySubStatesData data) {

            IGameplayStateObject nextGameplayStateObject = null;

            if (GameplayStateObjectsSource != null) {

                GameplayStateObjects ??= GameplayStateObjectsSource.GetGameplayStateObjects().GetEnumerator();

                if (GameplayStateObjects != null) {

                    if (GameplayStateObjects.MoveNext()) {

                        nextGameplayStateObject = GameplayStateObjects.Current;

                    } else { // loop

                        this.Reset();

                        GameplayStateObjects = GameplayStateObjectsSource.GetGameplayStateObjects().GetEnumerator();
                        if (GameplayStateObjects.MoveNext()) {
                            nextGameplayStateObject = GameplayStateObjects.Current;
                        }
                    }

                }

            }

            return nextGameplayStateObject;
        }

        public void Reset() {
            (GameplayStateObjects as IDisposable)?.Dispose();
            GameplayStateObjects = null;
        }

        public void SetGameplayStateObjectsSource(IGameplayStateObjectsSource gameplayStateObjectsSource) {
            GameplayStateObjectsSource = gameplayStateObjectsSource;
            GameplayStateObjects = null;
        }
    }
}