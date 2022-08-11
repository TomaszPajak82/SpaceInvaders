using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Audio
{
    public interface IAudioManager
    {
        void PlayOneShot(AudioClip clip, float volumeScale = 1);
        void PlayOneShot(AudioClipInfo clipInfo);

        ISound Play(AudioClipInfo clipInfo);
    }
}