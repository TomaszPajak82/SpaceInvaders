using SoftwareCore.Pool;
using SpaceInvaders.EntityRoot.Components;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SoftwareCore.Factories
{
    public class GameObjectFactoryWithPooling<T> : IFactory<(GameObject prefab, Vector3 position, Quaternion orientation, Transform parent), T>
    {
        IInstantiator Instantiator { get; set; }

        public GameObjectFactoryWithPooling(IInstantiator instantiator) {
            Instantiator = instantiator;
        }

        public T Create((GameObject prefab, Vector3 position, Quaternion orientation, Transform parent) param) {

            GameObject go = PoolUtils.RetriveOrCreateObject(param.prefab, param.position, param.orientation, param.parent,
            x => Instantiator.InstantiatePrefab(x.prefab, x.position, x.rotation, x.parent));

            T erc = go.GetComponent<T>();
            if (erc == null) {
                throw new MissingComponentException(nameof(T));
            }

            return erc;
        }
    }
}