using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.Tasks.Components
{
    public abstract class UniTaskComponent : MonoBehaviour {

        public abstract bool IsRunning {
            get;
        }

        public UniTask<object> StartTask() {
            return OnStartTask();
        }

        protected abstract UniTask<object> OnStartTask();
    }
}