using System;
using System.Collections;
using UnityEngine;

public class CubeRemover : MonoBehaviour
{
    [SerializeField] private CubePooler _pool;
    [SerializeField] private int _cubeLifetimeMin;
    [SerializeField] private int _cubeLifetimeMax;

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
        int randomValue = UnityEngine.Random.Range(_cubeLifetimeMin, _cubeLifetimeMax);
        WaitForSeconds wait = new WaitForSeconds(randomValue);

        yield return wait;

        cube.ChangeStatus();
        cube.SetDefaultColor();

        CubeRemoved?.Invoke(cube.transform.position);
        _pool.ReleaseObject(cube);
    }
}