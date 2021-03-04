using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public Text livesCounter;
    public Text scoreCounter;
    public Text goalNumber;
    //public Text currentWeapon;

    [SerializeField]
    private int score;
    private int lives;

    public const int scoreUnit = 200;

    public void OnDestroyedAsteroidUI(params object[] parameters)
    {
        score += scoreUnit;
        scoreCounter.text =  score.ToString();
    }

    public void OnPlayerDied(params object[] parameters)
    {
        lives --;
        livesCounter.text = lives.ToString();
    }

	private void OnEnable() {
        EventManager.Subscribe("NotifyLives", UISetInitialLives);
        EventManager.Subscribe("NotifyGoal", UISetGoal);
        EventManager.Subscribe("AsteroidDestroyed", OnDestroyedAsteroidUI);
		EventManager.Subscribe("PlayerDied", OnPlayerDied);
        EventManager.Subscribe("LostLife", OnPlayerDied);
    }

    private void OnDisable() {
		EventManager.Unsubscribe("AsteroidDestroyed", OnDestroyedAsteroidUI);
		EventManager.Unsubscribe("PlayerDied", OnPlayerDied);
        EventManager.Unsubscribe("LostLife", OnPlayerDied);
        EventManager.Unsubscribe("NotifyLives", UISetInitialLives);
        EventManager.Subscribe("NotifyGoal", UISetGoal);
    }

    private void UISetInitialLives(params object[] parameters)
    {
        if (parameters[0] is int)
        {
            lives = (int)parameters[0];
            livesCounter.text = lives.ToString();
        } 

    }

    private void UISetGoal(params object[] parameters)
    {
        if (parameters[0] is int)
        {
            int n = (int)parameters[0] * HUDManager.scoreUnit;
            goalNumber.text = n.ToString();
        }

    }
}
