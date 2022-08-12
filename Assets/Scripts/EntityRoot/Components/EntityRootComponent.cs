using System.Collections;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.EntityRoot.Components
{
    //object on top of every autonomous GameObject hierarchy
    public class EntityRootComponent : MonoBehaviour
    {

        public class Factory: PlaceholderFactory<(GameObject prefab, Vector3 position, Quaternion orientation, Transform parent), EntityRootComponent>
        {
        }

    }
}
