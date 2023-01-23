using System.Collections;
using UnityEngine;

namespace com.rainssong.camera
{

    public class CameraView : MonoBehaviour
    {

        // private Camera theCamera;

        //距离摄像机8.5米 用黄色表示
        public float upperDistance = 8.5f;
        //距离摄像机12米 用红色表示
        public float lowerDistance = 12.0f;


        public Camera theCamera
        {
            get
            {
                return Camera.main;
            }
        }

        public Transform tx
        {
            get => theCamera.transform;
        }

        void OnDrawGizmos ()
        {
            FindUpperCorners ();
            FindLowerCorners ();
        }

        void FindUpperCorners ()
        {
            Vector3[] corners = GetCorners (upperDistance);

            // for debugging
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine (corners[0], corners[1]); // UpperLeft -> UpperRight
            Gizmos.DrawLine (corners[1], corners[3]); // UpperRight -> LowerRight
            Gizmos.DrawLine (corners[3], corners[2]); // LowerRight -> LowerLeft
            Gizmos.DrawLine (corners[2], corners[0]); // LowerLeft -> UpperLeft
        }

        void FindLowerCorners ()
        {
            Vector3[] corners = GetCorners (lowerDistance);
            Gizmos.color = Color.red;
            // for debugging
            Gizmos.DrawLine (corners[0], corners[1]);
            Gizmos.DrawLine (corners[1], corners[3]);
            Gizmos.DrawLine (corners[3], corners[2]);
            Gizmos.DrawLine (corners[2], corners[0]);
        }

        Vector3[] GetCorners (float distance)
        {
            Vector3[] corners = new Vector3[4];

            float halfFOV = (theCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
            float aspect = theCamera.aspect;

            float height = distance * Mathf.Tan (halfFOV);
            float width = height * aspect;

            // UpperLeft
            corners[0] = tx.position - (tx.right * width);
            corners[0] += tx.up * height;
            corners[0] += tx.forward * distance;

            // UpperRight
            corners[1] = tx.position + (tx.right * width);
            corners[1] += tx.up * height;
            corners[1] += tx.forward * distance;

            // LowerLeft
            corners[2] = tx.position - (tx.right * width);
            corners[2] -= tx.up * height;
            corners[2] += tx.forward * distance;

            // LowerRight
            corners[3] = tx.position + (tx.right * width);
            corners[3] -= tx.up * height;
            corners[3] += tx.forward * distance;

            return corners;
        }
    }
}