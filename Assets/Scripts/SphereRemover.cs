using System.Collections;
using UnityEngine;

public class SphereRemover : MonoBehaviour
{
    [SerializeField] private SpherePooler _pool;
    [SerializeField] private int _sphereLifetimeMin;
    [SerializeField] private int _sphereLifetimeMax;

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