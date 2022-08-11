using System.Collections;
using UnityEngine;

namespace SoftwareCore.UI.Binders.Components
{
    public abstract class ImageUIBinderComponent : MonoBehaviour
    {

        public Sprite GetImage() {
            return OnGetImage();
        }

        protected abstract Sprite OnGetImage();

        public void SetImage(Sprite value) {
            OnSetImage(value);
        }

        protected abstract void OnSetImage(Sprite value);
    }
}