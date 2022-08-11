using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Cysharp;
using SoftwareCore.States;
using SoftwareCore.UI.Presenters.Components;

namespace SoftwareCore.States.Components
{
    public class LoadingStateComponent : GameObjectsActivatingStateComponent
    {

        public AssetReferenceGameObject contentToLoad;

        public TextUIPresenterComponent loadPercentageText;

        public float LastProgress { get; set; }

        UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> AsyncLoadingHandle { get; set; }

        IInstantiator Instantiator { get; set; }

        [Inject]
        public void Construct(IInstantiator instantiator) {
            Instantiator = instantiator;
        }

        protected override void OnOnEnter(IStateManager stateManager) {
            base.OnOnEnter(stateManager);

            LastProgress = 0;

            if (!string.IsNullOrWhiteSpace(contentToLoad.AssetGUID)) {

                AsyncLoadingHandle = Addressables.LoadAssetAsync<GameObject>(contentToLoad);
                AsyncLoadingHandle.Completed += x => {

                    GameObject resultPrefab = AsyncLoadingHandle.Result;

                    if (resultPrefab != null) {
                        GameObject go = Instantiator.InstantiatePrefab(resultPrefab);
                    }

                };

            }


            UpdateUI();
        }


        protected override void OnOnUpdate(float dTime) {
            base.OnOnUpdate(dTime);

            if (AsyncLoadingHandle.IsValid()) {

                float percentCompleted = AsyncLoadingHandle.PercentComplete;
                if (percentCompleted != LastProgress) {

                    LastProgress = percentCompleted;
                    UpdateUI();
                }
            }

        }

        void UpdateUI() {

            if (loadPercentageText != null) {
                loadPercentageText.SetText(LastProgress.ToString() + "%");
            }
        }

    }
}