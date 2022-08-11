using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.States
{

    //resize camera to contain renderers
    [RequireComponent(typeof(Camera))]
    public class CameraAutoSizeComponent : MonoBehaviour
    {
        public Renderer observedRenderer;

        Camera targetCamera;

        public float resolutionW;
        public float resolutionH;

        private void Awake() {
            targetCamera = this.GetComponent<Camera>();
        }

        void Update() {

            if (targetCamera != null && observedRenderer != null) {

                Resolution resolution = Screen.currentResolution;
                resolutionW = resolution.width;
                resolutionH = resolution.height;

                float screenRatio = (float)resolution.width / resolution.height;
                Vector3 rendererBoundsSize = observedRenderer.bounds.size;

                float rendererRatio = rendererBoundsSize.x / rendererBoundsSize.z;

                if (screenRatio >= rendererRatio) {
                    targetCamera.orthographicSize = rendererBoundsSize.z / 2;
                } else {
                    float differenceinSizeRatio = rendererRatio / screenRatio;
                    targetCamera.orthographicSize = rendererBoundsSize.z / 2 * differenceinSizeRatio;
                }
            }
        }
    }
}