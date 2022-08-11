using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Kill.Components
{
    public class ProxyKillComponent : KillComponent
    {
        public KillComponent target;

        UniTask<KillComponent> CurrentTask { get; set; }

        protected override bool OnIsKilling() {
            if (target != null) {
                return target.IsKilling();
            } else {
                return false;
            }
        }

        protected override UniTask<KillComponent> OnKill() {

            if (target != null) {

                if (target.IsKilling()) {
                    return CurrentTask;
                }

                return CurrentTask = innerKillTaskAsync();
            } else {
                return UniTask.FromResult<KillComponent>(this);
            }
        }

        async UniTask<KillComponent> innerKillTaskAsync() {

            try {
                var innerTask = target.Kill();
                await innerTask;
            } catch {
                throw;
            }

            return this;
        }
    }
}