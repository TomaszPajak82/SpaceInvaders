using SoftwareCore.Pool;
using SpaceInvaders.Settings;
using SpaceInvaders.States.GameplaySubStates.GameplayStateObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SpaceInvaders.Gameplay.Components;
using Cysharp.Threading.Tasks;
using SpaceInvaders.Health.Components;
using SpaceInvaders.Kill.Components;

namespace SpaceInvaders.States.GameplaySubStates
{
    public class GameplaySubStatesData
    {
        public IGameSettings Settings { get; set; }

        public GameObject PlayerPrefab { get; set; }

        public PlayerAreaComponent PlayerArea { get; set; }

        public EnemiesAreaBoundsComponent EnemiesArea { get; set; }

        //------runtime data--------------

        public IInstantiator Instantiator { get; set; }

        public IGameplayStateObject GameplayStateObject { get; set; }

        public GameObject PlayerGO { get; set; }

        public HashSet<GameObject> Projectiles { get; set; } = new HashSet<GameObject>();

        public Queue<(UniTask<KillComponent> task, GameObject go)> ObjectsInProcessOfKilling { get; set; } = new Queue<(UniTask<KillComponent> task, GameObject go)>();

        public int CurrenWaveNumber { get; set; }

        public int CurrentScore { get; set; }

        public int LivesLeft { get; set; }


        public void Cleanup() {

            if(GameplayStateObject != null) {
                GameplayStateObject.Cleanup();
                GameplayStateObject = null;
            }


            while (ObjectsInProcessOfKilling.TryDequeue(out var result)) {
                PoolUtils.ReleaseToPoolOrDestroy(result.go);
            }

            foreach (var projectile in Projectiles) {
                if (projectile != null) {
                    PoolUtils.ReleaseToPoolOrDestroy(projectile);
                }
            }

            Projectiles.Clear();



            PoolUtils.ReleaseToPoolOrDestroy(PlayerGO);
            PlayerGO = null;

        }
    }
}