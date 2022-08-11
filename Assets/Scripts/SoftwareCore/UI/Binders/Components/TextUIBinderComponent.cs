using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCore.UI.Binders.Components
{
    public abstract class TextUIBinderComponent : MonoBehaviour
    {
        public string GetText() {
            return OnGetText();
        }

        protected abstract string OnGetText();

        public void SetText(string value) {
            OnSetText(value);
        }

        protected abstract void OnSetText(string value);
    }
}