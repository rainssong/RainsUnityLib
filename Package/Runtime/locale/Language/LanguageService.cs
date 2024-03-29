﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using SimpleJSON;


namespace Language
{
	public class LanguageInfo : IEquatable<LanguageInfo>
	{
		public string Name;
		public LanguageInfo(){}

		public LanguageInfo(string name)
		{
			Name = name;
		}

		public static readonly LanguageInfo English = new LanguageInfo("English");

		public bool Equals(LanguageInfo other)
		{
			return other.Name == Name;
		}
	}

	[ExecuteInEditMode]
	public class LanguageService {

		private static LanguageService _instance;
		public static LanguageService Instance
		{
			get { return _instance ?? (_instance = new LanguageService()); }
		}
		public List<string> Files { get; set; }
		public Dictionary<string, Dictionary<string, string>> StringsByFile { get; set; }
		public Dictionary<string, string> Strings { get; set; }

		public List<LanguageInfo> Languages = new List<LanguageInfo>
		{
			LanguageInfo.English,
		};

		public List<String> LanguageNames = new List<String> ();

		private LanguageInfo _language = new LanguageInfo {Name = "English" };
        public Action changeLanCB;

        public LanguageInfo Language
		{
			get { return _language; }
			set
			{
				if (!HasLanguage(value))
				{
					Debug.LogError("Invalid Language " + value);
				}

				_language = value;
				ReadJsonFiles();
			}
		}
		bool HasLanguage(LanguageInfo language)
		{
			foreach (var systemLanguage in Languages)
			{
				if (systemLanguage.Equals(language))
					return true;
			}
			return false;
		}

		public LanguageService()
		{
			LoadContent();
		}

		public void LoadContent()
		{		
			TextAsset config = Resources.Load<TextAsset>("LocalizationConfig");
			var jsonArray = JSONNode.Parse (config.text);
			LoadAllLanguages ((JSONObject)jsonArray);
		}

		void LoadAllLanguages(JSONObject jsonClass){
			var d = LanguageInfo.English;
			foreach(KeyValuePair<string, JSONNode> json in jsonClass)
			{
				var language = new LanguageInfo (json.Value);
				LanguageNames.Add (language.Name);
				Languages.Add(language);
				if (json.Key == "Default"){
					d = language;
				}					
			}
			Language = d;
		}

		public string GetFromFile(string groupId, string key, string fallback)
		{
			if (!StringsByFile.ContainsKey(groupId))
			{
				Debug.LogWarning("Localization File Not Found : " + groupId);
				return fallback;
			}
			var group = StringsByFile[groupId];
			if (!group.ContainsKey(key))
			{
				return key;
			}
			return group[key];
		}
		// 读取Josn文件
		void ReadJsonFiles()
		{
			Strings = new Dictionary<string, string>();
			StringsByFile = new Dictionary<string, Dictionary<string, string>>();
			Files = new List<string>();

			var path = "Localization/" + Language.Name + "/";

			var resources = Resources.LoadAll<TextAsset>(path);
			if (!resources.Any())
			{
				Debug.LogError("Localization Files Not Found : " + Language.Name);
			}
			foreach (var resource in resources)
			{
				ReadTextAsset(resource);
			}
		}

		// 将TextAsset内容读取到字典中
		void ReadTextAsset(TextAsset resource)
		{
			var jsonArray = JSONNode.Parse (resource.text);
			Files.Add(resource.name);
			StringsByFile.Add(resource.name, new Dictionary<string, string>());
			foreach(KeyValuePair<string, JSONNode> json in (JSONObject)jsonArray)
			{
				StringsByFile[resource.name].Add(json.Key, json.Value);
				if (Strings.ContainsKey(json.Key))
					Debug.LogWarning("Duplicate string : " + resource + " : " + json.Key);
				else
					Strings.Add(json.Key, json.Value);
			}
			if (changeLanCB != null)
                changeLanCB.Invoke();
		}

		// 根据key获取相应的语言内容
		public string GetStringByKey(string key, string fallback="")
		{
			if (!Strings.ContainsKey(key))
			{
				//Debug.LogWarning(string.Format("Localization Key Not Found {0} : {1} ", Language.Name, key));
				return fallback;
			}
			return Strings[key]; 
		}

		//public void AddFiledToJson(string key)
		//{

		//}

		public bool Contains(string key)
		{
			return !string.IsNullOrEmpty(key) && Strings.ContainsKey(key);
		}

		public void UpdateText(GameObject textObj, string key, params string[] value)
		{
			Text label = textObj.transform.GetComponent<Text>();
			string newText = GetStringByKey (key, label.text);
			if (value.Length > 0){
				for(int i = 0; i < value.Length; i++){
					newText = string.Format (newText, value[i]);
				}
			}
			label.text = newText;
		}			
	}
}