using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ComponentShipMovementPlayer : MonoBehaviour, IHasRigidbody {
	public float speed;
	public float thrust;
	public float turnRate;
	

	private IShipMovementBehaviour behaviour;
	private IShipMovementController controller;
	private Rigidbody2D thisRB;

	private void Awake() {
		thisRB = GetComponent<Rigidbody2D>();
		behaviour = new ShipMovementBehaviourRigidBody2D(thisRB, transform).SetSpeed(speed)
			.SetThrust(thrust).SetTurnRate(turnRate);
		controller = new ShipMovementControllerPlayer();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		controller.Control(behaviour);
	}

	public Rigidbody2D GetRigidbody() {
		return GetComponent<Rigidbody2D>();
	}

}
