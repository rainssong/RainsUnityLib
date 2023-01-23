using System;
using System.Collections;
using System.Collections.Generic;
using com.rainssong.io;
using com.rainssong.utils;
using SimpleJSON;
using UnityEngine;

namespace com.rainssong.io
{
    /// <summary>
    /// FIXME:禁止New出来
    /// TODO:打印失败
    /// </summary>
    [RequireComponent(typeof(RequestManager))]
    public class AssetLoadManager : SingletonMono<AssetLoadManager>
	{
		public Action onComplete;
		

		[SerializeField]
		private RequestManager _requestManager;
		public RequestManager requestManager
		{
			get
			{
				_requestManager=this.GetAddComponent<RequestManager>();
				_requestManager.printLog = this.printLog;
                return _requestManager;
			}
		}

		private string configURL;
		private string _configStr;
		private JSONNode _configData;

        private string _baseURL;

		
		public bool _printLog = false;
		public bool printLog
		{
			get
			{
				return _printLog;
			}
			set
			{
				_printLog = value;
				requestManager.printLog = value;
			}
		}

		/// <summary>
		/// 绝对基础路径
		/// </summary>
		/// <value></value>
        public string baseURL { 
			get=>_baseURL; 
			set{
				_baseURL = value;
				requestManager.baseUrl=value;
			} 
		}

		public string GetFullPath(string path)
		{
			Uri uri;
			if(!path.Contains(":/"))
                uri=new Uri(new Uri(baseURL),path);
            else
                uri=new Uri(path);
			return uri.AbsolutePath;
		}

        public void loadConfigFile (string url)
		{
			Debug.Log ("loadConfigFile:" + url);
			configURL = url;
			requestManager.Add (url);
			requestManager.onLoadComplete = onConfigComplete;
			requestManager.StartLoad ();
		}

		private void onConfigComplete ()
		{
			requestManager.onLoadComplete = null;
			_configStr = System.Text.Encoding.UTF8.GetString (requestManager.GetData (configURL));
			_configData = JSON.Parse (_configStr);

			loadAllByConfig ();
		}

		public void loadAllByConfig ()
		{
			if (_configData == null)
			{
				print ("no configData");
				return;
			}
			if (_configData["resources"] == null)
			{
				print ("no resources data");
				return;
			}
			var resources = _configData["resources"].AsArray;

			for (var i = 0; i < resources.Count; i++)
			{
				var res = resources[i];
				requestManager.Add (res["url"], new RequestParams { id = res["name"] });
			}
			requestManager.onLoadComplete = OnAllComplete;
			requestManager.StartLoad ();
		}

		protected void OnAllComplete ()
		{

			if (onComplete != null)
				onComplete ();
		}
	}
}