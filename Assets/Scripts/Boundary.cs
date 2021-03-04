using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Boundary : MonoBehaviour {

	public Boundary linkedBoundary;

	[HideInInspector]
	private Vector2 boundaryDirection; //The direciton objects are moving to when reaching this boundary
	public Vector2 BoundaryDirection { get { return boundaryDirection; } }

	private Vector2 boundariesDeltaPos;

	private void Start() {
		boundaryDirection = ((Vector2)this.transform.position - Vector2.zero).normalized;
		boundariesDeltaPos = linkedBoundary.transform.position - transform.position;
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.GetComponent<Rigidbody2D>() != null)
		{
			Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
			if (Vector2.Dot(rb.velocity.normalized, BoundaryDirection) > 0)
			{
				Warp(other.transform);
			}
		}
	}

	private void Warp(Transform target) {
		target.position = target.position + (Vector3)boundariesDeltaPos;
	}
}
