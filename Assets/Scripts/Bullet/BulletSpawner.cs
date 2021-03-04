using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {

	public ComponentBullet prefab;

	private Pool<ComponentBullet> pool;

	private void Awake() {
		prefab = Resources.Load<ComponentBullet>("Prefabs/Bullet");
		pool = new Pool<ComponentBullet>(100, BulletFactory,
			ComponentBullet.Enable, ComponentBullet.Disable, false);

	}

	private void OnObjectDisabled(params object[] parameters) {
		if (parameters[0] is Transform)
		{
			var component = ((Transform)parameters[0]).GetComponent<ComponentBullet>();
			if (component != null)
			{
				pool.DisablePoolObject(component);
			}
		}
	}

	private void SpawnBullet(params object[] parameters) {
		var bullet = pool.GetObject();
		if (bullet == null) return;
		FiringDataPackage data;
		if (parameters[0] is FiringDataPackage)
			data = (FiringDataPackage)parameters[0];
		else return;
		bullet.SetBehaviour(data.type);
		bullet.transform.position = data.position;
		bullet.transform.up = data.direction;
	}

	public ComponentBullet BulletFactory() {
		return Instantiate(prefab);
	}

	private void OnEnable() {
		EventManager.Subscribe("DisableBullet", OnObjectDisabled);
		EventManager.Subscribe("FireBullet", SpawnBullet);
	}

	private void OnDestroy() {
		EventManager.Unsubscribe("DisableBullet", OnObjectDisabled);
		EventManager.Unsubscribe("FireBullet", SpawnBullet);
	}
}
