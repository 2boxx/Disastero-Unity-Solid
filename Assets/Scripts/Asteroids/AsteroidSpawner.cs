using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
	private GameObject asteroidPrefab;
    private Pool<Asteroid> asteroidPool;
    /// <summary>
    /// Screen boundaries for spawning new asteroids
    /// </summary>
    public List<Boundary> boundaries;

    [Header("Pool Settings")]
    public int initialStock;
    public bool isDyanmic;

    [Header("Gameplay Settings")]
    public int initialAsteroids;
    /// <summary>
    /// How many small asteroids need to be destroyed for a new big asteroid to spawn.
    /// </summary>
    public int smallAsteroidsForNextSpawn;
    private int smallAsteroidCounter;
    /// <summary>
    /// How fast are smaller fragments in relationship to their parent (default = 1.3)
    /// </summary>
    public float fragmentVelocityRatio;

    [Header("Randomized speed parameters for new asteroids")]
    public float minSpeed;
    public float maxSpeed;

    private void Awake()
    {
		asteroidPrefab = Resources.Load<GameObject>("Prefabs/Asteroid");
		smallAsteroidCounter = 0;
        asteroidPool = new Pool<Asteroid>(initialStock, GetAsteroid, Asteroid.InitializeAsteroid, Asteroid.DisposeAsteroid, isDyanmic);
    }

    private void Start()
    {
        for (int i = 0; i < initialAsteroids; i++)
        {
            SpawnNewAsteroid();
        }
    }

   /// <summary>
   /// SPAWN BRAND NEW WHOLE SIZED ASTEROID FROM THE EDGES
   /// </summary>
    public void SpawnNewAsteroid()
    {
        Asteroid newAsteroid = asteroidPool.GetPoolObject().GetObj;
        newAsteroid.transform.position = GetNewSpawnPosition();
        newAsteroid.SetRandomDirectionAndSpeed(minSpeed, maxSpeed);
        newAsteroid.SetSize(Asteroid.large);
    }

    /// <summary>
    /// Create new child asteroids via parameters
    /// </summary>
    /// <param name="position">New asteroid Position</param>
    /// <param name="size">New asteroid size </param>
    /// <param name="velocity">New asteroid velocity</param>
    public void CreateAsteroid (Vector2 position, int size, Vector2 velocity)
    {
        Asteroid newAsteroid = asteroidPool.GetPoolObject().GetObj;
        newAsteroid.transform.position = position;
        newAsteroid.SetVelocity(velocity);
        newAsteroid.SetSize(size);
    }
    /// <summary>
    /// Create smaller children fragments based on destroyed father and change trajectory
    /// </summary>
    /// <param name="position">parent position</param>
    /// <param name="prevSize">parent size</param>
    /// <param name="previousVelocity">parent velocity</param>
    public void CreateChildrenAsteroids(Vector2 position, int prevSize, Vector2 previousVelocity)
    {
        Vector2 velocityA = Quaternion.AngleAxis(45, Vector3.forward) * previousVelocity * fragmentVelocityRatio;
        Vector2 velocityB = Quaternion.AngleAxis(-45, Vector3.forward) * previousVelocity * fragmentVelocityRatio;

        CreateAsteroid(position, prevSize-1, velocityA);
        CreateAsteroid(position, prevSize-1, velocityB);
    }

    public void OnDestroyedAsteroid(params object[] parameters)
    {
        if (parameters[0] is AsteroidDataPacakge)
        {
            AsteroidDataPacakge ap = (AsteroidDataPacakge)parameters[0];
			asteroidPool.DisablePoolObject(ap.asteroid);
            if (ap.size > 0) //IF ASTEROID IS NOT THE SMALLEST, DIVIDE IT
            {
                CreateChildrenAsteroids(ap.position, ap.size, ap.direction);
            }
            else
            {
                //IF THE ASTEROID IS THE SMALLEST, ADD 1
                smallAsteroidCounter++;
                if (smallAsteroidCounter >= smallAsteroidsForNextSpawn)
                {
                    SpawnNewAsteroid();
                    smallAsteroidCounter = 0;
                }
                return;
            }
            
        }
    }

    public void ReturnAsteroidToPool(Asteroid asteroid)
    {
        asteroidPool.DisablePoolObject(asteroid);
    }

    public Vector2 GetNewSpawnPosition()
    {
        int index = Random.Range(0, boundaries.Count);
        BoxCollider2D box = boundaries[index].GetComponent<BoxCollider2D>();
        float x = Random.Range(box.bounds.min.x, box.bounds.max.x);
        float y = Random.Range(box.bounds.min.y, box.bounds.max.y);
        return new Vector2(x, y);
    }

	private void OnEnable() {
		EventManager.Subscribe("AsteroidDestroyed", OnDestroyedAsteroid);
	}

	private void OnDisable() {
		EventManager.Unsubscribe("AsteroidDestroyed", OnDestroyedAsteroid);
	}

	private Asteroid GetAsteroid() {
		Asteroid ast = Instantiate(asteroidPrefab).GetComponent<Asteroid>();
		return ast;
	}
}
