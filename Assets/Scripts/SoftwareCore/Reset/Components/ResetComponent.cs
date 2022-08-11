using System;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.Reset.Components
{

    //reset, will be used by Pooling
    public abstract class ResetComponent : MonoBehaviour
    {

        public event EventHandler Reseting;

        public event EventHandler Reseted;

        public void Reset() {
            Reseting?.Invoke(this, EventArgs.Empty);
            OnReset();
            Reseted?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void OnReset();

        protected void OnDestroy() {
            OnOnDestroy();
        }

        protected virtual void OnOnDestroy() {

        }
    }
}