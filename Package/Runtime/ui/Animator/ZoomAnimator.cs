using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace com.rainssong.ui
{
    public class ZoomAnimator : MonoBehaviour
    {
        private GameObject _go;
        [HideInInspector]
        protected GameObject go
        {
            get
            {
                if (_go == null)
                    _go = gameObject;
                return _go;
            }
        }
        public float delay = 0f;
        public float duration = 0.3f;
        public float minScale = 0.5f;
        public float maxScale = 1f;


        public ZoomAnimator()
        {

        }


        public void Reset()
        {
            
        }

        public void Animate()
        {
            
        }

        public void ZoomIn(float duration=-1)
        {
            if(duration<0)
                duration = this.duration;

            Complete();
            go.transform.DOScale(new Vector3(maxScale, maxScale), duration).SetDelay(delay).SetEase(Ease.OutBack);
        }

        public void ZoomOut(float duration = -1)
        {
            if (duration < 0)
                duration = this.duration;
            Complete();
            go.transform.DOScale(new Vector3(minScale, minScale), duration).SetDelay(delay).SetEase(Ease.OutBack);
        }

        public void Complete()
        {
            go.transform.DOComplete();
        }
    }
}

