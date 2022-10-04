using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace SoftwareCore.Extenject.Components
{

    public abstract class DIComponent : MonoBehaviour {

        public bool Injected{get; private set;}

        [Inject]
        private void InnerConstruct() {
            this.Injected = true;
        }

        protected void Awake() {

            // if not injected we will perform manual injection using scene context
            // this will solve problems with runtime addition of components

            if (!Injected) {

                Zenject.DiContainer container = FindObjectOfType<SceneContext>().Container;

                if(container == null) {
                    throw new MissingComponentException($"{nameof(SceneContext)} is not present on scene!");
                }

                container.Inject(this);
            }

            OnAwake();
        }

        protected virtual void OnAwake() {}

    }

}