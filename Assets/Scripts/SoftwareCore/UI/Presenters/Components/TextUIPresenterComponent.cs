using SoftwareCore.UI.Binders.Components;
using System.Collections;
using UnityEngine;

namespace SoftwareCore.UI.Presenters.Components
{
    public class TextUIPresenterComponent : MonoBehaviour
    {

        public TextUIBinderComponent textBinder;

        public string GetText() {
            if (textBinder != null) {
                return textBinder.GetText();
            } else {
                return string.Empty;
            }
        }


        public void SetText(string value) {
            if (textBinder != null) {
                textBinder.SetText(value);
            }
        }
    }
}
