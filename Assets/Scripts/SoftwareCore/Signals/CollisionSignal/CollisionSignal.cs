
using UnityEngine;

namespace SoftwareCore.Signals.CollisionSignal
{
    public class CollisionSignal : ICollisionSignal
    {
        public Collider PrimaryCollider { get; set; }

        public Collider SecondaryCollider { get; set; }

        public CollisionSignal(Collider primaryCollider, Collider secondaryCollider) {
            PrimaryCollider = primaryCollider;
            SecondaryCollider = secondaryCollider;
        }
    }
}
