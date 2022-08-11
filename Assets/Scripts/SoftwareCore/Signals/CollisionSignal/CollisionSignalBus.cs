using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SoftwareCore.Signals.CollisionSignal
{
    public class CollisionSignalBus : ICollisionSignalBus
    {

        public event EventHandler<ICollisionSignal> Received;

        SignalBus SignalBus { get; set; }

        public CollisionSignalBus(SignalBus signalBus) {
            SignalBus = signalBus;

            SignalBus.Subscribe<ICollisionSignal>(SignalBus_Signaled);
        }

        void SignalBus_Signaled(ICollisionSignal collisionSignal) {
            Received?.Invoke(this, collisionSignal);
        }

        public void Signal(ICollisionSignal collisionSignal) {
            SignalBus.Fire<ICollisionSignal>(collisionSignal);
        }

        /*
         * 
        private bool disposedValue;
        public void Dispose() {
            if (!disposedValue) {
                Received = null;

                SignalBus.Unsubscribe<ICollisionSignal>(SignalBus_Signaled);
                disposedValue = true;
            }
        }
        */
    }
}