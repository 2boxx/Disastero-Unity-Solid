using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LocalizationComponent : MonoBehaviour {

	private static LocalizationComponent instance;
	public static LocalizationComponent Instance { get { return instance; } }

	void Awake() {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            new LocalizationManager();
        }
        else Destroy(gameObject);
	}

	public void DownloadTextsDatabase() {
		LoadFromDisk();
	}

	public void LoadFromDisk() {
		string data = File.ReadAllText("Assets/Resources/Localization.json");
		List<object> parsedData = (List<object>)MiniJSON.Json.Deserialize(data);
		LocalizationManager.Instance.SetTexts(parsedData);
	}
}
