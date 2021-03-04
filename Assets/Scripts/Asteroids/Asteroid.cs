using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour {
    public float speed;
    public float lifeSpan;

    public static Collider2D[] boundaries;
    private Rigidbody2D rb;
    public int size;
    private SpriteRenderer myRenderer;
    public Sprite[] sprites;

    public const int large = 2;
    public const int medium = 1;
    public const int small = 0;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        SetRandomSprite();
    }

    public void Dispose()
    {
        rb.velocity = Vector2.zero;
		gameObject.SetActive(false);
	}

    public static void InitializeAsteroid(Asteroid asteroidObj)
    {
        asteroidObj.Initialize();
    }

    public static void DisposeAsteroid(Asteroid asteroidObj)
    {
        asteroidObj.Dispose();
	}

    public void SetRandomDirectionAndSpeed(float minSpeed, float maxSpeed)
    {
        float angle = UnityEngine.Random.Range(0, 360);
        Vector2 randDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.up;
        float randSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        rb.velocity = randDirection * randSpeed;
    }

    public void SetSize(int newSize)
    {
        size = newSize;

        switch (size)
        {
            case Asteroid.large:
                transform.localScale = Vector3.one * 1f;
                break;
            case Asteroid.medium:
                transform.localScale = Vector3.one * 0.618f;
                break;
            case Asteroid.small:
                transform.localScale = Vector3.one * 0.381f;
                break;
            default:
                break;
        }
    }

    public void DestroyAsteroid()
    {
		EventManager.CallEvent("AsteroidDestroyed",
			new AsteroidDataPacakge { size = size,
				direction = rb.velocity, position = transform.position, asteroid = this });
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetRandomSprite()
    {
        myRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
    }

	private void OnEnable() {
		EventManager.Subscribe("CollisionEnter", CheckCollision);
		EventManager.Subscribe("BombExplosion", CheckExplosion); 
        EventManager.Subscribe("LaserStrike", CheckExplosion);
    }

	private void OnDisable() {
		EventManager.Unsubscribe("CollisionEnter", CheckCollision);
		EventManager.Unsubscribe("BombExplosion", CheckExplosion);
        EventManager.Unsubscribe("LaserStrike", CheckExplosion);
    }

	private void CheckCollision(params object[] parameters) {
		if (!(parameters[0] is CollisionDataPackage)) return;
		var data = (CollisionDataPackage)parameters[0];
		if (data.collission.collider.transform != transform &&
			data.collission.otherCollider.transform != transform) return;
		DestroyAsteroid();
	}

	private void CheckExplosion(params object[] parameters) {
		if (!(parameters[0] is ExplosionDataPackage)) return;
		var data = (ExplosionDataPackage)parameters[0];
		foreach (var item in data.colliders)
		{
			if(item.transform == transform)
			{
				DestroyAsteroid();
				return;
			}
		}
	}
}
