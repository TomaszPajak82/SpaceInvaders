using System.Collections;
using UnityEngine;

namespace SoftwareCore.Progress
{
    public class ConsoleLogProgressReceiverComponent : ProgressReceiverComponent
    {
        [SerializeField]
        float lastProgress;

        protected override void OnUpdateProgress(float progress) {
            lastProgress = progress;
            Debug.Log(progress);
        }
    }
}