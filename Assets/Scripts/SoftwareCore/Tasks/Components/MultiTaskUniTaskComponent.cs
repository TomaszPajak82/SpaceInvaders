
using Cysharp.Threading.Tasks;
using SoftwareCore.Tasks.Components;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace SpaceInvaders.Health.Components
{
    public class MultiTaskUniTaskComponent : UniTaskComponent
    {
        public UniTaskComponent[] taskComponents;

        public bool searchForTasks = false;
        public Transform[] searchRoots;

        UniTask<object> CurrentTask { get; set; }

        public override bool IsRunning {
            get {

                Initialize();

                foreach (var task in taskComponents) {
                    if (task != null) {
                        if (task.IsRunning) {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        bool IsInitialized { get; set; }

        private void Awake() {
            Initialize();
        }

        private void Initialize() {
            if (IsInitialized) {
                return;
            }

            IsInitialized = true;
            if (searchForTasks && searchRoots != null) {

                taskComponents = searchRoots.
                    Where(x => x != null)
                    .SelectMany(x => x.GetComponentsInChildren<UniTaskComponent>(true))
                    .Where(x => x != this).Distinct().ToArray();
            }
        }

        protected override UniTask<object> OnStartTask() {

            Initialize();

            if (this.IsRunning) {
                return CurrentTask;
            }

            if (taskComponents.Length > 0) {
                return CurrentTask = innerTaskAsync();
            } else {
                return UniTask.FromResult<object>(this);
            }

        }

        async UniTask<object> innerTaskAsync() {

            var tasks = Array.ConvertAll(taskComponents, x => x.StartTask());

            try {
                await UniTask.WhenAll(tasks);
            } catch (OperationCanceledException) {
                
            }

            return this;
        }

    }
}