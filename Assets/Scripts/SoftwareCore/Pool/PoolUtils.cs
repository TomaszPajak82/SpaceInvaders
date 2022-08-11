using SoftwareCore.Pool.GameObjectPool;
using System;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.Pool
{

    public static class PoolUtils
    {

        public static GameObject RetriveOrCreateObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null,Func<(GameObject prefab,Vector3 position,Quaternion rotation,Transform parent),GameObject> customInstantiation = null) {

            GameObjectPool.GameObjectPool pool = GameObjectPoolManager.Instance.RetriveOrCreate(prefab);

            GameObject go;
            if (pool.TryGet(out go, false)) {
                go.transform.SetPositionAndRotation(position, rotation);
                go.transform.parent = parent;
                go.SetActive(true);
            } else {
                if (customInstantiation != null) {
                    go = customInstantiation(new(prefab, position, rotation, parent));
                } else {
                    go = GameObject.Instantiate(prefab, position, rotation, parent);
                }
                pool.Register(go);
            }

            return go;
        }

        public static void ReleaseToPoolOrDestroy(GameObject go) {

            if (go != null) {

                PoolClientComponent poolClient = go.GetComponent<PoolClientComponent>();
                if (poolClient != null) {
                    poolClient.Release();
                } else {
                    GameObject.Destroy(go);
                }

            }
        }

        public static void ReleaseToPoolOrDestroy(Transform transf) {

            if (transf != null) {
                ReleaseToPoolOrDestroy(transf.gameObject);
            }
        }

    }
}