using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    public interface IGameplayStateObject 
    {
        void Initialize(GameplaySubStatesData data);

        IGameplayStateObjectSession StartPrePlaySession();

        IGameplayStateObjectSession StartPlaySession();

        void Cleanup();

    }
}