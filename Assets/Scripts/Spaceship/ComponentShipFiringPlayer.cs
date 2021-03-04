using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentShipFiringPlayer : MonoBehaviour {

	[Header("General")]
	public Transform bulletSource;

	[Header("Normal bullet")]
	public float normalBulletInterval;
	public float normalBulletLifetime;
	public float normalBulletSpeed;

	[Header("Bomb bullet")]
	public float bombBulletInterval;
	public float bombBulletLifetime;
	public float bombBulletSpeed;
	public float bombBulletRadius;

	[Header("LaserBuller")]
	public float laserBulletRange;
    public float laserBulletWidth;

	private List<IShipFiringBehaviour> behaviourList = new List<IShipFiringBehaviour>();

	private IShipFiringBehaviour currentBehaviour; //comportamiento de disparo actual
	private IShipFiringController controller; //controlador de disparo actual
	private ShipFiringBehavioursManager behaviourManager; //se encarga de switchear el behaviour

	private void Awake() {
		//Disparo comun behaviour, construccion y builder
		behaviourList.Add(new ShipFiringBehaviourAutomatic(bulletSource)
			.SetBulletInterval(normalBulletInterval).SetBulletDuration(normalBulletLifetime)
			.SetBulletSpeed(normalBulletSpeed));
		//Disparo bomba behaviour, construccion y builder
		behaviourList.Add(new ShipFiringBehaviourBombAutomatic(bulletSource)
			.SetExplosionRadius(bombBulletRadius).SetBulletInterval(bombBulletInterval)
			.SetBulletDuration(bombBulletLifetime).SetBulletSpeed(bombBulletSpeed));
		//Disparo laser behaviour, construccion y builder
		//Marcos: Si extendes/modificas algun builder o construccion, acordate de ajustar aca.
		behaviourList.Add(new ShipFiringBehaviourLaser(bulletSource).SetLaserRange(laserBulletRange).SetLaserWidth(laserBulletWidth));

		behaviourManager = new ShipFiringBehavioursManager(this);
		currentBehaviour = behaviourList[0];
		controller = new ShipFiringControllerPlayer();
	}

	// Update is called once per frame
	void Update() {
		behaviourManager.Update();
		controller.Control(currentBehaviour);
	}

	public void SetBehaviour(int i) {
		if ((i < behaviourList.Count) && (behaviourList[i] != null))
			currentBehaviour = behaviourList[i];
	}
}
