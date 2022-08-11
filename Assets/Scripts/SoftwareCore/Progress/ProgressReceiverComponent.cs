using System.Collections;
using UnityEngine;

namespace SoftwareCore.Progress
{
    public abstract class ProgressReceiverComponent : MonoBehaviour
    {

        public void UpdateProgress(float progress) {
            OnUpdateProgress(progress);
        }

        protected abstract void OnUpdateProgress(float progress);

    }
}