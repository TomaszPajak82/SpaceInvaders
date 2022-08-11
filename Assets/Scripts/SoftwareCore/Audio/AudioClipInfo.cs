using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Audio
{
    [System.Serializable]
    public class AudioClipInfo
    {
        [SerializeField]
        AudioClip audioClip;
        public float volumeScale=1;

        public (AudioClip clip,float volume) GetData() {
            return new(audioClip, volumeScale);
        }
    }
}