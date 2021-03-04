using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LocalizationManager
{
    private Dictionary<UserLanguage, Dictionary<string, string>> _texts;
    private static LocalizationManager _instance;
    public static LocalizationManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new LocalizationManager();
               
            return _instance;
        }
    }
    public UserLanguage userLanguage;

    public LocalizationManager()
    {
        //Debug.Log("Lenguaje OS: " + Application.systemLanguage);
        _instance = this;
        _texts = new Dictionary<UserLanguage, Dictionary<string, string>>();
        _texts.Add(UserLanguage.English, new Dictionary<string, string>());
        _texts.Add(UserLanguage.Spanish, new Dictionary<string, string>());
        _texts.Add(UserLanguage.Japanese, new Dictionary<string, string>());
        LocalizationComponent.Instance.DownloadTextsDatabase();
    }

    public void ChangeLanguage(UserLanguage newLang)
    {
        userLanguage = newLang;
        LocalizationFetch.UpdateAllTexts(newLang);
    }

    public void SetTexts(List<object> databaseResult)
    {
        for (int i = 0; i < databaseResult.Count; i++)
        {
            Dictionary<string, object> currentDictionary = (Dictionary<string, object>)databaseResult[i];
            switch (currentDictionary["language"].ToString())
            {
                case "eng":
                    _texts[UserLanguage.English].Add(currentDictionary["textkey"].ToString(), currentDictionary["textvalue"].ToString());
                    break;
                case "spa":
                    _texts[UserLanguage.Spanish].Add(currentDictionary["textkey"].ToString(), currentDictionary["textvalue"].ToString());
                    break;
                case "jp":
                    _texts[UserLanguage.Japanese].Add(currentDictionary["textkey"].ToString(), currentDictionary["textvalue"].ToString());
                    break;
                default:
                    break;
            }
        }
    }


    public string TryGetText(UserLanguage language, string key)
    {
        if (_texts[language].ContainsKey(key))
        {
            return _texts[language][key];
        }
        else
        {
            return key;
            //throw new Exception("No existe esa key");
        }
    }
}
