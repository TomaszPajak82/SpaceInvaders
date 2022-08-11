using System.Collections;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Components
{
    public class PlayerAreaComponent : MonoBehaviour
    {
        public float extents = 5;

        public (Vector3 position, Quaternion orientation) GetInitialPositionAndOrientation() {
            return (this.transform.position, this.transform.rotation);
        }

        public Vector3 CorrectPosition(Vector3 position) {

            Vector3 transfPos = this.transform.position;
            Vector3 right = this.transform.right;

            Vector3 projection = Vector3.Project(position - transfPos, right);

            if (projection.magnitude > extents) {
                projection = projection.normalized * extents;
            }

            return transfPos + projection;
        }

        private void OnDrawGizmos() {

            Gizmos.matrix = this.transform.localToWorldMatrix;

            Vector3 right = this.transform.right;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Vector3.zero, 0.1f);
            Gizmos.DrawLine(-right * extents, right * extents);

            Gizmos.DrawLine(Vector3.zero, Vector3.forward);
        }


    }
}
