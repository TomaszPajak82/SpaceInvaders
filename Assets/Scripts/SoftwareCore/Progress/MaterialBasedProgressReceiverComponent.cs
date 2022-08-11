using System.Collections;
using UnityEngine;

namespace SoftwareCore.Progress
{
    public class MaterialBasedProgressReceiverComponent : ProgressReceiverComponent
    {

        public Renderer target;

        public Transform rendererSearchRoot;

        public string propertyName = "_Progress";


        MaterialPropertyBlock materialPropertyBlock;

        private void Awake() {
            if(rendererSearchRoot != null && target == null) {
                target = rendererSearchRoot.GetComponentInChildren<Renderer>();
            }
        }

        protected override void OnUpdateProgress(float progress) {
            
            if(target != null) {

                if(materialPropertyBlock == null) {
                    materialPropertyBlock = new MaterialPropertyBlock();
                }

                materialPropertyBlock.SetFloat(propertyName, progress);

                target.SetPropertyBlock(materialPropertyBlock);
                
            }

        }

    }
}