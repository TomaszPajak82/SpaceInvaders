using SpaceInvaders.Updatable.Components;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.Projectiles.Components
{
    //projectile shot from the units
    public class ProjectileComponent : UpdatableComponent
    {

        public float baseProjectileSpeed = 5;

        public float projectileSpeedMultiplier = 1;

        [Header("Guided")]
        public bool isGuided = false;

        public Transform target;

        public float rotationSpeedDeg = 20;

        public float maxGuidingAngle = 180;

        protected override void OnDoUpdate(float dTime) {

            this.transform.Translate(new Vector3(0, 0, baseProjectileSpeed * projectileSpeedMultiplier) * dTime, Space.Self);

            if (isGuided && target != null) {
                Vector3 pos = this.transform.position;

                Vector3 toTargetVect = target.position - pos;

                float angle = Vector3.SignedAngle(this.transform.forward, toTargetVect, Vector3.up);

                if (Mathf.Abs(angle) <= maxGuidingAngle) {

                    float deltaAngle = rotationSpeedDeg * dTime;

                    if (Mathf.Abs(angle) < deltaAngle) {
                        deltaAngle = angle;
                    }

                    deltaAngle *= Mathf.Sign(angle);

                    this.transform.Rotate(Vector3.up, deltaAngle, Space.Self);
                }
            }

        }

        protected override void OnDoFixedUpdate(float dTime) {
  
        }

    }
}
