using System.Collections;
using UnityEngine;

public class SphereRemover : MonoBehaviour
{
    [SerializeField] private SphereSpawner _pool;
    [SerializeField] private int _sphereLifetimeMin;
    [SerializeField] private int _sphereLifetimeMax;

    private void OnEnable()
    {
        _pool.Spawned += ReleaseSphere;
    }

    private void OnDisable()
    {
        _pool.Spawned -= ReleaseSphere;
    }

    public void ReleaseSphere(Sphere sphere)
    {
        StartCoroutine(ReleaseCount(sphere));
    }

    private IEnumerator ReleaseCount(Sphere sphere)
    {
        int randomValue = Random.Range(_sphereLifetimeMin, _sphereLifetimeMax);
        WaitForSeconds wait = new WaitForSeconds(randomValue);

        sphere.StartFade(randomValue);

        yield return wait;

        sphere.Explode();

        _pool.ReleaseObject(sphere);
    }
}