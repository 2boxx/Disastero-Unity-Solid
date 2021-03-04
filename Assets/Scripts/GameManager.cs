using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /// <summary>
    /// Asteroid destrucitons needed to win
    /// </summary>
    public int goal;
    private int currentAsteroids;

    private void OnEnable()
    {
        EventManager.Subscribe("AsteroidDestroyed", AddScore);
        EventManager.Subscribe("GameOver", Lose);
        EventManager.Subscribe("GameWon", Win);
    }
    private void OnDisable()
    {
        EventManager.Unsubscribe("GameOver", Lose);
        EventManager.Unsubscribe("GameWon", Win);
        EventManager.Unsubscribe("AsteroidDestroyed", AddScore);
    }

    private void Start()
    {
        currentAsteroids = 0;
        EventManager.CallEvent("NotifyGoal", goal);
    }

    private void AddScore(params object[] parameters)
    {
        currentAsteroids += 1;
        if (currentAsteroids >= goal) EventManager.CallEvent("GameWon");
    }

    private void Win(params object[] parameters)
    {
        currentAsteroids = 0;
        EventManager.CallEvent("ChangeScene", 2);
    }

    private void Lose(params object[] parameters)
    {
        currentAsteroids = 0;
        EventManager.CallEvent("ChangeScene", 3);
    }

}
