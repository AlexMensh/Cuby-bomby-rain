using System;
using UnityEngine;

public class CubeRemover : MonoBehaviour
{
    [SerializeField] private CubePooler _pool;

    public event Action<Transform> CubeRemoved;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
        {
            _pool.ReleaseObject(cube);

            CubeRemoved?.Invoke(cube.transform);
        }
    }
}