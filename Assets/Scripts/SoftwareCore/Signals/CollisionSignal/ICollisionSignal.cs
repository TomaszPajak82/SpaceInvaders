using UnityEngine;

namespace SoftwareCore.Signals.CollisionSignal
{
    public interface ICollisionSignal
    {
        Collider PrimaryCollider { get; }
        Collider SecondaryCollider { get; }
    }

}