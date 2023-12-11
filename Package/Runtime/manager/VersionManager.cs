using com.rainssong.utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


namespace com.rainssong.manager
{

    public class VersionManager : SingletonMono<VersionManager>
    {
        private System.Version curVersion;
        private System.Version minVersion;
        public bool IsNeedUpdate;
        [SerializeField]
        public const string ServerMinVersionUrl = "Min Version API";

        public void CheckVersion(UnityAction<bool> onComplete = null)
        {
            IsNeedUpdate = false;
            StartCoroutine(CheckVersionCoroutine(onComplete));
        }

        private IEnumerator CheckVersionCoroutine(UnityAction<bool> onComplete)
        {
            // Fetch server minimum version
            UnityWebRequest req = UnityWebRequest.Get(ServerMinVersionUrl);
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(req.error);
                yield break;
            }

            minVersion = new System.Version(req.downloadHandler.text);
            curVersion = new System.Version(Application.version);

            if (curVersion < minVersion)
            {
                Debug.LogFormat("Current version ({0}) is below the minimum required version ({1}). Please update.", curVersion, minVersion);
                IsNeedUpdate = true;
            }

            Debug.Log("Version check completed!");

            if (onComplete != null)
            {
                onComplete(IsNeedUpdate);
            }
        }
    }

}