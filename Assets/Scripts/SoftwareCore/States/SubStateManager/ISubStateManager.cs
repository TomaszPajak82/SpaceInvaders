using System.Collections;
using UnityEngine;

namespace SoftwareCore.States
{
    public interface ISubStateManager : IStateManager
    {
        public void Update(float dTime);

        public void FixedUpdate(float dTime);

    }
}
