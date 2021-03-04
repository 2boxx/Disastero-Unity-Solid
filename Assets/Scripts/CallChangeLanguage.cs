using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallChangeLanguage : MonoBehaviour {

    public void ChangeLanguage(UserLanguage newLang)
    {
        LocalizationManager.Instance.ChangeLanguage(newLang);
    }

    public void ChangeLanguageInt(int newLang)
    {
        LocalizationManager.Instance.ChangeLanguage((UserLanguage)newLang);
    }
}
