using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SoftwareCore.UI.Binders.Components
{
    //this lets us call this method via UnityEvent and such.
    public class MethodTriggeredButtonUIBinderComponent : ButtonUIBinderComponent
    {
        public void Trigger_Clicked() {
            this.RaiseClickedEvent();
        }


    }

}