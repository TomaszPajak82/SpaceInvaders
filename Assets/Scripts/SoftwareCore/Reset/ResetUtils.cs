using SoftwareCore.Reset.Components;
using System;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Reset
{


    public static class ResetUtils
    {
        public static bool SubscribeToFirstResetingInParents(Transform transf, EventHandler eventHandler) {

            if (transf == null) {
                return false;
            }

            ResetComponent resetComponent = transf.GetComponentInParent<ResetComponent>(true);
            if (resetComponent != null) {
                resetComponent.Reseting += eventHandler;
                return true;
            }

            return false;
        }

        public static bool UnsubscribeFromFirstResetingInParents(Transform transf, EventHandler eventHandler) {

            if(transf == null) {
                return false;
            }

            ResetComponent resetComponent = transf.GetComponentInParent<ResetComponent>(true);
            if (resetComponent != null) {
                resetComponent.Reseting -= eventHandler;
                return true;
            }

            return false;
        }

    }

}