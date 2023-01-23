using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.rainssong.ui
{
    /// <summary>
    /// 可以Modal形式展示
    /// </summary>
    public class UIBase : UIBehaviour
    {
        private Image bg;

        public virtual void Show (Boolean isModal = false)
        {
            this.gameObject.SetActive (true);
            //this.gameObject.active=true;
            if (isModal)
            {
                if (bg == null)
                {
                    GameObject ob = new GameObject ("bg");
                    bg = ob.AddComponent<Image> ();
                    bg.color = new Color (0f, 0f, 0f, 0.49f);
                    //bg.rectTransform.sizeDelta = new Vector2(2000, 1100);
                    ob.transform.SetParent (this.transform.parent.transform, false);
                    ob.transform.SetSiblingIndex (this.gameObject.transform.GetSiblingIndex ());
                    bg.rectTransform.anchorMin = new Vector2 (0, 0);
                    bg.rectTransform.anchorMax = new Vector2 (1, 1);
                    bg.rectTransform.offsetMin = new Vector2 (0, 0);
                    bg.rectTransform.offsetMax = new Vector2 (1, 1);
                }
                bg.gameObject.SetActive (true);
            }
            else
            {
                if (bg != null)
                    bg.gameObject.SetActive (false);
            }

            // transform.localScale = new Vector3(0.1f, 0.1f);
            // transform.DOScale(1f, duration).SetEase(Ease.OutBack);
            // transform.DOPlay();
        }

        public bool visible
        {
            get
            {
                if (gameObject != null) return gameObject.activeSelf;
                else return false;
            }
            set { if (gameObject != null) gameObject.SetActive (value); }
        }

        public virtual void Hide ()
        {
            this.gameObject.SetActive (false);
            if (bg != null)
                bg.gameObject.SetActive (false);
        }

        public virtual void Init ()
        {

        }

        protected override void Awake ()
        {
            //初始化
            Init ();
            //重置
            // Reset();
        }
    }
}