using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExampleScreen : MonoBehaviour 
{
    public Text[] localizableTexts;
    public UserLanguage language;
    	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateTexts();
        }
	}

    private void UpdateTexts()
    {
        for (int i = 0; i < localizableTexts.Length; i++)
        {
            string key = gameObject.name + "." + localizableTexts[i].gameObject.name;
            localizableTexts[i].text = LocalizationManager.Instance.TryGetText(language, key);
        }
    }
}

public enum UserLanguage
{
    English,
    Spanish,
    Japanese
}
