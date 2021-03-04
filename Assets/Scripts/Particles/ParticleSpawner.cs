using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {
    private ParticleContainer explosionPrefab;
    private Pool<ParticleContainer> particlePool;

    [Header("Pool Settings")]
    public int initialStock;
    public bool isDyanmic;

    private void Awake()
    {
        explosionPrefab = Resources.Load<ParticleContainer>("Prefabs/Explosion");
        particlePool = new Pool<ParticleContainer>(initialStock, ParticleFactory, ParticleContainer.InitializePC, ParticleContainer.DisposePC, isDyanmic);
    }

    private void OnEnable()
    {
        EventManager.Subscribe("AsteroidDestroyed", SpawnExplosionFX);
        EventManager.Subscribe("LostLife", SpawnExplosionFX);
        EventManager.Subscribe("BombExplosion", SpawnExplosionFX);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("AsteroidDestroyed", SpawnExplosionFX);
        EventManager.Unsubscribe("LostLife", SpawnExplosionFX);
        EventManager.Unsubscribe("BombExplosion", SpawnExplosionFX);
    }

    private void SpawnExplosionFX(params object[] parameters)
    {
        if (parameters[0] is AsteroidDataPacakge)
        {
            AsteroidDataPacakge ap = (AsteroidDataPacakge)parameters[0];
            var pc = particlePool.GetPoolObject();
            if (!pc.GetObj.initialized)
            {
                pc.GetObj.Awake();
            }
            pc.GetObj.SetPosition(ap.position);
            //Instantiate(explosionPrefab, ap.position, Quaternion.identity);
        }
        if (parameters[0] is ShipDataPackage)
        {
            ShipDataPackage sp = (ShipDataPackage)parameters[0];
            var pc = particlePool.GetPoolObject();
            if (!pc.GetObj.initialized)
            {
                pc.GetObj.Awake();
            }
            pc.GetObj.SetPosition(sp.player.transform.position);
            //Instantiate(explosionPrefab, ap.position, Quaternion.identity);
        }
        if (parameters[0] is ExplosionDataPackage)
        {
            ExplosionDataPackage ep = (ExplosionDataPackage)parameters[0];
            var pc = particlePool.GetPoolObject();
            if (!pc.GetObj.initialized)
            {
                pc.GetObj.Awake();
            }
            pc.GetObj.SetPosition(ep.source.position);
            //Instantiate(explosionPrefab, ap.position, Quaternion.identity);
        }
    }

    private ParticleContainer ParticleFactory()
    {
        return Instantiate(explosionPrefab);
    }
}
