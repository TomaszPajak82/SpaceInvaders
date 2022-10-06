using SoftwareCore.Signals.CollisionSignal;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Signals.Components
{

    public class SignalCollisionOnTriggerEnterComponent : MonoBehaviour
    {
        public Collider primaryCollider;

        ICollisionSignalSender CollisionSignalSender { get; set; }

        CollisionSignal ReusableCollisionSignal { get; set; }

        [Inject]
        void Construct(ICollisionSignalSender collisionSignalSender) {

            ReusableCollisionSignal = new CollisionSignal(null, null);
            CollisionSignalSender = collisionSignalSender;
        }


        private void OnTriggerEnter(Collider other) {
            
            if (ReusableCollisionSignal == null) {
                ReusableCollisionSignal = null;
            }

            ReusableCollisionSignal.PrimaryCollider = primaryCollider;
            ReusableCollisionSignal.SecondaryCollider = other;

            CollisionSignalSender.Signal(ReusableCollisionSignal);
        }

    }

}