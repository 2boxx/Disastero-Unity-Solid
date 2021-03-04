using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSceneLoader : MonoBehaviour {

    public void LoadScene(int index)
    {
        EventManager.CallEvent("ChangeScene", index);
    }
}
