using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviourNormal : BulletBehaviourBase/*, IBridgeCollisionEnter*/ {

	private float speed; //velocidad bala
	private float maxDuration; //duracion de vida
	private Rigidbody2D thisRB; //rigidbody de bala

	//timer de vida
	private float lifetime = 0;

	#region Builder
	public BulletBehaviourNormal SetSpeed(float speed) {
		this.speed = speed;
		return this;
	}
	public BulletBehaviourNormal SetDuration(float duration) {
		maxDuration = duration;
		return this;
	}
	#endregion

	//Behaviour/Strategy Update
	public override void Update() {
		//tiempo de vida. Desactiva al alcanzar maxDuration
		lifetime += Time.deltaTime;
		if (lifetime > maxDuration)
		{
			NotifyDisable();
		}

		//velocidad constante
		if(thisRB != null)
			thisRB.velocity = transform.up * speed;
	}

	//Pool enable
	public override void Enable() {
		base.Enable();
		//resetea el timer de vida
		lifetime = 0;

		//apariencia
		transform.localScale = new Vector3(1, 1, 1);
		var render = transform.GetComponent<SpriteRenderer>();
		if (render != null)
		{
			render.enabled = true;
			render.color = Color.yellow;
		}
        var line = transform.GetComponent<LineRenderer>();
        if (line != null) line.enabled = false;
        //configura collider y rigidbody
        var col = transform.GetComponent<Collider2D>();
		if (col != null) col.enabled = true;
		if (thisRB != null) thisRB.isKinematic = false;

		//subscripcion al evento de colision
		EventManager.Subscribe("CollisionEnter", CollissionEnter);
	}

	//Pool disable
	public override void Disable() {
		base.Disable();
		EventManager.Unsubscribe("CollisionEnter", CollissionEnter);
	}

	//Asigna el transform de la bala al behaviour
	public override void SetTransform(Transform parent) {
		base.SetTransform(parent);
		thisRB = transform.GetComponent<Rigidbody2D>();
		if (thisRB == null)
			Debug.Log("Bullet " + transform.gameObject.name + " doesn't contain RigidBody2D");
	}

	public void CollissionEnter(params object[] parameters) {
		//Se fija la data del evento y termina antes si no esta involucrado
		if (!(parameters[0] is CollisionDataPackage)) return;
		var data = (CollisionDataPackage)parameters[0];
		if (data.sender.transform != transform) return;
		//aplica daño si involucrado
		var damageable = data.collission.gameObject.GetComponent<IDamageable>();
		if (damageable != null)
			damageable.Damage();
		//Se desactiva a si misma
		NotifyDisable();

	}

	/*
	public void OnCollisionEnter(Collision2D collision) {
		var damageable = collision.gameObject.GetComponent<IDamageable>();
		if (damageable != null)
			damageable.Damage();
		NotifyDisable();
	}*/
}
