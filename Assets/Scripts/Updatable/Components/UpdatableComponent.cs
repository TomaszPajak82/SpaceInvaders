using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Updatable.Components
{

    public abstract class UpdatableComponent : MonoBehaviour
    {
        // Update is called once per frame
        public void DoUpdate(float dTime) {
            OnDoUpdate(dTime);
        }

        protected abstract void OnDoUpdate(float dTime);

        public void DoFixedUpdate(float dTime) {
            OnDoFixedUpdate(dTime);
        }

        protected abstract void OnDoFixedUpdate(float dTime);
    }

}


    


