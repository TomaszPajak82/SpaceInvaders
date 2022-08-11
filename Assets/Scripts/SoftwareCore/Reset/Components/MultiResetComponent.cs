using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoftwareCore.Reset.Components
{
    public class MultiResetComponent : ResetComponent
    {
        public ResetComponent[] resetComponents;

        public bool findResetComponents = true;

        public Transform[] searchRoots;

        private void Awake() {

            if (findResetComponents) {

                HashSet<ResetComponent> unique = new HashSet<ResetComponent>();
                unique.UnionWith(resetComponents);

                List<ResetComponent> tmpList = new List<ResetComponent>();
                foreach(Transform transf in searchRoots) {
                    if(transf != null) {
                        transf.GetComponentsInChildren<ResetComponent>(true, tmpList);
                        unique.UnionWith(tmpList);
                    }
                }

                unique.Remove(this);
                resetComponents = unique.ToArray();
                
            }
        }

        protected override void OnReset() {

            foreach(var item in resetComponents) {

                if(item != null) {
                    item.Reset();
                }
            }

        }

    }
}