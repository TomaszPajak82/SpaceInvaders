using SoftwareCore.Pool;
using System;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.Audio
{
    //to implement (info when sound is playing)
    public interface ISound: IDisposable
    {
        bool IsPlaying { get; }

        float Time { get; }

        bool Loop { get; set; }

        void Play();

        void Stop();

        bool IsDisposed { get; }
    }

    public class AudioSourceSound:ISound
    {
        AudioSource AudioSource { get; set; }

        public AudioClip Clip {
            get => AudioSource.clip;
            set => AudioSource.clip = value;
        }

        public bool Loop {
            get => AudioSource.loop;
            set => AudioSource.loop = value;
        }

        public bool IsPlaying {
            get => AudioSource.isPlaying;
        }

        public float Time => AudioSource.time;

        public bool IsDisposed {
            get => AudioSource == null;
        }

        public AudioSourceSound(AudioSource audioSource) {
            AudioSource = audioSource ?? throw new ArgumentNullException(nameof(audioSource));
        }

        public void Play() {
            AudioSource.Play();
        }

        public void Stop() {
            AudioSource.Stop();
        }

        public void Dispose() {
            if (IsDisposed) {
                return;
            }

            AudioSource.Stop();
            PoolUtils.ReleaseToPoolOrDestroy(AudioSource.gameObject);

        }
    }
}