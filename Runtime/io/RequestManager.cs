using System.Net.Mime;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using com.rainssong.utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

namespace com.rainssong.io
{
    public class RequestManager : MonoBehaviour
    {
        protected List<LoadingItem> loadList = new List<LoadingItem>();
        public Dictionary<string, UnityWebRequest> urlDic = new Dictionary<string, UnityWebRequest>();
        public Dictionary<string, UnityWebRequest> idDic = new Dictionary<string, UnityWebRequest>();
        public Action onLoadComplete;

        public int completeFlag = 0;

        public bool isInited = false;

        public string baseUrl = "";

        public bool printLog = false;

        public void Reset()
        {
            if (urlDic != null)
                foreach (var item in urlDic)
                {
                    item.Value.Dispose();
                }
            loadList = new List<LoadingItem>();
            urlDic = new Dictionary<string, UnityWebRequest>();
            idDic = new Dictionary<string, UnityWebRequest>();
        }


        public string GetFullPath(string path)
        {
            if (!path.Contains(":/"))
            {
                if (baseUrl.EndsWith("/") || path.StartsWith("/"))
                {
                    return baseUrl + path;
                }
                else
                {
                    return baseUrl + "/" + path;
                }
            }
            return path;
        }

        protected UnityWebRequest CreateRequest(string url, RequestParams pams = null)
        {
            var urlFix = GetFullPath(url);

            //FIXME:应该用URI避免双斜杠问题
            //如果是相对路径，补充完整
            // if(!urlFix.Contains(@":/"))
            //     urlFix=baseUrl+"/"+urlFix;

            //FIXME:使用URI后此问题还存在吗？
            //修正安卓环境下未加前缀的问题
            /*#if UNITY_ANDROID
                        if (urlFix.Contains (Application.persistentDataPath) && !urlFix.Contains ("file://"))
                            urlFix = "file://" + urlFix;
            #endif*/

            UnityWebRequest webRequest = new UnityWebRequest(urlFix);
            // UnityWebRequest webRequest = UnityWebRequest.Get (urlFix);
            urlDic[urlFix] = webRequest;
            webRequest.method = Http.Get;

            if (pams != null && pams.id != null)
                idDic[pams.id] = webRequest;


            if (printLog) Debug.Log("RequestManager.CreateRequest:" + urlFix);

            if (urlFix.EndsWith("png", true, CultureInfo.CurrentCulture) || urlFix.EndsWith("jpg", true, CultureInfo.CurrentCulture) || urlFix.EndsWith("jpeg", true, CultureInfo.CurrentCulture))
            {
                webRequest.downloadHandler = new DownloadHandlerTexture();
            }
            else if (urlFix.EndsWith("mp3", true, CultureInfo.CurrentCulture) || urlFix.EndsWith("wmv", true, CultureInfo.CurrentCulture))
                webRequest.downloadHandler = new DownloadHandlerAudioClip(urlFix, AudioType.UNKNOWN);
            else
            {
                webRequest.downloadHandler = new DownloadHandlerBuffer();
            }



            return webRequest;
        }

        public byte[] GetDataByID(string id)
        {
            if (idDic.ContainsKey(id))
                return idDic[id].downloadHandler.data;
            return null;
        }

        public Texture2D GetTexture2D(string idOrURL)
        {
            return GetTexture2DByID(idOrURL) ?? GetTexture2DByURL(idOrURL) ?? null;
        }

        public Sprite GetSprite(string idOrURL)
        {
            var t2d = GetTexture2D(idOrURL);
            var sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2());
            return sprite;
        }

        public Texture2D GetTexture2DByID(string id)
        {
            if (GetRequestByID(id) != null)
                return DownloadHandlerTexture.GetContent(GetRequestByID(id));
            return null;
        }

        public Texture2D GetTexture2DByURL(string url)
        {
            if (GetRequestByURL(url) != null)
                return DownloadHandlerTexture.GetContent(GetRequestByURL(url));
            return null;
        }

        public AudioClip GetAudioClip(string value)
        {
            if (GetRequest(value) != null)
                return DownloadHandlerAudioClip.GetContent(GetRequest(value));
            return null;
        }

        public byte[] GetDataByURL(string url)
        {
            if (urlDic.ContainsKey(url))
                return urlDic[url].downloadHandler.data;
            if (urlDic.ContainsKey(GetFullPath(url)))
                return urlDic[GetFullPath(url)].downloadHandler.data;
            return null;
        }


