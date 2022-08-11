using SoftwareCore.Audio;
using SoftwareCore.UI.Binders.Components;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SoftwareCore.Audio.Components
{
    public class PlaySoundOnButtonPressedComponent : MonoBehaviour
    {

        public AudioClipInfo sound;

        public ButtonUIBinderComponent buttonBinder;

        public bool findBinder = true;

        IAudioManager AudioManager { get; set; }

        [Inject]
        void Construct(IAudioManager audioManager) {
            AudioManager = audioManager;
        }

        private void Awake() {
            if (findBinder && buttonBinder == null) {
                buttonBinder = this.GetComponent<ButtonUIBinderComponent>();
            }
        }

        private void OnEnable() {
            if(buttonBinder != null) {
                buttonBinder.Clicked += ButtonBinder_Clicked;
            }
        }

        private void OnDisable() {
            if (buttonBinder != null) {
                buttonBinder.Clicked -= ButtonBinder_Clicked;
            }
        }

        private void ButtonBinder_Clicked(object sender, System.EventArgs e) {
            AudioManager.PlayOneShot(sound);
        }


    }
}