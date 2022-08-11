using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SoftwareCore.UI.Binders.Components.UnityUI
{

    [RequireComponent(typeof(Image))]
    public class ImageUnityUIImageUIBinderComponent : ImageUIBinderComponent
    {
        Image image;
        Image Image {
            get { 
                if(image == null) {
                    image = this.GetComponent<Image>();
                }
                return image;
            }
            set { 
                image = value; 
            }
        }



        protected override Sprite OnGetImage() {
            if (Image != null) {
                return Image.sprite;
            } else {
                return null;
            }
        }

        protected override void OnSetImage(Sprite value) {
            if (Image != null) {
                Image.sprite = value;
            }
        }

        
    }
}
