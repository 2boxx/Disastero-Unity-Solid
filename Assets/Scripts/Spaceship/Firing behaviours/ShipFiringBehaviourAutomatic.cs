using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFiringBehaviourAutomatic : IShipFiringBehaviour {

	private float bulletInterval; //intervalo entre balas
	private float lastBulletTime;

	//Bullet parameters
	protected float bulletDuration;
	protected float bulletSpeed;

	protected Transform transform;

	public ShipFiringBehaviourAutomatic(Transform transform,
		float firingInterval = 1, float bulletDuration = 1, float bulletSpeed = 1) {

		this.transform = transform;

		bulletInterval = firingInterval;
		this.bulletDuration = bulletDuration;
		this.bulletSpeed = bulletSpeed;

	}

	#region Builder
	public ShipFiringBehaviourAutomatic SetBulletInterval(float interval) {
		bulletInterval = interval;
		return this;
	}
	public ShipFiringBehaviourAutomatic SetBulletDuration(float duration) {
		bulletDuration = duration;
		return this;
	}
	public ShipFiringBehaviourAutomatic SetBulletSpeed(float speed) {
		bulletSpeed = speed;
		return this;
	}
	#endregion Builder

	//Metodo llamado por el controlador. Maneja el timer de disparo
	public virtual void Fire() {
		if ((Time.time - lastBulletTime) >= bulletInterval)
		{
			lastBulletTime = Time.time;
			CallFireBullet();
		}
	}

	//Lo que ejecuta el disparo en si. Timer manejado por Fire()
	protected virtual void CallFireBullet() {
		//Llama al evento de disparar. Pasa dato de direccion, posicion, y tipo.
		EventManager.CallEvent("FireBullet", new FiringDataPackage
		{
			direction = transform.up,
			position = transform.position,
			type = new BulletBehaviourNormal()
				.SetSpeed(bulletSpeed).SetDuration(bulletDuration).SetSpeed(bulletSpeed)
		});
	}
}
