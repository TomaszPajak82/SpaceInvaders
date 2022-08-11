using System.Collections;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    public interface IGameplayStateObjectSession 
    {
        void Update(float dTime);
        void FixedUpdate(float dTime);

        bool IsCompleted { get; }
    }
}