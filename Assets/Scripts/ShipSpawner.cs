using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour {


    public float invulnerabilityTime;
    public float respawnTime;
    public float flashInterval = 0.25f;
    private GameObject ship;
    private Collider2D shipCol;
    private SpriteRenderer shipRenderer;

    private void OnEnable()
    {
        EventManager.Subscribe("Respawn", Respawn);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("Respawn", Respawn);
    }

    private void Respawn(params object[] parameters)
    {
        if (parameters[0] is ShipDataPackage)
        {
            ShipDataPackage sp = (ShipDataPackage)parameters[0];
            ship = sp.player;
            shipCol = ship.GetComponent<Collider2D>();
            shipRenderer = ship.GetComponent<SpriteRenderer>();
            StartCoroutine("RespawnCoroutine");
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        ship.transform.position = Vector2.zero;
        ship.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        ship.SetActive(true);
        StartCoroutine("InvulnerableCoroutine");
    }

    private IEnumerator InvulnerableCoroutine()
    {
        shipCol.enabled = false;
        float t = 0f;
        while (t < invulnerabilityTime)
        {
            t += flashInterval * 2;
            shipRenderer.enabled = true;
            yield return new WaitForSeconds(flashInterval);
            shipRenderer.enabled = false;
            yield return new WaitForSeconds(flashInterval);
        }
        shipRenderer.enabled = true;
        shipCol.enabled = true;
    }
}
