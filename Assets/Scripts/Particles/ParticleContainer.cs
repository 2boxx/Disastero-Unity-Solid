using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleContainer : MonoBehaviour {
    public float lifeSpan;
    public List<ParticleSystem> particleChildren;
    public bool initialized = false;

    public void Awake()
    {
        particleChildren = new List<ParticleSystem>();
        foreach (Transform item in transform)
        {
            var i = item.GetComponent<ParticleSystem>();
            if (i != null) particleChildren.Add(i);
        }
        initialized = true;
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        foreach (var item in particleChildren)
        {
            item.time = 0;
            item.Play();
            var main = item.main;
            main.playOnAwake = true;
        }
    }

    public void Dispose()
    {
        foreach (var item in particleChildren)
        {
            item.Clear();
        }
        gameObject.SetActive(false);
    }

    public static void InitializePC(ParticleContainer asteroidObj)
    {
        asteroidObj.Initialize();
    }

    public static void DisposePC(ParticleContainer asteroidObj)
    {
        asteroidObj.Dispose();
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }
}
