using SpaceInvaders.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{
    partial class WaveGameplayStateObject
    {
        public class WaveGroup
        {
            public GameObject[][] Objects { get; set; }

            (Vector3 position, Quaternion orientation)[][] InitialPositionAndOrientation { get; set; }

            Dictionary<GameObject, (int column, int row)> ObjectsRowAndColumnDict { get; set; }

            int CurrentBeginColumnIndex { get; set; }

            int CurrentEndColumnIndex { get; set; }


            public WaveGroup(GameObject[][] waveObjectsInInitialPosition) {

                Objects = waveObjectsInInitialPosition;
                
                InitialPositionAndOrientation = Array.ConvertAll(Objects, x => new (Vector3 position, Quaternion orientation)[x.Length]);

                ObjectsRowAndColumnDict = new Dictionary<GameObject, (int column, int row)>();


                for (int waveColumnIndex = 0; waveColumnIndex < Objects.Length; waveColumnIndex++) {

                    GameObject[] objectsInColumn = Objects[waveColumnIndex];

                    for (int waveRowIndex = 0; waveRowIndex < objectsInColumn.Length; waveRowIndex++) {
                        GameObject go = objectsInColumn[waveRowIndex];

                        ObjectsRowAndColumnDict.Add(go, new(waveColumnIndex, waveRowIndex));

                    }

                }


                StorePositionsAndOrientationsAsInitial();

                Reset();
            }

            public void Reset() {

                CurrentBeginColumnIndex = 0;
                CurrentEndColumnIndex = Objects.Length - 1;

                RestorePositionsAndOrientationsToInitial();

            }

            public void RestorePositionsAndOrientationsToInitial() {

                for (int waveColumnIndex = 0; waveColumnIndex < Objects.Length; waveColumnIndex++) {

                    GameObject[] objectsInColumn = Objects[waveColumnIndex];

                    for (int waveRowIndex = 0; waveRowIndex < objectsInColumn.Length; waveRowIndex++) {

                        Transform goTransf = objectsInColumn[waveRowIndex].transform;
                        var posAndOrient = InitialPositionAndOrientation[waveColumnIndex][waveRowIndex];
                        goTransf.SetPositionAndRotation(posAndOrient.position, posAndOrient.orientation);

                    }

                }
            }

            void StorePositionsAndOrientationsAsInitial() {
                for (int waveColumnIndex = 0; waveColumnIndex < Objects.Length; waveColumnIndex++) {

                    GameObject[] objectsInColumn = Objects[waveColumnIndex];

                    for (int waveRowIndex = 0; waveRowIndex < objectsInColumn.Length; waveRowIndex++) {

                        Transform goTransf = objectsInColumn[waveRowIndex].transform;

                        InitialPositionAndOrientation[waveColumnIndex][waveRowIndex] = new(goTransf.position, goTransf.rotation);

                    }

                }
            }

            public void SavePositionsAndOrientations(ref (Vector3 position, Quaternion orientation)[][] snapshotPositionAndOrientation) {

                if (snapshotPositionAndOrientation == null) {
                    snapshotPositionAndOrientation = Array.ConvertAll(Objects, arr => new (Vector3 position, Quaternion orientation)[arr.Length]);
                }

                for (int i = 0; i < Objects.Length; i++) {
                    for (int j = 0; j < Objects[i].Length; j++) {
                        snapshotPositionAndOrientation[i][j] = (Objects[i][j].transform.position, Objects[i][j].transform.rotation);
                    }
                }

            }

            public void LoadPositionsAndOrientations((Vector3 position, Quaternion orientation)[][] snapshotPositionAndOrientation) {

                for (int i = 0; i < Objects.Length; i++) {
                    for (int j = 0; j < Objects[i].Length; j++) {
                        var posOrient = (snapshotPositionAndOrientation[i][j].position, snapshotPositionAndOrientation[i][j].orientation);
                        Objects[i][j].transform.SetPositionAndRotation(posOrient.position, posOrient.orientation);
                    }
                }

            }

            public void LerpPositionsAndOrientations((Vector3 position, Quaternion orientation)[][] startPositionAndOrientation, (Vector3 position, Quaternion orientation)[][] endPositionAndOrientation, float t) {

                for (int i = 0; i < Objects.Length; i++) {
                    for (int j = 0; j < Objects[i].Length; j++) {

                        Vector3 newPos = Vector3.LerpUnclamped(startPositionAndOrientation[i][j].position, endPositionAndOrientation[i][j].position, t);
                        Quaternion newOrient = Quaternion.LerpUnclamped(startPositionAndOrientation[i][j].orientation, endPositionAndOrientation[i][j].orientation, t);

                        Objects[i][j].transform.SetPositionAndRotation(newPos, newOrient);
                    }
                }

            }

            public void ForEach(Action<GameObject> action) {

                if (action == null) {
                    return;
                }

                for (int waveColumnIndex = 0; waveColumnIndex < Objects.Length; waveColumnIndex++) {

                    GameObject[] objectsInColumn = Objects[waveColumnIndex];

                    for (int waveRowIndex = 0; waveRowIndex < objectsInColumn.Length; waveRowIndex++) {
                        action(objectsInColumn[waveRowIndex]);
                    }
                }

            }

            public void ForEachColumn(Action<GameObject[]> action) {

                if (action == null) {
                    return;
                }

                for (int waveColumnIndex = 0; waveColumnIndex < Objects.Length; waveColumnIndex++) {
                    GameObject[] objectsInColumn = Objects[waveColumnIndex];
                    action(objectsInColumn);
                }

            }
        }
    }
}