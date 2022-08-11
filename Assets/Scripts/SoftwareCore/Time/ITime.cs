using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCore.Time
{
    public interface ITime
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }

    }

}