using SoftwareCore.Audio;
using SoftwareCore.UI.Binders.Components;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SoftwareCore.Audio.Components
{
    public class PlaySoundOnEnableComponent : MonoBehaviour
    {

        public AudioClipInfo audioClipInfo;


        public bool loop = true;

        IAudioManager AudioManager { get; set; }

        ISound Sound { get; set; }

        [Inject]
        void Construct(IAudioManager audioManager) {
            AudioManager = audioManager;
        }

        private void OnEnable() {

            if (Sound == null) {
                Sound = AudioManager.Play(audioClipInfo);
                Sound.Loop = loop;
            }

            if (Sound != null && !Sound.IsDisposed) {
                Sound.Play();
            }
        }

        private void OnDisable() {

            if (Sound != null && !Sound.IsDisposed) {
                Sound.Stop();
            }
        }

    }
}