using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Components
{

    public class EnemiesAreaBoundsComponent : MonoBehaviour
    {
        public float horizontalExtent = 5;
        public float verticalExtent = 5;

        public Quaternion GetEnemiesOrientation() {
            return this.transform.rotation;
        }

        //return true when position corrected
        public (Vector3 position, bool isHorrizontalyCorrected, bool isVerticalyCorrected) CorrectPosition(Vector3 position) {



            Vector3 transfPos = this.transform.position;

            //-------------horizontal check
            bool isHorrizontalyCorrected = false;
            Vector3 right = this.transform.right;

            Vector3 projectionHorizontal = Vector3.Project(position - transfPos, right);

            if (projectionHorizontal.magnitude > horizontalExtent) {
                projectionHorizontal = projectionHorizontal.normalized * (horizontalExtent);
                isHorrizontalyCorrected = true;
            }

            //------------vertical check
            bool isVerticalyCorrected = false;
            Vector3 forward = this.transform.forward;

            Vector3 projectionVertical = Vector3.Project(position - transfPos, forward);

            if (projectionVertical.magnitude > verticalExtent) {
                projectionVertical = projectionVertical.normalized * (verticalExtent);
                isVerticalyCorrected = true;
            }



            return (projectionHorizontal + projectionVertical, isHorrizontalyCorrected, isVerticalyCorrected);

        }


        private void OnDrawGizmos() {

            //Vector3 position = this.transform.position;

            Gizmos.matrix = this.transform.localToWorldMatrix;

            Vector3 leftTop = new Vector3(-horizontalExtent, 0, verticalExtent);
            Vector3 rightTop = new Vector3(horizontalExtent, 0, verticalExtent);
            Vector3 rightBottom = new Vector3(horizontalExtent, 0, -verticalExtent);
            Vector3 leftBottom = new Vector3(-horizontalExtent, 0, -verticalExtent);


            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(rightBottom, leftBottom);
            Gizmos.DrawLine(leftBottom, leftTop);

            Gizmos.DrawLine(Vector3.zero, Vector3.forward);

        }

    }

}