        public UnityWebRequest GetRequest(string idOrURL)
        {
            return GetRequestByID(idOrURL) ?? GetRequestByURL(idOrURL);
        }

        public UnityWebRequest GetRequestByID(string id)
        {
            if (idDic.ContainsKey(id))
                return idDic[id];
            return null;
        }

        public UnityWebRequest GetRequestByURL(string url)
        {
            string urlFix;
            urlFix = GetFullPath(url);

            if (printLog)
                print("RequestManager.GetRequestByURL:" + urlFix);
            //在安卓下多了个jar:file:///的前缀
            if (!urlDic.ContainsKey(urlFix))
                CreateRequest(urlFix);
            return urlDic[urlFix];
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        public void Get(string url, Action<UnityWebRequest> actionResult)
        {
            if (printLog)
                Debug.Log("RequestManager.Get:" + url);
            StartCoroutine(_Get(url, actionResult));
        }

        protected IEnumerator _Get(string url, Action<UnityWebRequest> actionResult = null)
        {
            UnityWebRequest webRequest = GetRequestByURL(url);
            //print("Send"+url);
            //FIXME:InvalidOperationException: UnityWebRequest has already been sent; cannot begin sending the request again
            yield return webRequest.SendWebRequest();


            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                if (printLog) Debug.Log(webRequest.error + ":" + webRequest.url);
            }
            else
            {
                if (printLog) Debug.Log("RequestManager.Suceess:" + webRequest.url);
                actionResult?.Invoke(webRequest);
            }
        }

        /// <summary>
        /// 向服务器提交post请求
        /// </summary>
        /// <param name="serverURL">服务器请求目标地址,like "http://www.my-server.com/myform"</param>
        /// <param name="lstformData">form表单参数</param>
        /// <param name="lstformData">处理返回结果的委托</param>
        /// <returns></returns>
        IEnumerator _Post(string serverURL, List<IMultipartFormSection> lstformData, Action<UnityWebRequest> actionResult)
        {
            //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            //formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
            //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));
            UnityWebRequest webRequest = UnityWebRequest.Post(serverURL, lstformData);
            yield return webRequest.SendWebRequest();
            actionResult?.Invoke(webRequest);
        }

        public void Add(string url, RequestParams param = null)
        {
            var li = new LoadingItem();
            li.url = url;
            //提前创建绑定ID
            CreateRequest(url, param);
            if (printLog)
                Debug.Log("RequestManager.Add:" + url);
            loadList.Add(li);
        }

        /// <summary>
        /// 单次完成,检测整体完成情况
        /// </summary>
        protected void OnComplete(UnityWebRequest uwr)
        {
            completeFlag++;

            if (printLog)
                print("complete: " + uwr.url + " " + completeFlag + "/" + loadList.Count);

            if (completeFlag == loadList.Count)
            {
                loadList = new List<LoadingItem>();
                if (onLoadComplete != null)
                    onLoadComplete.Invoke();
            }


        }

        public void StartLoad()
        {
            completeFlag = 0;

            for (int i = 0; i < loadList.Count; i++)
            {
                Get(loadList[i].url, OnComplete);
            }
        }

        public byte[] GetData(string idOrURL)
        {
            return GetDataByID(idOrURL) ?? GetDataByURL(idOrURL);
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="downloadFilePathAndName">储存文件的路径和文件名 like 'Application.persistentDataPath+"/unity3d.html"'</param>
        /// <param name="actionResult">请求发起后处理回调结果的委托,处理请求对象</param>
        /// <returns></returns>
        IEnumerator _DownloadFile(string url, string downloadFilePathAndName, Action<UnityWebRequest> actionResult)
        {
            var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
            uwr.downloadHandler = new DownloadHandlerFile(downloadFilePathAndName);
            yield return uwr.SendWebRequest();
            if (actionResult != null)
            {
                actionResult(uwr);
            }
        }


        private void OnApplicationQuit()
        {
            foreach (var item in urlDic)
            {
                item.Value.Dispose();
            }
            urlDic.Clear();
            idDic.Clear();
        }

        [ContextMenu("Destroy")]
        public void Destroy()
        {

            if (printLog)
                print("Destroy");
        }
    }

    public class LoadingItem
    {
        public string id;
        public string url;
    }

    public class RequestParams
    {
        public string id;
    }


}