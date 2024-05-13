using System;
using System.Collections.Generic;
using UnityEngine;

public class SpherePooler : MonoBehaviour
{
    [SerializeField] private Sphere _prefab;
    [SerializeField] private CubeRemover _cubeRemover;

    private List<Sphere> _pool;

    public event Action SphereSpawned;

    private void Awake()
    {
        _pool = new List<Sphere>();
    }

    private void OnEnable()
    {
        _cubeRemover.CubeRemoved += GetObject;
    }

    private void OnDisable()
    {
        _cubeRemover.CubeRemoved -= GetObject;
    }

    public void GetObject(Transform transform)
    {
        Sphere sphere = null;

        foreach (var item in _pool)
        {
            if (item.isActiveAndEnabled == false)
            {
                sphere = item;
                break;
            }
        }

        if (sphere == null)
        {
            sphere = CreateObject();
        }

        sphere.gameObject.transform.position = transform.position;
        sphere.gameObject.SetActive(true);

        SphereSpawned?.Invoke();
    }

    public void ReleaseObject(Sphere sphere)
    {
        sphere.gameObject.SetActive(false);
    }
    public int GetSpherePooledAmount()
    {
        return _pool.Count;
    }

    private Sphere CreateObject()
    {
        Sphere sphere = Instantiate(_prefab);

        _pool.Add(sphere);

        return sphere;
    }
}