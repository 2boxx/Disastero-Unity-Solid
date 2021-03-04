using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FiringDataPackage {
	public BulletBehaviourBase type;
	public Vector2 position;
	public Vector2 direction;
}

public struct AsteroidDataPacakge
{
    public int size;
	public Asteroid asteroid;
    public Vector2 position;
    public Vector2 direction;
}

public struct CollisionDataPackage {
	public MonoBehaviour sender;
	public Collision2D collission;
}

public struct ShipDataPackage {
    public GameObject player;
}

public struct TriggerDataPackage {
	public MonoBehaviour sender;
	public Collider2D collider;
}

public struct ExplosionDataPackage {
	public Transform source;
	public Collider2D[] colliders;
}