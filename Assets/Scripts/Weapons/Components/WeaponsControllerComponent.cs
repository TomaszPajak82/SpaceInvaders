using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace SpaceInvaders.Weapons.Components
{
    public class WeaponsControllerComponent : MonoBehaviour
    {

        public WeaponComponent[] weapons;

        public bool searchForWeapons = false;
        public Transform[] searchForWeaponsRoots;


        private void Awake() {

            if (searchForWeapons) {
                List<WeaponComponent> foundWeapons = new List<WeaponComponent>();
                foreach (var root in searchForWeaponsRoots) {
                    root.GetComponentsInChildren<WeaponComponent>(true, foundWeapons);
                }

                foundWeapons.AddRange(weapons);
                weapons = foundWeapons.Distinct().ToArray();

            }
        }

        public void Fire(IList<GameObject> result) {

            foreach (var weapon in weapons) {
                if (weapon != null) {
                   weapon.Fire(result);
                }
            }
        }
    }

}