using SoftwareCore.UI.ViewModel;
using SpaceInvaders.Upgrades.Components;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.UI.ViewModels
{
    public class UpgradeSelectionViewModel : ViewModel
    {

        UpgradeInfoScriptableObject selectedUpgrade;
        public UpgradeInfoScriptableObject SelectedUpgrade {
            get {
                return selectedUpgrade;
            }
            set {
                SetProperty(ref selectedUpgrade, value);
            }
        }

        UpgradeInfoScriptableObject[] upgradesSelection;

        public UpgradeInfoScriptableObject[] UpgradesSelection {
            get {
                return upgradesSelection;
            }
            set {
                SetProperty(ref upgradesSelection, value);
            }
        }


    }
}