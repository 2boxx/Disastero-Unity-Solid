using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFiringBehaviourLaser : IShipFiringBehaviour
{

    private float laserRange;
    private float laserWidth;
    private Transform transform;

    public ShipFiringBehaviourLaser(Transform transform, float range = 1, float width = 1)
    {
        laserRange = range;
        laserWidth = width;
        this.transform = transform;
    }

    #region Builder
    public ShipFiringBehaviourLaser SetLaserRange(float range)
    {
        laserRange = range;
        return this;
    }
    public ShipFiringBehaviourLaser SetLaserWidth(float width)
    {
        laserWidth = width;
        return this;
    }
    #endregion


    public void Fire()
    {
        //Marcos, podes cambiar esto pero pensaria que esto da lo necesario
        EventManager.CallEvent("FireBullet", new FiringDataPackage
        {
            direction = transform.up,
            position = transform.position,
            type = new BulletBehaviourLaser().SetLaserRange(laserRange).SetLaserWidth(laserWidth)
        });
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ShipFiringBehaviourLaser : IShipFiringBehaviour {

//	private float laserRange;
//	private Transform transform;

//	public ShipFiringBehaviourLaser(Transform transform, float range = 1) {
//		laserRange = range;
//		this.transform = transform;
//	}

//	#region Builder
//	public ShipFiringBehaviourLaser SetLaserRange(float range) {
//		laserRange = range;
//		return this;
//	}
//	#endregion

//	public void Fire() {
//		//Marcos, podes cambiar esto pero pensaria que esto da lo necesario
//		EventManager.CallEvent("FireBullet", new FiringDataPackage
//		{
//			direction = transform.up,
//			position = transform.position,
//			type = new BulletBehaviourLaser().SetLaserRange(laserRange)
//		});
//	}
//}
