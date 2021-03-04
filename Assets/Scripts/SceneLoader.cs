using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public static SceneLoader instance;

    private void OnEnable()
    {
        EventManager.Subscribe("ChangeScene", SetSceneProxy);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("ChangeScene", SetSceneProxy);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void SetSceneProxy(params object[] parameters)
    {
        if(parameters[0] is int)
        {
            SceneManager.LoadScene((int)parameters[0]);
        }
    }
    
}
