using System;
using System.Collections;
using UnityEngine;

public class CubeRemover : MonoBehaviour
{
    [SerializeField] private CubePooler _pool;
    [SerializeField] private float _cubeLifetimeMin;
    [SerializeField] private float _cubeLifetimeMax;

    public event Action<Transform> CubeRemoved;

    public void ReleaseCube(Cube cube)
    {
        if (cube.IsColorChanged() == false)
        {
            cube.SetRandomColor();
            cube.ChangeStatus();

            StartCoroutine(LifetimeCount(cube));
        }
    }

    private IEnumerator LifetimeCount(Cube cube)
    {
        WaitForSeconds destroyDelay = new WaitForSeconds(UnityEngine.Random.Range(_cubeLifetimeMin, _cubeLifetimeMax));
        
        yield return destroyDelay;

        cube.ChangeStatus();
        cube.SetDefaultColor();

        _pool.ReleaseObject(cube);
        CubeRemoved?.Invoke(cube.transform);
    }
}