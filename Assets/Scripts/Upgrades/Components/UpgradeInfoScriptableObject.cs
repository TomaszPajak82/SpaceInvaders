
using UnityEngine;

namespace SpaceInvaders.Upgrades.Components
{

    [CreateAssetMenu(fileName = "UpgradeInfoSO", menuName = "Upgrade/CreateInfo", order = 1)]
    public class UpgradeInfoScriptableObject : ScriptableObject
    {
        public UpgradeScriptableObject upgrade;

        public Sprite image;

        public string text;

    }
}