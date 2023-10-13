using System;
using System.Collections;
using System.Collections.Generic;
using com.rainssong.ui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace com.rainssong.ui
{
	public class Alert : UIBase
	{
		public Text contextTxt;
		public Button OKBtn;
		public Action onOK;

		override protected void Awake ()
		{
			OKBtn.onClick.AddListener (Hide);
		}

		public string context
		{
			get { return contextTxt.text; }
			set { contextTxt.text = value; }
		}

		// Update is called once per frame
		void Update ()
		{

		}

		public void Show (string c, bool showBtn = true, Action cb = null)
		{
			if (c != null)
				context = c.Replace ("\\n", "\n"); //在编辑器模式下无法输入换行符
			OKBtn.gameObject.SetActive (showBtn);
			base.Show (true);

			OKBtn.onClick.RemoveAllListeners ();
			if (cb != null)
				OKBtn.onClick.AddListener (new UnityAction (cb));
		}

		public void Show (string c)
		{
			Show (c, true, null);
		}

	}
}