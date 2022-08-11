
using UnityEngine;

namespace SpaceInvaders.Upgrades.Components
{

    [CreateAssetMenu(fileName = "UpgradeSO", menuName = "Upgrade/Create", order = 1)]
    public class UpgradeScriptableObject:ScriptableObject
    {
        [SerializeField]
        bool isConsumable;
        public bool IsConsumable { get => isConsumable; }
    }
}