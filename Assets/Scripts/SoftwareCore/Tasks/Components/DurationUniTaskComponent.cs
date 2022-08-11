using Cysharp.Threading.Tasks;
using SoftwareCore.Progress;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace SoftwareCore.Tasks.Components
{
    public class DurationUniTaskComponent : UniTaskComponent
    {

        public ProgressReceiverComponent progressReceiver;

        public float duration = 1;

        UniTask<object> CurrentTask { get; set; }


        bool isRunning = false;
        public override bool IsRunning => isRunning;

        CancellationTokenSource CancelationTokenSource { get; set; }

        private void OnDisable() {

            UpdateProgress(0);

            if (CancelationTokenSource != null) {
                CancelationTokenSource.Cancel();
                CancelationTokenSource.Dispose();
                CancelationTokenSource = null;
            }
        }

        protected override UniTask<object> OnStartTask() {

            if (isRunning) {
                return CurrentTask;
            }

            if (CancelationTokenSource != null) {
                CancelationTokenSource.Cancel();
                CancelationTokenSource.Dispose();
            }

            CancelationTokenSource = new CancellationTokenSource();
            return CurrentTask = ProcessProgressAsync(CancelationTokenSource.Token);
        }

        async UniTask<object> ProcessProgressAsync(CancellationToken cancellationToken) {

            isRunning = true;

            try {
                float elapsedTime = 0;

                while (elapsedTime < duration) {

                    await UniTask.NextFrame(PlayerLoopTiming.Update, cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();

                    elapsedTime += UnityEngine.Time.deltaTime;

                    float progresss = duration != 0 ? Mathf.Clamp01(elapsedTime / duration) : 1;

                    UpdateProgress(progresss);

                    if (progresss >= 1) {
                        break;
                    }

                }
            } finally {
                isRunning = false;
            }

            return this;
        }

        void UpdateProgress(float progresss) {
            if (progressReceiver != null) {
                progressReceiver.UpdateProgress(progresss);
            }
        }

    }
}