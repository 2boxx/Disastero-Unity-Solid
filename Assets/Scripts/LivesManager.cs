using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour {
    /// <summary>
    /// Player lives per session.
    /// </summary>
    public int initialLives;
    /// <summary>
    /// Lives left. 1 = using last life.
    /// </summary>
    private int livesLeft;

    private void Start()
    {
        livesLeft = initialLives;
        EventManager.CallEvent("NotifyLives", livesLeft);
    }

    private void OnEnable()
    {
        EventManager.Subscribe("LostLife", OnPlayerDeath);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("LostLife", OnPlayerDeath);
    }


    private void OnPlayerDeath (params object[] parameters)
    {
        if (parameters[0] is CollisionDataPackage)
        {
            CollisionDataPackage cp = (CollisionDataPackage)parameters[0];
            if (livesLeft > 0)
                {
                EventManager.CallEvent("Respawn", new ShipDataPackage { player = cp.sender.gameObject});
                livesLeft--;
                }
            else EventManager.CallEvent("GameOver");
        }
    }
}
