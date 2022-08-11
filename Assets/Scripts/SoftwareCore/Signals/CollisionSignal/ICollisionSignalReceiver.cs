using System;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Signals.CollisionSignal
{
    public interface ICollisionSignalReceiver
    {
        event EventHandler<ICollisionSignal> Received;
    }
}