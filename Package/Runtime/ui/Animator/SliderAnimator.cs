using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace com.rainssong.ui
{
	public class SliderAnimator : MonoBehaviour
	{
		[HideInInspector]
		public Slider slider;

		public float delay=0f;
		public float duration=0.4f;

		public SliderAnimator ()
		{
			
		}

		public void Awake()
		{
			if(slider==null)
				slider=this.GetComponent<Slider>();
		}

		public void to(float percent,float duration=-1f)
		{
			if(duration<0)
				duration=this.duration;

			DOTween.To(()=> slider.value, x=> slider.value = x,percent,duration).SetDelay(delay);
		}
	}
}

