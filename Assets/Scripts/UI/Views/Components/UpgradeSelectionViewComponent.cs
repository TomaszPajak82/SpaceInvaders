using SoftwareCore.Pool;
using SpaceInvaders.States;
using SpaceInvaders.UI.Presenters.Components;
using SpaceInvaders.UI.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.UI.Views.Components
{
    public class UpgradeSelectionViewComponent : MonoBehaviour
    {
        public Transform container;

        public GameObject itemPrefab;

        UpgradeSelectionViewModel viewModel;
        public UpgradeSelectionViewModel ViewModel {
            get { return viewModel; }
            set { 

                if(viewModel != value) {
                    if(viewModel != null) {
                        viewModel.PropertyChanged -= ViewModel_PropertyChanged;
                    }

                    viewModel = value;

                    if (viewModel != null) {
                        viewModel.PropertyChanged += ViewModel_PropertyChanged;
                    }

                    UpdateView();
                }

            }
        }

        IInstantiator Instantiator { get; set; }

        [Inject]
        void Construct(IInstantiator instantiator) {
            Instantiator = instantiator;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            UpdateView();
        }

        List<UpgradeSelectionItemUIPresenterComponent> upgradeItems = new List<UpgradeSelectionItemUIPresenterComponent>();

        void UpdateView() {

            foreach(var item in upgradeItems) {
                item.Selected -= DisplayComponent_Selected;
            }
            upgradeItems.Clear();

            if (Destroying) { //When Destroying this component than just return since we cannot deparent children when destroying
                return;
            }

            if (container != null) {
                for (int i = container.childCount - 1; i >= 0; i--) {
                    Transform child = container.GetChild(i);
                    PoolUtils.ReleaseToPoolOrDestroy(child);
                }
            }


            if(itemPrefab != null) {

                var upgradesSelection = ViewModel?.UpgradesSelection;

                if(upgradesSelection != null) {

                    for (int i = 0; i < upgradesSelection.Length;i++) {


                        GameObject go = PoolUtils.RetriveOrCreateObject(itemPrefab, Vector3.zero, Quaternion.identity, container,
                            x => Instantiator.InstantiatePrefab(x.prefab, x.position, x.rotation, x.parent)
                            ) ;

                        //this is needed since extenject/zenject sets recttransform scale to zero (BUG)
                        RectTransform rTransf = go.GetComponent<RectTransform>();
                        if(rTransf != null) {
                            rTransf.localScale = Vector3.one;
                        }


                        UpgradeSelectionItemUIPresenterComponent displayComponent = go.GetComponent<UpgradeSelectionItemUIPresenterComponent>();
                        if(displayComponent != null) {
                            
                            upgradeItems.Add(displayComponent);
                            displayComponent.SetDisplayData(upgradesSelection[i]);
                            displayComponent.Selected += DisplayComponent_Selected;
                        }
                    }
                }
               
            }


        }

        private void DisplayComponent_Selected(object sender, System.EventArgs e) {
            if(ViewModel != null) {
                ViewModel.SelectedUpgrade = (sender as UpgradeSelectionItemUIPresenterComponent).UpgradeInfo;
            }
        }

        bool Destroying { get; set; }

        private void OnDestroy() {
            Destroying = true;
            ViewModel = null;
        }
    }
}