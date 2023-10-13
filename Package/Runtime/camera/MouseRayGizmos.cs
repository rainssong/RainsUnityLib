using System.Security.AccessControl;
using System.Collections;
using UnityEngine;

namespace com.rainssong.camera
{

    public class MouseRayGizmos : MonoBehaviour
    {


        void OnDrawGizmos ()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
                Gizmos.DrawLine(ray.origin, hit.point);
            else
                Gizmos.DrawLine(ray.origin, ray.direction*10);
        }
    }
}