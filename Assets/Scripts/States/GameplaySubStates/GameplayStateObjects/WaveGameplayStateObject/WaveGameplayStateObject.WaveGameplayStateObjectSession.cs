using SpaceInvaders.Settings;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    partial class WaveGameplayStateObject
    {
        class WaveGameplayStateObjectSession : IGameplayStateObjectSession
        {

            public WaveGameplayStateObject WaveGameplayStateObject { get; protected set; }

            public bool IsCompleted { get; protected set; }



            public void Update(float dTime) {
                OnUpdate(dTime);
            }

            protected virtual void OnUpdate(float dTime) { }

            public void FixedUpdate(float dTime) {
                OnFixedUpdate(dTime);
            }

            protected virtual void OnFixedUpdate(float dTime) { }

        }
    }
}