using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationFetch : MonoBehaviour {

	public static List<LocalizationFetch> instances;

	public Text targetText;
	public string textKey;

	private void OnEnable() {
		if (instances == null) instances = new List<LocalizationFetch>();
		instances.Add(this);
	}

	private void OnDisable() {
		instances.Remove(this);
	}

    private void Start()
    {
        UpdateText(LocalizationManager.Instance.userLanguage);
    }

    public void UpdateText(UserLanguage language) {
		targetText.text = LocalizationManager.Instance.TryGetText(language, textKey);
	}

	public static void UpdateAllTexts(UserLanguage language) {
		if (instances == null) return;
		foreach (var item in instances)
		{
			if (item != null) item.UpdateText(language);
		}
	}
}
