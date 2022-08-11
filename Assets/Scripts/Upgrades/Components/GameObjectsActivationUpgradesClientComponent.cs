using SoftwareCore.Reset;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Upgrades.Components
{
    public class GameObjectsActivationUpgradesClientComponent : UpgradesClientComponent
    {

        [Serializable]
        public class UpgradeActivation
        {
            public UpgradeScriptableObject[] upgrades;
            public GameObject[] activates;

            public void Update(HashSet<UpgradeScriptableObject> currentUpgrades) {

                bool activate = true;
                foreach(var upgrade in upgrades) {
                    if (!currentUpgrades.Contains(upgrade)) {
                        activate = false;
                        break;
                    }
                }

                foreach(var go in activates) {
                    go.SetActive(activate);
                }

            }
        }

        public UpgradeActivation[] upgradesActivations;

        HashSet<UpgradeScriptableObject> CurrentUpgrades { get; set; } = new HashSet<UpgradeScriptableObject>();

        [SerializeField]
        bool autoresetOnReset = true;

        private void Awake() {
            if (autoresetOnReset) {
                ResetUtils.SubscribeToFirstResetingInParents(this.transform,
                    (x, y) => this.ClearUpgrades()
                    ); 
            }
        }

        protected override IEnumerable<UpgradeScriptableObject> OnGetUpgrades() {
            return CurrentUpgrades;
        }

        protected override bool OnContainsUpgrade(UpgradeScriptableObject upgrade) {
            return CurrentUpgrades.Contains(upgrade);
        }

        protected override bool OnAddUpgrade(UpgradeScriptableObject upgrade) {
            bool ret = CurrentUpgrades.Add(upgrade);
            UpdateUpgrades();
            return ret;
        }

        protected override int OnAddUpgradesRange(IEnumerable<UpgradeScriptableObject> upgrades) {

            int newCount = 0;
            foreach(var upgrade in upgrades) {
                if (CurrentUpgrades.Add(upgrade)) {
                    newCount++;
                }
            }

            UpdateUpgrades();
            return newCount;
        }


        protected override void OnClearUpgrades() {
            CurrentUpgrades.Clear();
            UpdateUpgrades();
        }

        void UpdateUpgrades() {

            foreach (var item in upgradesActivations) {
                item.Update(CurrentUpgrades);
            }

            if(CurrentUpgrades.RemoveWhere(x => x.IsConsumable) > 0) {
                foreach (var item in upgradesActivations) {
                    item.Update(CurrentUpgrades);
                }
            }

        }
    }
}