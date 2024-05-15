using System;
using System.Collections;
using UnityEngine;

public class CubeRemover : MonoBehaviour
{
    [SerializeField] private CubePooler _pool;
    [SerializeField] private float _cubeLifetimeMin;
    [SerializeField] private float _cubeLifetimeMax;

    public event Action<Vector3> CubeRemoved;

    public void ReleaseCube(Cube cube)
    {
        if (cube.IsColorChanged() == false)
        {
            cube.SetRandomColor();
            cube.ChangeStatus();

            StartCoroutine(ReleaseCount(cube));
        }
    }

    private IEnumerator ReleaseCount(Cube cube)
    {
        WaitForSeconds destroyDelay = new WaitForSeconds(UnityEngine.Random.Range(_cubeLifetimeMin, _cubeLifetimeMax));

        yield return destroyDelay;

        cube.ChangeStatus();
        cube.SetDefaultColor();

        _pool.ReleaseObject(cube);
        CubeRemoved?.Invoke(cube.transform.position);
    }
}