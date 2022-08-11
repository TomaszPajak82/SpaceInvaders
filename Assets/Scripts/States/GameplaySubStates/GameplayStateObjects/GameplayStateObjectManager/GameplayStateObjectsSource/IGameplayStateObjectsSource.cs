using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    public interface IGameplayStateObjectsSource 
    {
        IEnumerable<IGameplayStateObject> GetGameplayStateObjects();
    }
}