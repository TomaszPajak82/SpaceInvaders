using SpaceInvaders.States.GameplaySubStates;
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using SpaceInvaders.UI.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpaceInvaders.UI.Views.Components;
using SpaceInvaders.Upgrades.Components;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects.Components
{
    public class UpgradeSelectionGameplayStateObjectProviderComponent : GameplayStateObjectProviderComponent,IGameplayStateObject
    {
        public GameObject[] gameObjectsToActivate;

        public UpgradeSelectionViewComponent upgradeSelectionView;

        public int maxUpgradesChoiceCount = 2;

        public UpgradeInfoScriptableObject[] allUpgrades;

        UpgradeSelectionViewModel ViewModel { get; set; }

        GameplaySubStatesData Data { get; set; }

        DummyGameplayStateObjectSession PreGameplaySession { get; set; }

        protected override IGameplayStateObject OnGetGameplayStateObject() {
            return this;
        }

        public void Initialize(GameplaySubStatesData data) {

            if(ViewModel == null) {
                ViewModel = new UpgradeSelectionViewModel();
                ViewModel.UpgradesSelection = null;
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }

            if(PreGameplaySession == null) {
                PreGameplaySession = new DummyGameplayStateObjectSession(false, false);
            }

            Data = data;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(ViewModel.SelectedUpgrade)) {

                if(ViewModel.SelectedUpgrade != null) {

                    if(Data.PlayerGO != null) {
                        UpgradesClientComponent ucc = Data.PlayerGO.GetComponent<UpgradesClientComponent>();
                        if(ucc != null) {
                            ucc.AddUpgrade(ViewModel.SelectedUpgrade.upgrade);
                        }
                    }

                    PreGameplaySession.IsCompleted = true;
                }
            }
        }

        List<UpgradeInfoScriptableObject> currentSelection = new List<UpgradeInfoScriptableObject>();
        public IGameplayStateObjectSession StartPrePlaySession() {

            PreGameplaySession.IsCompleted = true;

            if (ViewModel != null) {
                ViewModel.SelectedUpgrade = null;

                UpgradeInfoScriptableObject[] upgradesChoice = Array.Empty<UpgradeInfoScriptableObject>();

                if (Data.PlayerGO != null) {
                    UpgradesClientComponent ucc = Data.PlayerGO.GetComponent<UpgradesClientComponent>();
                    if (ucc != null) {
                        var availableUpgrades = allUpgrades.Select(x => x).Where(y => !ucc.ContainsUpgrade(y.upgrade)).ToArray();

                        upgradesChoice = CreateArrayWithRandomItemsWithoutRepetition(availableUpgrades, maxUpgradesChoiceCount);
                    }
                }

                ViewModel.UpgradesSelection = upgradesChoice;


                if (this.upgradeSelectionView != null) {
                    this.upgradeSelectionView.ViewModel = ViewModel;

                    if (upgradesChoice.Length > 0) {
                        PreGameplaySession.IsCompleted = false;
                    }
                }

 
            }

            if (!PreGameplaySession.IsCompleted) {
                SetGameObjectsActivation(true);
            }

            return PreGameplaySession;
        }

        T[] CreateArrayWithRandomItemsWithoutRepetition<T>(T[] source, int maxLength) {

            int currentChoicesCount = Mathf.Min(source.Length, maxUpgradesChoiceCount);
            if (currentChoicesCount > 0) {

                T[] choicesArr = new T[currentChoicesCount];
                
                var randomIndexes = Enumerable.Range(0, source.Length).OrderBy(x => UnityEngine.Random.value).Take(currentChoicesCount);

                int arrIndex = 0;
                foreach(var index in randomIndexes) {
                    choicesArr[arrIndex] = source[index];
                    arrIndex++;
                }
                return choicesArr;

            } else {
                return Array.Empty<T>();
            }
        }


        public IGameplayStateObjectSession StartPlaySession() {
            SetGameObjectsActivation(false);
            return DummyGameplayStateObjectSession.CompletedSession;
        }


        public void Cleanup() {
            SetGameObjectsActivation(false);
        }

        void SetGameObjectsActivation(bool active) {
            foreach (var go in gameObjectsToActivate) {
                if (go != null) {
                    go.SetActive(active);
                }
            }
        }

    }
}