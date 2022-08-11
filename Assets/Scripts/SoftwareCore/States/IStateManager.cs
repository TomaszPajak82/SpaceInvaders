using UnityEditor;
using UnityEngine;

namespace SoftwareCore.States
{
    public interface IStateManager
    {

        public IState State { get; set; }

    }

}