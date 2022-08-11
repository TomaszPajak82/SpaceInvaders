using SoftwareCore.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SoftwareCore.States.Components
{
    //this class lets us change statess via UnityEvent and similar.
    public class MethodTriggeredStateComponent : GameObjectsActivatingStateComponent
    {

        [Space]
        public StateComponent state1;
        public StateComponent state2;
        public StateComponent state3;
        public StateComponent state4;
        public StateComponent state5;
        public StateComponent state6;
        public StateComponent state7;
        public StateComponent state8;
        public StateComponent state9;
        public StateComponent state10;


        [Inject]
        public void Construct() {

        }

        protected void TransitionToState(IState state) {
            if (CurrentStateManager != null) {
                CurrentStateManager.State = state;
            }
        }

        public void Trigger_StartState1() {
            TransitionToState(state1);
        }

        public void Trigger_StartState2() {
            TransitionToState(state2);
        }

        public void Trigger_StartState3() {
            TransitionToState(state3);
        }

        public void Trigger_StartState4() {
            TransitionToState(state4);
        }

        public void Trigger_StartState5() {
            TransitionToState(state5);
        }

        public void Trigger_StartState6() {
            TransitionToState(state6);
        }

        public void Trigger_StartState7() {
            TransitionToState(state7);
        }

        public void Trigger_StartState8() {
            TransitionToState(state8);
        }

        public void Trigger_StartState9() {
            TransitionToState(state9);
        }

        public void Trigger_StartState10() {
            TransitionToState(state10);
        }

    }
}