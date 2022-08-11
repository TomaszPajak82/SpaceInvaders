using SoftwareCore.Pool;
using SpaceInvaders.Settings;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using System.Linq;
using System;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    partial class WaveGameplayStateObject
    {
        class PrePlayGameplayStateObjectSession : WaveGameplayStateObjectSession
        {

            GameplaySubStatesData Data { get; set; }

            (Vector3 position, Quaternion orientation)[][] startPositions;

            (Vector3 position, Quaternion orientation)[][] endPositions;

            float ElapsedTime { get; set; }

            public void Initialize(WaveGameplayStateObject waveGameplayStateObject, GameplaySubStatesData data) {

                WaveGameplayStateObject = waveGameplayStateObject;

                ElapsedTime = 0;

                this.IsCompleted = false;

                this.Data = data;



                this.WaveGameplayStateObject.Wave = CreateWaveGroup(this.WaveGameplayStateObject.Prefabs,this.WaveGameplayStateObject.ColumnsCount, this.WaveGameplayStateObject.RowsCount, this.WaveGameplayStateObject.HorizontalSpacing, this.WaveGameplayStateObject.VerticalSpacing);

                this.WaveGameplayStateObject.Wave.RestorePositionsAndOrientationsToInitial();

                if (startPositions == null || endPositions == null) {
                    this.WaveGameplayStateObject.Wave.SavePositionsAndOrientations(ref endPositions);


                    Quaternion enemiesOrientation = Data.EnemiesArea.GetEnemiesOrientation();
                    this.WaveGameplayStateObject.Wave.SavePositionsAndOrientations(ref startPositions);


                    Vector3 shiftPerRowIndex = enemiesOrientation * -Vector3.forward * Data.Settings.IncomingWaveTripDistancePerRow;
                    Vector3 constantShift = enemiesOrientation * -Vector3.forward * Data.Settings.IncomingWaveTripDistance;

                    for (int columnIndex = 0; columnIndex < startPositions.Length; columnIndex++) {
                        for (int rowIndex = 0; rowIndex < startPositions[columnIndex].Length; rowIndex++) {
                            startPositions[columnIndex][rowIndex].position += constantShift + shiftPerRowIndex * (startPositions[columnIndex].Length - rowIndex);
                        }
                    }
                }

                this.WaveGameplayStateObject.Wave.LoadPositionsAndOrientations(startPositions);

                Data.CurrenWaveNumber = Data.CurrenWaveNumber + 1;
            }

            protected override void OnUpdate(float dTime) {
                base.OnUpdate(dTime);

                ElapsedTime += dTime;

                float tripDuration = Data.Settings.IncomingWaveTripDuration;

                if (tripDuration > 0) {
                    float k = Mathf.Clamp01(ElapsedTime / tripDuration);
                    this.WaveGameplayStateObject.Wave.LerpPositionsAndOrientations(startPositions, endPositions, k);
                }

                if (ElapsedTime >= tripDuration) {
                    this.WaveGameplayStateObject.Wave.RestorePositionsAndOrientationsToInitial();
                    IsCompleted = true;
                }
            }

            WaveGroup CreateWaveGroup(GameObject[] prefabs,int columns, int rows, float horizontalDistance, float verticalDistance) {

                GameObject[][] objects = new GameObject[columns][];
                for (int i = 0; i < objects.Length; i++) {
                    objects[i] = new GameObject[rows];
                }

                Quaternion areaOrientation = Data.EnemiesArea.GetEnemiesOrientation();

                Vector3 areaCenter = Data.EnemiesArea.transform.position;

                Vector3 areaForwardDirection = areaOrientation * Vector3.forward;
                Vector3 areaRightDirection = areaOrientation * Vector3.right;

                Vector3 areaCenterBottom = areaCenter - areaForwardDirection * Data.EnemiesArea.verticalExtent;


                float groupWidthExtent = ((float)(columns - 1) / 2) * horizontalDistance;


                List<int> retrived = new List<int>();
                for (int rowIndex = 0; rowIndex < rows; rowIndex++) {

                    Vector3 verticalPosition = areaCenterBottom + (areaForwardDirection * rowIndex * verticalDistance);

                    GameObject prefab = prefabs[rowIndex % prefabs.Length];

                    for (int columnIndex = 0; columnIndex < columns; columnIndex++) {

                        Vector3 positionShift = areaRightDirection * (-groupWidthExtent + columnIndex * horizontalDistance);

                        objects[columnIndex][rowIndex] = PoolUtils.RetriveOrCreateObject(prefab, verticalPosition + positionShift, areaOrientation, null,
                            x => Data.Instantiator.InstantiatePrefab(x.prefab, x.position, x.rotation, x.parent));

                        retrived.Add(objects[columnIndex][rowIndex].GetHashCode());
                    }

                }

                return new WaveGroup(objects);
            }

        }
    }
}