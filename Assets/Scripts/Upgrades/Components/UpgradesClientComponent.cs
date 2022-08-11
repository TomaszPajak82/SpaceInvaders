using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Upgrades.Components
{
    public abstract class UpgradesClientComponent : MonoBehaviour
    {

        public IEnumerable<UpgradeScriptableObject> GetUpgrades() {
            return OnGetUpgrades();
        }

        protected abstract IEnumerable<UpgradeScriptableObject> OnGetUpgrades();


        public bool ContainsUpgrade(UpgradeScriptableObject upgrade) {
            return OnContainsUpgrade(upgrade);
        }

        protected abstract bool OnContainsUpgrade(UpgradeScriptableObject upgrade);



        public bool AddUpgrade(UpgradeScriptableObject upgrade) {
            return OnAddUpgrade(upgrade);
        }

        protected abstract bool OnAddUpgrade(UpgradeScriptableObject upgrade);

        
        public int AddUpgradesRange(IEnumerable<UpgradeScriptableObject> upgrades) {
            return OnAddUpgradesRange(upgrades);
        }

        protected abstract int OnAddUpgradesRange(IEnumerable<UpgradeScriptableObject> upgrades);
        

        public void ClearUpgrades() {
            OnClearUpgrades();
        }

        protected abstract void OnClearUpgrades();
    }

}