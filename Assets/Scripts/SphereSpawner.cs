using System;
using UnityEngine;
using UnityEngine.Pool;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Sphere _prefab;
    [SerializeField] private CubeRemover _cubeRemover;

    private ObjectPooler<Sphere> _pool;

    public event Action<Sphere> Spawned;

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
        Sphere sphere = _pool.GetObject(position);
        Spawned?.Invoke(sphere);
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

        foreach (var sphere in _pool.PooledObjects)
        {
            if (sphere.gameObject.activeSelf)
            {
                activeCount++;
            }
        }

        return activeCount;
    }
}