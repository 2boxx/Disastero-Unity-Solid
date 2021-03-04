using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFiringBehavioursManager {

	private ComponentShipFiringPlayer user;

	public ShipFiringBehavioursManager(ComponentShipFiringPlayer user) {
		this.user = user;
	}

	public void Update() {
		if (Input.anyKeyDown)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1)) user.SetBehaviour(0);
			else if (Input.GetKeyDown(KeyCode.Alpha2)) user.SetBehaviour(1);
			else if(Input.GetKeyDown(KeyCode.Alpha3)) user.SetBehaviour(2);
		}
	}
}
