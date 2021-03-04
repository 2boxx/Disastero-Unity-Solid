using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ComponentBullet : MonoBehaviour {

	private BulletBehaviourBase activeBehaviour;

	private bool postponeInitDisabling = false;

	private void Awake() {
		if (postponeInitDisabling) Disable(this);
	}

	private void Update() {
		activeBehaviour.Update();
	}

	public static void Enable(ComponentBullet item) {
		if (item == null) return;
		item.gameObject.SetActive(true);
	}

	public static void Disable(ComponentBullet item) {
		item.gameObject.SetActive(false);
	}

	public void PostponeDisabling() {
		postponeInitDisabling = true;
	}

	public void SetBehaviour(BulletBehaviourBase behaviour) {
		activeBehaviour = behaviour;
		behaviour.SetTransform(transform);
		activeBehaviour.Enable();
	}

	#region Collission
	private void OnCollisionEnter2D(Collision2D collision) {
		EventManager.CallEvent("CollisionEnter",
			new CollisionDataPackage { sender = this, collission = collision });

		if(activeBehaviour is IBridgeCollisionEnter)
			((IBridgeCollisionEnter)activeBehaviour).OnCollisionEnter(collision);
	}
	private void OnCollisionStay2D(Collision2D collision) {
		if (activeBehaviour is IBridgeCollisionStay)
			((IBridgeCollisionStay)activeBehaviour).OnCollisionStay(collision);
	}
	private void OnCollisionExit2D(Collision2D collision) {
		if (activeBehaviour is IBridgeCollisionExit)
			((IBridgeCollisionExit)activeBehaviour).OnCollisionExit(collision);
	}
	#endregion
	#region Trigger
	private void OnTriggerEnter2D(Collider2D collision) {
		EventManager.CallEvent("TriggerEnter",
			new TriggerDataPackage { sender = this, collider = collision });

		if (activeBehaviour is IBridgeTriggerEnter)
			((IBridgeTriggerEnter)activeBehaviour).OnTriggerEnter(collision);
	}
	private void OnTriggerStay2D(Collider2D collision) {
		if (activeBehaviour is IBridgeTriggerStay)
			((IBridgeTriggerStay)activeBehaviour).OnTriggerStay(collision);
	}
	private void OnTriggerExit2D(Collider2D collision) {
		if (activeBehaviour is IBridgeTriggerExit)
			((IBridgeTriggerExit)activeBehaviour).OnTriggerExit(collision);
	}
	#endregion

}
