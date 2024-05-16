using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> where T : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    private List<T> _pool = new List<T>();

    public IEnumerable<T> PooledObjects => _pool;

    public ObjectPooler(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;
    }

    public T GetObject(Vector3 position)
    {
        T item = null;

        foreach (var checkItem in _pool)
        {
            if (checkItem.isActiveAndEnabled == false)
            {
                item = checkItem;
                break;
            }
        }

        if (item == null)
        {
            item = CreateObject();
        }

        item.gameObject.transform.position = position;
        item.gameObject.SetActive(true);

        return item;
    }

    public void PutObject(T item)
    {
        item.gameObject.SetActive(false);
    }

    private T CreateObject()
    {
        T item = Object.Instantiate(_prefab);
        _pool.Add(item);
        item.gameObject.SetActive(false);
        item.transform.parent = _container.transform;

        return item;
    }
}