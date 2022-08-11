using System.Collections;
using UnityEngine;

namespace SoftwareCore.States
{
    public class SubStateManager : ISubStateManager
    {
        IState state;

        public IState State {
            get => state;

            set {

                if (state == value) {
                    return;
                }

                state?.OnExit();

                state = value;

                state?.OnEnter(this);
            }
        }


        public SubStateManager() { }

        public void Update(float dTime) {
            if (state != null) {
                state.OnUpdate(dTime);
            }
        }

        public void FixedUpdate(float dTime) {
            if (state != null) {
                state.OnUpdate(dTime);
            }
        }

    }
}