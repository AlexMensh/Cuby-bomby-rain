using System;
using System.Collections.Generic;
using UnityEngine;

public class SpherePooler : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Sphere _prefab;
    [SerializeField] private CubeRemover _cubeRemover;

    private ObjectPooler<Sphere> _pool;

    private void Awake()
    {
        _pool = new ObjectPooler<Sphere>(_prefab, _container);
    }

    private void OnEnable()
    {
        _cubeRemover.CubeRemoved += GetObject;
    }

    private void OnDisable()
    {
        _cubeRemover.CubeRemoved -= GetObject;
    }

    public void GetObject(Vector3 position)
    {
        _pool.GetObject(position);
    }

    public void ReleaseObject(Sphere sphere)
    {
        _pool.PutObject(sphere);
    }
    public int GetPooledSphereAmount()
    {
        int pooledSpheres = 0;
        foreach (var sphere in _pool.PooledObjects)
        {
            pooledSpheres++;
        }
        return pooledSpheres;
    }

    public int GetActiveSpheresCount()
    {
        int activeCount = 0;
        foreach (var cube in _pool.PooledObjects)
        {
            if (cube.gameObject.activeSelf)
            {
                activeCount++;
            }
        }
        return activeCount;
    }
}