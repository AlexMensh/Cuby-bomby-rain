using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private bool _isAutoExpanded;
    [SerializeField] private Transform _container;

    private List<T> _pool;

    public ObjectPooler(T prefab, int itemsAmount, bool isAutoExpanded, Transform container)
    {
        _prefab = prefab;
        _container = container;
        _isAutoExpanded = isAutoExpanded;

        CreatePool(itemsAmount);
    }

    public void CreatePool(int itemsCount)
    {
        _pool = new List<T>();

        for (int i = 0; i < itemsCount; i++)
        {
            CreateObject();
        }
    }

    public T GetObject(Vector3 position)
    {
        if (isCanGetObject(out T item))
        {
            item.transform.position = position;
            _pool.Remove(item);
            return item;
        }

        if (_isAutoExpanded)
        {
            return CreateObject();
        }

        throw new System.Exception($"No items to pool {typeof(T)}. Turn ON AutoExpand.");
    }

    public void PutObject(T item)
    {
        _pool.Add(item);
        item.gameObject.SetActive(false);
    }

    private bool isCanGetObject(out T item)
    {
        foreach (var checkItem in _pool)
        {
            if (checkItem.isActiveAndEnabled == false)
            {
                item = checkItem;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        item = null;
        return false;
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var item = Object.Instantiate(_prefab);

        item.gameObject.SetActive(isActiveByDefault);
        item.transform.parent = _container.transform;
        _pool.Add(item);

        return item;
    }
}