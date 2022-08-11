using System;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.UI.Binders.Components
{
    public abstract class ButtonUIBinderComponent : MonoBehaviour
    {

        public event EventHandler Clicked;

        protected void RaiseClickedEvent() {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void OnDestroy() {
            OnOnDestroy();
            Clicked = null;
        }

        protected virtual void OnOnDestroy() { }

    }
}