using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipMovementBehaviour {
	void MoveForward(); //param float speed?
	void MoveBackwards();
	void TurnLeft(); //param float turnRate?
	void TurnRight();
}

public interface IShipMovementController {
	void Control(IShipMovementBehaviour behaviour);
}

public interface IShipFiringBehaviour {
	void Fire();
}

public interface IShipFiringController {
	void Control(IShipFiringBehaviour behaviour);
}


//Clase base meteoro?

public interface IHasRigidbody {
	Rigidbody2D GetRigidbody();
}

public interface IDamageable {
	void Damage();
}

#region Collission
public interface IBridgeCollisionEnter {
	void OnCollisionEnter(Collision2D collision);
}
public interface IBridgeCollisionStay {
	void OnCollisionStay(Collision2D collision);
}
public interface IBridgeCollisionExit {
	void OnCollisionExit(Collision2D collision);
}
public interface IBridgeCollissionFull : IBridgeCollisionEnter,
	IBridgeCollisionStay, IBridgeCollisionExit { }
#endregion

#region Trigger
public interface IBridgeTriggerEnter {
	void OnTriggerEnter(Collider2D collision);
}
public interface IBridgeTriggerStay {
	void OnTriggerStay(Collider2D collision);
}
public interface IBridgeTriggerExit {
	void OnTriggerExit(Collider2D collision);
}
public interface IBridgeTriggerFull: IBridgeTriggerEnter,
	IBridgeTriggerStay, IBridgeTriggerExit { }
#endregion
