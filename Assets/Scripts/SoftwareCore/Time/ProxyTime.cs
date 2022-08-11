using UnityEngine;

namespace SoftwareCore.Time
{
    public sealed class ProxyTime : ITime
    {
        
        public float DeltaTime => UnityEngine.Time.deltaTime;

        public float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime;
    }
}

