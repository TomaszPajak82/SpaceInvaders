using System.Collections;
using UnityEngine;

namespace SoftwareCore.Audio
{
    public class AudioManager : IAudioManager
    {
        AudioSource MainAudioSource { get; set; }

        GameObject TemplateAudioSourceGO { get; set; }

        public AudioManager() {
            GameObject go = new GameObject("MainAudioSource");
            MainAudioSource = go.AddComponent<AudioSource>();

            TemplateAudioSourceGO = GameObject.Instantiate<GameObject>(go);
            go.name = "AudioSource";
        }

        public void PlayOneShot(AudioClip clip,float volumeScale) {
            MainAudioSource.PlayOneShot(clip, volumeScale);
        }

        public void PlayOneShot(AudioClipInfo clipInfo) {
            var audioData = clipInfo.GetData();
            MainAudioSource.PlayOneShot(audioData.clip, audioData.volume);
        }

        public ISound Play(AudioClipInfo clipInfo) {

            GameObject audioGO = Pool.PoolUtils.RetriveOrCreateObject(TemplateAudioSourceGO, Vector3.zero, Quaternion.identity, null);
            AudioSource audioSource = audioGO.GetComponent<AudioSource>();

            var audioData = clipInfo.GetData();
            audioSource.clip = audioData.clip;
            audioSource.volume = audioData.volume;

            AudioSourceSound sound = new AudioSourceSound(audioSource);
            sound.Play();

            return sound;
        }

    }
}