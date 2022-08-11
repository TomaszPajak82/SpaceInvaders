
using SoftwareCore.UI.Binders.Components;
using SpaceInvaders.Upgrades.Components;
using System;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.UI.Presenters.Components
{
    
    public class UpgradeSelectionItemUIPresenterComponent : MonoBehaviour
    {

        public TextUIBinderComponent textBinder;
        public ImageUIBinderComponent imageBinder;
        public ButtonUIBinderComponent buttonBinder;

        public UpgradeInfoScriptableObject UpgradeInfo { get; protected set; }

        public event EventHandler Selected;

        private void OnEnable() {
            if (buttonBinder != null) {
                buttonBinder.Clicked += ButtonBinder_Clicked;
            }

            UpdateDisplayData();
        }

        private void OnDisable() {
            if (buttonBinder != null) {
                buttonBinder.Clicked -= ButtonBinder_Clicked;
            }
        }

        private void ButtonBinder_Clicked(object sender, System.EventArgs e) {
            Selected?.Invoke(this, EventArgs.Empty);
        }

        public void SetDisplayData(UpgradeInfoScriptableObject upgradeInfo) {
            UpgradeInfo = upgradeInfo;
            UpdateDisplayData();
        }

        void UpdateDisplayData() {

            Sprite currentImage = UpgradeInfo?.image;
            imageBinder?.SetImage(currentImage);

            string currentText = UpgradeInfo?.text;
            textBinder?.SetText(currentText);
        }

    }
}