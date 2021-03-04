using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementControllerPlayer : IShipMovementController {

	public void Control(IShipMovementBehaviour behaviour) {
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		if (x != 0)
		{
			if (x > 0) behaviour.TurnRight();
			else behaviour.TurnLeft();
		}
		if (y!=0)
		{
			if (y > 0) behaviour.MoveForward();
			else behaviour.MoveBackwards();
		}
	}


}
