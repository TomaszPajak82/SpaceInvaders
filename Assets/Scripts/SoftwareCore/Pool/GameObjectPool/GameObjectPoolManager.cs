using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Pool.GameObjectPool
{
    public class GameObjectPoolManager
    {

        //last execution order
        [DefaultExecutionOrder(32000)]
        class GameObjectPoolManagerComponent : MonoBehaviour
        {
            private void OnDestroy() {
                GameObjectPoolManager.poolManager = null;
            }
        }

        static GameObjectPoolManager poolManager;
        public static GameObjectPoolManager Instance {
            get {
                if (poolManager == null) {
                    poolManager = new GameObjectPoolManager();
                }

                return poolManager;
            }
        }


        Dictionary<GameObject, GameObjectPool> Pools { get; set; }

        private GameObjectPoolManager() {

            GameObject managerGO = new GameObject("_" + nameof(GameObjectPoolManager));
            managerGO.hideFlags = HideFlags.HideInHierarchy;
            managerGO.AddComponent<GameObjectPoolManagerComponent>();

            Pools = new Dictionary<GameObject, GameObjectPool>();
        }

        public GameObjectPool RetriveOrCreate(GameObject key) {

            GameObjectPool pool;
            if (!Pools.TryGetValue(key, out pool)) {
                pool = new GameObjectPool();
                Pools.Add(key, pool);
            }

            return pool;
        }

    }
}