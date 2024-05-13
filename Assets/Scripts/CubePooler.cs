using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePooler : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _cubesToPoolAmount;
    [SerializeField] private float _spawnRadius;

    private List<Cube> _pool;

    private void Awake()
    {
        _pool = new List<Cube>();
    }

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    public void GetObject()
    {
        Cube cube = null;

        foreach (var item in _pool)
        {
            if (item.isActiveAndEnabled == false)
            {
                cube = item;
                break;
            }
        }

        if (cube == null)
        {
            cube = CreateObject();
        }

        cube.gameObject.transform.position = GetSpawnPosition();
        cube.gameObject.SetActive(true);
    }

    public void ReleaseObject(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    public int GetCubesPooledAmount()
    {
        return _pool.Count;
    }

    public int GetCubesToPool()
    {
        return _cubesToPoolAmount;
    }

    public int GetActiveCubesCount()
    {
        int activeCount = 0;
        foreach (var cube in _pool)
        {
            if (cube.gameObject.activeSelf)
            {
                activeCount++;
            }
        }
        return activeCount;
    }

    private Cube CreateObject()
    {
        Cube cube = Instantiate(_prefab, GetSpawnPosition(), Quaternion.identity);

        _pool.Add(cube);

        return cube;
    }

    private IEnumerator SpawnObjects()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);
        for (int i = 0; i < _cubesToPoolAmount; i++)
        {
            yield return wait;

            GetObject();
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = _container.transform.position + Random.insideUnitSphere * _spawnRadius;

        return spawnPosition;
    }
}