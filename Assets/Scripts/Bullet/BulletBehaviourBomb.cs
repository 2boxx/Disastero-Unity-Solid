using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviourBomb : BulletBehaviourBase {

	private float speed; //velocidad de movimiento
	private float maxDuration; //duracion antes de explotar
	private float explosionRadius; //radio de explosion
	private Rigidbody2D thisRB; //rigidbody de bala


	private float lifetime = 0;

	#region Builder
	public BulletBehaviourBomb SetSpeed(float speed) {
		this.speed = speed;
		return this;
	}
	public BulletBehaviourBomb SetDuration(float duration) {
		maxDuration = duration;
		return this;
	}
	public BulletBehaviourBomb SetExplosionRadius(float radius) {
		explosionRadius = radius;
		return this;
	}
	#endregion

	//Behaviour/Strategy Update
	public override void Update() {
		//tiempo de vida. Detona al alcanzar maxDuration
		lifetime += Time.deltaTime;
		if (lifetime > maxDuration)
		{
			Explode();
		}

		//Velocidad constante
		if (thisRB != null)
			thisRB.velocity = transform.up * speed;
	}

	//Pool enable
	public override void Enable() {
		base.Enable();
		//resetea el timer de vida
		lifetime = 0;

		//apariencia
		transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		var render = transform.GetComponent<SpriteRenderer>();
		if (render != null)
		{
			render.enabled = true;
			render.color = Color.red;
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

	//Metodo correspondiente al evento "CollisionEnter"
	public void CollissionEnter(params object[] parameters) {
		//Se fija la data del evento y termina antes si no esta involucrado
		if (!(parameters[0] is CollisionDataPackage)) return;
		var data = (CollisionDataPackage)parameters[0];
		if (data.sender.transform != transform) return;

		//Si es el involucrado, explota
		Explode();
	}

	private void Explode() {
		//Obtiene los colliders de la explosion
		Collider2D[] exploded = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

		//Llama al evento explosion
		EventManager.CallEvent("BombExplosion",
			new ExplosionDataPackage { source = transform, colliders = exploded });

		//Daña a IDamageables cercanos
		foreach (var item in exploded)
		{
			if (item is IDamageable) ((IDamageable)item).Damage();
		}

		//desactiva a si misma
		NotifyDisable();
	}
}
