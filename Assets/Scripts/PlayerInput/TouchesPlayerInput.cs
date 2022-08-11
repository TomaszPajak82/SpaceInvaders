using UnityEditor;
using UnityEngine;
using Zenject;

namespace SpaceInvaders.PlayerInput
{
    public class TouchesPlayerInput : IPlayerInput, ITickable
    {
        public float HorizontalInputValue { get; protected set; }

        public bool Fire { get; protected set; }

        public void Tick() {

            Resolution resolution = Screen.currentResolution;

            bool leftSideTouched = false;
            bool rightSideTouched = false;

            Fire = false;

            int touchesCount = Input.touchCount;
            for (int i = 0; i < touchesCount; i++) {

                Touch touch = Input.GetTouch(i);

                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {

                    Vector2 touchPosition = touch.position;
                    float normalizedX = touchPosition.x / resolution.width;

                    if (normalizedX > 0.5) {
                        rightSideTouched = true;
                    } else if (normalizedX < 0.5) {
                        leftSideTouched = true;
                    }

                }
            }


            if (Input.GetKey(KeyCode.D)) {
                rightSideTouched = true;
            }

            if (Input.GetKey(KeyCode.A)) {
                leftSideTouched = true;
            }


            HorizontalInputValue = 0;

            if (leftSideTouched) {
                HorizontalInputValue += -1;
            }

            if (rightSideTouched) {
                HorizontalInputValue += 1;
            }

            //for now autofire
            Fire = true;

            if (Fire == false) {
                Fire = Input.GetKeyDown(KeyCode.Space);
            }


        }
    }
}