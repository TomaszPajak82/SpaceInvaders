using System.Collections;
using UnityEngine;

namespace SoftwareCore.Reset.Components
{
    public class ProxyResetComponent : ResetComponent
    {
        public ResetComponent target;

        protected override void OnReset() {
            if(target != null) {
                target.Reset();
            }
        }

    }
}