using System;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.Pool.GameObjectPool
{
    public abstract class PoolClientComponent : MonoBehaviour
    {

        public abstract bool IsReleased { get; protected set; }

        public void Release() {
            if (!IsReleased) {
                OnRelease();
            }
        }

        protected abstract void OnRelease();

        protected void OnDestroy() {
            OnOnDestroyed();
        }

        protected virtual void OnOnDestroyed() { }


    }

}