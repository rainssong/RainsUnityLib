using System.Globalization;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace com.rainssong.ui
{
	public class AlphaAnimator : MonoBehaviour
	{
		[HideInInspector]
		protected GameObject go;
		public float delay=0f;
		public float duration=0.3f;
		public float startAlpha=0f;
		public float endAlpha=1f;

		public AlphaAnimator ()
		{
			
		}

		public void Awake()
		{
			if(go==null)
                go = this.gameObject;

            DOTween.defaultTimeScaleIndependent = true;
		}

		public void OnEnable()
		{
			Reset();
			Animate();
		}


		public void OnDisable()
		{
			Complete();
		}


		public void Reset()
		{
			Component[] comps = GetComponentsInChildren<Component>();  
			for (int index = 0; index < comps.Length; index++)  
			{  
				Component c = comps[index];  
				if (c is Graphic)  
				{  
					// (c as Graphic)  
					// 	.DOFade(startAlpha, 0f);
				}  
			} 
		}

		public void Animate()
		{
//			if(img!=null)
//			img.DOFade(endAlpha,duration).SetDelay(delay);

			Complete();
			Reset();

			Component[] comps = GetComponentsInChildren<Component>();  
			for (int index = 0; index < comps.Length; index++)  
			{  
				Component c = comps[index];  
				if (c is Graphic)  
				{  
					// (c as Graphic)  
					// 	.DOFade(endAlpha, duration)
					// 	.SetDelay(delay);
				}  
			}  
		}

		public void Complete()
		{
			Component[] comps = GetComponentsInChildren<Component>();  
			for (int index = 0; index < comps.Length; index++)  
			{  
				Component c = comps[index];  
				if (c is Graphic)  
				{  
					(c as Graphic)  
						.DOComplete();
				}  
			}  
		}
	}
}

