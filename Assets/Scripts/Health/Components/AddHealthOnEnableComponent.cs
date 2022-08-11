using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Health.Components
{
    public class AddHealthOnEnableComponent : MonoBehaviour
    {
        public HealthComponent target;
        public int value = 1;

        private void OnEnable() {
            if(target != null) {
                target.Health += value;
            }
        }
    }

}