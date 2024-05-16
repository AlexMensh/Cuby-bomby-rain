using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _cubesToPoolAmount;
    [SerializeField] private float _spawnRadius;

    private ObjectPooler<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPooler<Cube>(_prefab, _container);
    }

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    public void GetObject()
    {
        _pool.GetObject(GetSpawnPosition());
    }

    public void ReleaseObject(Cube cube)
    {
        _pool.PutObject(cube);
    }

    public int GetCubesToPool()
    {
        return _cubesToPoolAmount;
    }

    public int GetPooledCubesAmount()
    {
        int pooledCubes = 0;

        foreach (var cube in _pool.PooledObjects)
        {
            pooledCubes++;
        }

        return pooledCubes;
    }

    public int GetActiveCubesCount()
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