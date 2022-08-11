
using SoftwareCore.UI.Binders.Components;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SoftwareCore.UI.Presenters.Components
{
    public class UnityEventBasedButtonUIPresenterComponent : MonoBehaviour
    {

        public ButtonUIBinderComponent buttonBinder;

        public UnityEvent receiverEvent;

        // Start is called before the first frame update
        void OnEnable() {

            if (buttonBinder != null) {
                buttonBinder.Clicked += StartButtonBinder_ButtonClicked; ;
            }
        }


        private void OnDisable() {

            if (buttonBinder != null) {
                buttonBinder.Clicked -= StartButtonBinder_ButtonClicked; ;
            }
        }

        private void StartButtonBinder_ButtonClicked(object sender, System.EventArgs e) {
            if (receiverEvent != null) {
                receiverEvent.Invoke();
            }
        }

    }
}