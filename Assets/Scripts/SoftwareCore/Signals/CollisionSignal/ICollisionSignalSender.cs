using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Signals.CollisionSignal
{
    public interface ICollisionSignalSender
    {
        void Signal(ICollisionSignal collisionSignal);
    }

}