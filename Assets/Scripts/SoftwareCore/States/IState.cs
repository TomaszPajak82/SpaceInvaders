using UnityEditor;
using UnityEngine;

namespace SoftwareCore.States
{
    public interface IState
    {
        void OnEnter(IStateManager stateManager);

        void OnExit();

        void OnUpdate(float dTime);

        void OnFixedUpdate(float dTime);
    }

}