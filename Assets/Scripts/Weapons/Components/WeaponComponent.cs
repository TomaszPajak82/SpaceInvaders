using SoftwareCore.Audio;
using SoftwareCore.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.Weapons.Components
{
    public class WeaponComponent : MonoBehaviour
    {

        public GameObject projectilePrefab;

        public Transform projectileSpawnTransform;

        public AudioClipInfo firingSound;

        IAudioManager AudioManager { get; set; }

        IInstantiator Instantiator { get; set; }

        [Inject]
        void Construct(IInstantiator instantiator, IAudioManager audioManager) {
            Instantiator = instantiator;
            AudioManager = audioManager;
        }


        public void Fire(IList<GameObject> result) {

            if (this.isActiveAndEnabled && projectilePrefab != null) {

                GameObject projectile = PoolUtils.RetriveOrCreateObject(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation, null,
                    (x) => Instantiator.InstantiatePrefab(x.prefab, x.position, x.rotation, x.parent)
                    );

                if (result != null) {
                    result.Add(projectile);
                }

                AudioManager.PlayOneShot(firingSound);
            }
        }

    }
}