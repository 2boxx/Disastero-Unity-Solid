using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehaviourBase {

	protected Transform transform; 

	public BulletBehaviourBase() {
	}

	public virtual void SetTransform(Transform transform) {
		this.transform = transform;
	}

	public abstract void Update();

	public virtual void Enable() {
		transform.gameObject.SetActive(true);
	}

	public virtual void Disable() {
		transform.gameObject.SetActive(false);
	}

	protected virtual void NotifyDisable() {
		EventManager.CallEvent("DisableBullet", transform);
	}

}
