using Cysharp.Threading.Tasks;
using SoftwareCore.Tasks.Components;
using System;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Kill.Components
{
    public class UniTaskKillComponent : KillComponent
    {
        public UniTaskComponent target;

        UniTask<KillComponent> CurrentTask { get; set; }

        protected override bool OnIsKilling() {
            if (target != null) {
                return target.IsRunning;
            } else {
                return false;
            }
        }

        protected override UniTask<KillComponent> OnKill() {

            if (target != null) {

                if (target.IsRunning) {
                    return CurrentTask;
                }

                return CurrentTask = innerKillTaskAsync();
            } else {
                return UniTask.FromResult<KillComponent>(this);
            }
        }

        async UniTask<KillComponent> innerKillTaskAsync() {

            try {
                var innerTask = target.StartTask();
                await innerTask;
            } catch {
                throw;
            }

            return this;
        }
    }
}