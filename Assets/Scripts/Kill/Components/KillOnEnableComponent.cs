using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Kill.Components
{
    public class KillOnEnableComponent : MonoBehaviour
    {
        public KillComponent target;

        private void OnEnable() {
            if (target != null) {
                KillAsync();
            }
        }

        async void KillAsync() {
            if (target != null) {
                if (!target.IsKilling()) {
                    try { await UniTask.WhenAll(target.Kill()); } catch (OperationCanceledException) { }
                }
            }
        }
    }
}