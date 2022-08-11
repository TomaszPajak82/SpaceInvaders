using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Kill.Components
{
    //this should be called when unit dies
    public abstract class KillComponent : MonoBehaviour
    {

        public bool IsKilling() {
            return OnIsKilling();
        }

        protected abstract bool OnIsKilling();

        public UniTask<KillComponent> Kill() {
            return OnKill();
        }

        protected abstract UniTask<KillComponent> OnKill();
    }
}