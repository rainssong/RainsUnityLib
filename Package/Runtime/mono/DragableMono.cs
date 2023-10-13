using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.rainssong.mono
{

    public class DragableMono : MonoBehaviour
    {
        public Vector3 mousePos;
        public bool dragable = true;
        public Action OnStartDrag;
        public Action OnStopDrag;
        public bool lockX;
        public bool lockY;
        public bool lockZ;

        

        private void OnMouseUp ()
        {
            if (!dragable)
                return;

            OnStopDrag?.Invoke ();
        }



        //通过鼠标移动物体Cube
        IEnumerator OnMouseDown ()
        {
            if (!dragable)
                yield break;
            OnStartDrag?.Invoke ();
            // transform.Translate(0f, 0, -0.1f);
            Vector3 screenSpace = Camera.main.WorldToScreenPoint (transform.position); //三维物体坐标转屏幕坐标
            //将鼠标屏幕坐标转为三维坐标，再计算物体位置与鼠标之间的距离
            var offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            while (Input.GetMouseButton (0))
            {
                Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                var curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;
                if(lockY)
                    curPosition.y = transform.position.y;
                if(lockX)
                    curPosition.x = transform.position.x;
                if(lockZ)
                    curPosition.z = transform.position.z;
                transform.position = curPosition;
                yield return new WaitForFixedUpdate ();
            }

        }

    }
}