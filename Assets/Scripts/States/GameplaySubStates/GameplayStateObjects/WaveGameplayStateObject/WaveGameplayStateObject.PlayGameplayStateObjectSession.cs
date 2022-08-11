using SpaceInvaders.Settings;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SpaceInvaders.Projectiles.Components;
using SpaceInvaders.Weapons.Components;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    partial class WaveGameplayStateObject
    {
        class PlayGameplayStateObjectSession : WaveGameplayStateObjectSession
        {

            GameplaySubStatesData Data { get; set; }

            Vector3 CurrentDirection { get; set; }

            public void Initialize(WaveGameplayStateObject waveGameplayStateObject, GameplaySubStatesData data) {

                WaveGameplayStateObject = waveGameplayStateObject;

                this.IsCompleted = false;

                this.Data = data;

                CurrentDirection = Vector3.right;
            }

            protected override void OnUpdate(float dTime) {
                base.OnUpdate(dTime);
                UpdateWave(dTime);

                IsCompleted = CheckWaveIsEmpty();
            }

            bool CheckWaveIsEmpty() {

                var units = WaveGameplayStateObject.Wave.Objects;

                bool enemiesPresent = false;
                for (int i = 0; i < units.Length; i++) {
                    for (int j = 0; j < units[i].Length; j++) {
                        if (units[i][j].activeSelf) {
                            enemiesPresent = true;
                            break;
                        }
                    }
                }

                return !enemiesPresent;
                
            }

            List<GameObject> tmpGameObjectslist = new List<GameObject>();
            void UpdateWave(float dTime) {

                UpdatePositions(dTime);

                UpdateFiring(dTime);
            }

            void UpdatePositions(float dTime) {

                float enemySpeed = this.WaveGameplayStateObject.MovementSpeed;
                var enemiesArea = Data.EnemiesArea;

                Quaternion enemiesOrientation = enemiesArea.GetEnemiesOrientation();
                Vector3 directionWorld = enemiesOrientation * CurrentDirection;
                Vector3 positionShiftWorld = directionWorld * enemySpeed * dTime;

                bool sidesReached = false;
                this.WaveGameplayStateObject.Wave.ForEach(x => {

                    if (!x.activeSelf) {
                        return;
                    }

                    var correctedPosition = enemiesArea.CorrectPosition(x.transform.position + positionShiftWorld);
                    if (correctedPosition.isHorrizontalyCorrected) {
                        sidesReached = true;
                    }

                    x.transform.position = correctedPosition.position;
                });

                if (sidesReached) { //swap direction
                    CurrentDirection = -CurrentDirection;
                }
            }

            void UpdateFiring(float dTime) {

                float firingPropabilityPerSecond = this.WaveGameplayStateObject.ColumnFiringPerSecondPropability;

                tmpGameObjectslist.Clear();
                this.WaveGameplayStateObject.Wave.ForEachColumn(column => {

                    for (int i = column.Length - 1; i >= 0; i--) {
                        if (column[i].activeSelf) { //first active
                            if (UnityEngine.Random.value <= firingPropabilityPerSecond * dTime) {

                                WeaponsControllerComponent weaponController = column[i].GetComponent<WeaponsControllerComponent>();
                                if (weaponController != null) {
                                    weaponController.Fire(tmpGameObjectslist);
                                }

                            }
                            break;
                        }
                    }

                });

                tmpGameObjectslist.ForEach(x => {
                    var projectileComponent = x.GetComponent<ProjectileComponent>();
                    if (projectileComponent != null && projectileComponent.isGuided) {
                        projectileComponent.target = Data.PlayerGO.transform;
                    }

                    }) ;

                tmpGameObjectslist.ForEach(x => Data.Projectiles.Add(x));
            }

        }
    }
}