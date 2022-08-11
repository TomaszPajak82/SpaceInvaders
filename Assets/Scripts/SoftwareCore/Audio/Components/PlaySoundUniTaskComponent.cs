using Cysharp.Threading.Tasks;
using SoftwareCore.Tasks.Components;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SoftwareCore.Audio.Components
{
    public class PlaySoundUniTaskComponent : UniTaskComponent
    {
        public AudioClipInfo audioClipInfo;

        public override bool IsRunning => false;

        IAudioManager AudioManager { get; set; }

        [Inject]
        void Construct(IAudioManager audioManager) {
            AudioManager = audioManager;
        }

        protected override UniTask<object> OnStartTask() {

            AudioManager.PlayOneShot(audioClipInfo);

            return UniTask.FromResult<object>(this);
        }
    }
}