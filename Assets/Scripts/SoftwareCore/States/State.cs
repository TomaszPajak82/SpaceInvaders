using UnityEditor;
using UnityEngine;

namespace SoftwareCore.States
{
    public abstract class State : IState
    {

        public bool IsRunning { get; private set; }

        public IStateManager CurrentStateManager { get; private set; }

        public void OnEnter(IStateManager stateManager) {
            CurrentStateManager = stateManager;
            IsRunning = true;
            OnOnEnter(stateManager);
        }

        protected abstract void OnOnEnter(IStateManager stateManager);


        public void OnExit() {
            OnOnExit();
            CurrentStateManager = null;
            IsRunning = false;
        }

        protected abstract void OnOnExit();


        public void OnFixedUpdate(float dTime) {
            OnOnFixedUpdate(dTime);
        }

        protected abstract void OnOnFixedUpdate(float dTime);


        public void OnUpdate(float dTime) {
            OnOnUpdate(dTime);
        }

        protected abstract void OnOnUpdate(float dTime);

    }

}