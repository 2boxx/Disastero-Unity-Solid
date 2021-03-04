using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFiringBehaviourBombAutomatic : ShipFiringBehaviourAutomatic {

	private float bombExplosionRadius; //radio de explosion de la bala bomba

	public ShipFiringBehaviourBombAutomatic(Transform transform,
		float firingInterval = 1, float bulletDuration = 1, float bulletSpeed = 1,
		float bombRadius = 1) : base(transform, firingInterval, bulletDuration, bulletSpeed) {

		bombExplosionRadius = bombRadius;
	}

	//Builder addon para radio explosion
	public ShipFiringBehaviourBombAutomatic SetExplosionRadius(float radius) {
		bombExplosionRadius = radius;
		return this;
	}

	//Llamado por el metodo Fire() del padre, cual maneja el timer de disparo
	protected override void CallFireBullet() {
		//Llama al evento de disparar. Pasa dato de direccion, posicion, radio explosion, y tipo.
		EventManager.CallEvent("FireBullet", new FiringDataPackage
		{
			direction = transform.up,
			position = transform.position,
			type = new BulletBehaviourBomb().SetDuration(bulletDuration)
				.SetSpeed(bulletSpeed).SetExplosionRadius(bombExplosionRadius)
		});
	}

}
