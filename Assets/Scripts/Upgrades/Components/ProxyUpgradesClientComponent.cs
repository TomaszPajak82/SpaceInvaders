using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceInvaders.Upgrades.Components
{
    public class ProxyUpgradesClientComponent : UpgradesClientComponent
    {

        public UpgradesClientComponent target;

        protected override IEnumerable<UpgradeScriptableObject> OnGetUpgrades() {
            if (target != null) {
                return target.GetUpgrades();
            } else {
                return Enumerable.Empty<UpgradeScriptableObject>();
            }
        }

        protected override bool OnContainsUpgrade(UpgradeScriptableObject upgrade) {
            if (target != null) {
                return target.ContainsUpgrade(upgrade);
            } else {
                return false;
            }
        }

        protected override bool OnAddUpgrade(UpgradeScriptableObject upgrade) {
            if(target != null) {
                return target.AddUpgrade(upgrade);
            } else {
                return false;
            }
        }

        protected override int OnAddUpgradesRange(IEnumerable<UpgradeScriptableObject> upgrades) {
            if (target != null) {
                return target.AddUpgradesRange(upgrades);
            } else {
                return 0;
            }
        }



        protected override void OnClearUpgrades() {
            if (target != null) {
                target.ClearUpgrades();
            }
        }
    }
}