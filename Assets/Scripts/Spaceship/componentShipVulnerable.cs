using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class componentShipVulnerable : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D col)
    {
        EventManager.CallEvent("LostLife", new CollisionDataPackage { sender = this, collission = col });
    }
}
