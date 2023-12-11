using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using UnityEditor;
using SimpleJSON;
using System.IO;


#if UNITY_EDITOR
namespace Language
{
	[CustomEditor(typeof(LanguageText), true)]
	public class LanguageEditor : Editor
    {
		protected LanguageText Target;
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			Target = target as LanguageText;

			if (Application.isPlaying)
				return;

			var service = LanguageService.Instance;


            if (service == null || service.Strings == null)
			{
				var p = EditorGUILayout.TextField("Key", Target.Key);
				if (p != Target.Key)
				{
					Target.Key = p;
					EditorUtility.SetDirty(target);
				}
				EditorGUILayout.LabelField("Error ", "ILocalizationService Not Found");
			}
			else
			{
				var languages = service.LanguageNames.ToArray();
				var languageIdx = Array.IndexOf(languages, service.Language.Name);
				var language = EditorGUILayout.Popup("Language", languageIdx, languages);
				if (language != languageIdx){
					Target.Language = languages[language];
					service.Language = new LanguageInfo (languages[language]);

					EditorUtility.SetDirty(target);
				}
				if (!string.IsNullOrEmpty(Target.Key)){
					Target.Value = service.GetStringByKey(Target.Key, Target.Key);
				}


				//TODO：迁移到Service中
				if(!service.Contains(Target.Key))
				{
					if (GUILayout.Button("Add Field to JSON"))
					{
                        //service.StringsByFile[Target.File].Select(o => o.Key).ToArray(); 
                        var path = "Localization/" + service.Language.Name + "/"+Target.File;
						//读取JSON
                        var resource = Resources.Load<TextAsset>(path);

						var fullPath = "Assets/Resources/" + path + ".json";

                        if (resource != null)
						{
							
                            service.LoadContent();
                            var json=JSON.Parse(resource.text);
							json.Add(Target.Key, Target.Key);
							File.WriteAllText(fullPath, json.ToString());
							Debug.Log("Add" + Target.Key);
							AssetDatabase.Refresh();
							service.LoadContent();
							System.Diagnostics.Process.Start(System.Environment.CurrentDirectory+"/"+ fullPath);
							//Application.OpenURL("./"+fullPath);
                        }
                    }

                }

				var files = service.StringsByFile.Select(o => o.Key).ToArray();

				var findex = Array.IndexOf(files, Target.File);
				var fi = EditorGUILayout.Popup("File", findex, files);

				if (findex == -1 || fi != findex){
					Target.File = files[0]; 
					EditorUtility.SetDirty(target);
				}
				//
				if (!string.IsNullOrEmpty(Target.File))
				{
					var words = service.StringsByFile[Target.File].Select(o => o.Key).ToArray();
					var index = Array.IndexOf(words, Target.Key);

					var i = EditorGUILayout.Popup("Keys", index, words);

					if (i != index)
					{
						Target.Key = words[i];
						Target.Value = service.GetStringByKey(Target.Key, string.Empty);
						EditorUtility.SetDirty(target);
					}

				}
				if (!string.IsNullOrEmpty(Target.Value))
				{
					EditorGUILayout.LabelField("Value ", Target.Value);
					Target.GetComponent<UnityEngine.UI.Text>().text = Target.Value;
				}
			}
		}


        //private void OnValidate()
        //{
        //	Target = target as LanguageText;

        //	if (Target.Key == "")
        //		Target.Key = Target.GetComponent<UnityEngine.UI.Text>().text;
        //}

        //public void OnEnable()
        //{
        //	Target = target as LanguageText;

        //	if (Application.isPlaying)
        //		return;

        //	if (Target.Key == "")
        //		Target.Key = Target.GetComponent<UnityEngine.UI.Text>().text;
        //}
    }
}
#endif