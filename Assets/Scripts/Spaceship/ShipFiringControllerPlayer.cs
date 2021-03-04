using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFiringControllerPlayer : IShipFiringController {

	public void Control(IShipFiringBehaviour behaviour) {
		if (Input.GetButton("Fire1")) behaviour.Fire();
	}
}
