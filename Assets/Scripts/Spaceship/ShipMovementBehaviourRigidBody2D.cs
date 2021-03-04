using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementBehaviourRigidBody2D : IShipMovementBehaviour {

	private float thrust;
	private float maxSpeed;
	private float turnRate;
	private Rigidbody2D thisRB;
	private Transform transform;

	public ShipMovementBehaviourRigidBody2D(Rigidbody2D rb, Transform transform, float thrust = 1, float maxSpeed = 1, float turnRate = 45) {
		thisRB = rb;
		this.transform = transform;
		this.maxSpeed = maxSpeed;
		this.turnRate = turnRate;
	}

	public void MoveForward() {
		thisRB.AddForce(transform.up * thrust);
		if (thisRB.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
			thisRB.velocity = (thisRB.velocity.normalized * maxSpeed);
	}

	public void MoveBackwards() {
		thisRB.AddForce(-transform.up * thrust);
		if (thisRB.velocity.sqrMagnitude > (maxSpeed * maxSpeed))
			thisRB.velocity = (thisRB.velocity.normalized * maxSpeed);
	}

	public void TurnLeft() {
		transform.Rotate(0, 0, (turnRate * Time.deltaTime));
	}

	public void TurnRight() {
		transform.Rotate(0, 0, (-turnRate * Time.deltaTime));
	}

	#region Builder
	public ShipMovementBehaviourRigidBody2D SetThrust(float thrust) {
		this.thrust = thrust;
		return this;
	}

	public ShipMovementBehaviourRigidBody2D SetSpeed(float maxSpeed) {
		this.maxSpeed = maxSpeed;
		return this;
	}

	public ShipMovementBehaviourRigidBody2D SetTurnRate(float turnRate) {
		this.turnRate = turnRate;
		return this;
	}
	#endregion Builder
}
