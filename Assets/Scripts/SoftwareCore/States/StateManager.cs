
using SoftwareCore.Time;
using Zenject;

namespace SoftwareCore.States
{
    public class StateManager : IStateManager, ITickable, IFixedTickable
    {
        ITime Time { get; set; }


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


        public StateManager(ITime time) {
            this.Time = time;
        }

        public void Tick() {
            if (state != null) {
                state.OnUpdate(Time.DeltaTime);
            }
        }

        public void FixedTick() {
            if (state != null) {
                state.OnUpdate(Time.FixedDeltaTime);
            }
        }


    }

}