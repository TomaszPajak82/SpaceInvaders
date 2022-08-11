using SoftwareCore.Reset.Components;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Pool.GameObjectPool
{
    public class GameObjectPool
    {

        class GameObjectPoolClientComponen : PoolClientComponent
        {
            public GameObjectPool Pool { get; set; }

            ResetComponent[] ResetComponents { get; set; }


            bool isReleased = false;
            public override bool IsReleased { get => isReleased;
                protected set => isReleased = value;
            }

            protected override void OnRelease() {
                Pool.Release(this.gameObject);
            }

            public void SetIsReleased(bool value) {
                isReleased = value;
                if(isReleased == false) {
                    Reset();
                }
            }

            private void Reset() {
                foreach(ResetComponent item in ResetComponents) {
                    if(item != null) {
                        item.Reset();
                    }
                }
            }

            private void Awake() {
                ResetComponents = this.gameObject.GetComponents<ResetComponent>();
            }
        }

        Stack<GameObject> PooledObjects { get; set; }

        public GameObjectPool() {
            PooledObjects = new Stack<GameObject>();
        }

        public void Register(GameObject go) {
            GameObjectPoolClientComponen poolClientComponent = go.AddComponent<GameObjectPoolClientComponen>();
            poolClientComponent.Pool = this;
        }

        public void Release(GameObject go) {
            go.SetActive(false);
            go.transform.parent = null;

            GameObjectPoolClientComponen gopcc = go.GetComponent<GameObjectPoolClientComponen>();
            if (gopcc != null) {
                gopcc.SetIsReleased(true);
            }

            PooledObjects.Push(go);
        }

        public bool TryGet(out GameObject go, bool autoActivate = true) {

            bool retrived = false;
            while (retrived = PooledObjects.TryPop(out go)) {
                if (go != null) { //might be already destroyed
                    break;
                } else { // if is destroyed than continue searching
                    retrived = false;
                }
            }

            if (retrived) {
                GameObjectPoolClientComponen gopcc = go.GetComponent<GameObjectPoolClientComponen>();
                if (gopcc != null) {
                    gopcc.SetIsReleased(false);
                }
            }

            if (retrived && autoActivate) {
                go.SetActive(true);
            }

            return retrived;
        }
    }
}