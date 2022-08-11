using SoftwareCore.Pool;
using SpaceInvaders.Settings;
using System;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.States.GameplaySubStates.GameplayStateObjects
{


    public partial class WaveGameplayStateObject : IGameplayStateObject
    {

        GameplaySubStatesData Data { get; set; }

        WaveGroup Wave { get; set; }

        PrePlayGameplayStateObjectSession CreationSession { get; set; } = new PrePlayGameplayStateObjectSession();
        PlayGameplayStateObjectSession PlaySession { get; set; } = new PlayGameplayStateObjectSession();


        public GameObject[] Prefabs { get; set; }
        public int ColumnsCount { get; set; }
        public int RowsCount { get; set; }
        public float HorizontalSpacing { get; set; }
        public float VerticalSpacing { get; set; }

        public float MovementSpeed { get; set; }

        public float ColumnFiringPerSecondPropability { get; set; }

        public void Initialize(GameplaySubStatesData data) {
            this.Data = data;
        }


        public IGameplayStateObjectSession StartPrePlaySession() {
            CreationSession.Initialize(this,this.Data);
            return CreationSession;
        }


        public IGameplayStateObjectSession StartPlaySession() {
            PlaySession.Initialize(this,this.Data);
            return PlaySession;
        }

        public void Cleanup() {
            this.Wave.ForEach(go => PoolUtils.ReleaseToPoolOrDestroy(go));
        }

    }
}