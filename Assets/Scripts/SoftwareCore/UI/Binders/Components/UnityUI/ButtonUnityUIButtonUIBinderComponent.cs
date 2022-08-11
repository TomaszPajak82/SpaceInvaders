using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SoftwareCore.UI.Binders.Components.UnityUI
{

    [RequireComponent(typeof(Button))]
    public class ButtonUnityUIButtonUIBinderComponent : ButtonUIBinderComponent
    {
        private void Awake() {
            Button button = this.GetComponent<Button>();
            button.onClick.AddListener(ButtonClicked);
        }

        void ButtonClicked() {
            this.RaiseClickedEvent();
        }
    }
}
