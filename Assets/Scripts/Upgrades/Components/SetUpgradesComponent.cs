using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Upgrades.Components
{
    public class SetUpgradesComponent : MonoBehaviour
    {

        public UpgradesClientComponent target;

        public UpgradeScriptableObject[] upgrades;

        private void Start() {
            target.AddUpgradesRange(upgrades);
        }

    }
}

