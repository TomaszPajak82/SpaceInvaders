using SoftwareCore.Reset;
using System;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Health.Components
{
    public class HealthComponent : MonoBehaviour
    {

        public int baseHealth = 1;

        public int Health { 
            get; 
            set; 
        }

        public bool IsAlive {
            get => Health > 0; 
        }

        [SerializeField]
        bool autoresetOnReset = true;

        private void Awake() {

            Health = baseHealth;

            if (autoresetOnReset) {
                ResetUtils.SubscribeToFirstResetingInParents(this.transform,
                    (x, y) => this.Health = this.baseHealth
                    ); 
            }
        }

    }
}
