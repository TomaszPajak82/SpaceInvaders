using System.Collections;
using TMPro;
using UnityEngine;

namespace SoftwareCore.UI.Binders.Components
{
    public class TMPTextUIBinderComponent : TextUIBinderComponent
    {

        public TextMeshProUGUI textMeshPro;

        protected override string OnGetText() {
            return textMeshPro != null ? textMeshPro.text : null;
        }

        protected override void OnSetText(string value) {

            if (textMeshPro != null) {
                textMeshPro.text = value;
            }

        }

    }
}