using com.rainssong.utils;
using Language;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManagerBase : SingletonMono<AppManagerBase>
{
    public string version=>Application.version;
    public string defalutLan = "English";
    public const string homePageUrl = "https://www.rainssong.com";
    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        InitLan();
    }

    public void InitLan()
    {
        var lan = PlayerPrefs.GetString("language", Application.systemLanguage.ToString());

        ChangeLan(lan);
    }

    public void ChangeLan(string lan)
    {
        var hasLan = LanguageService.Instance.LanguageNames.IndexOf(lan) >= 0;
        if (hasLan)
            LanguageService.Instance.Language = new LanguageInfo(lan);
        else
            LanguageService.Instance.Language = new LanguageInfo(defalutLan);

        PlayerPrefs.SetString("language", LanguageService.Instance.Language.Name);
    }


    public void OpenHomePage()
    {
        Application.OpenURL(homePageUrl);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
