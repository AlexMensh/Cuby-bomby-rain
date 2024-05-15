using System.Collections;
using UnityEngine;

public class SphereRemover : MonoBehaviour
{
    [SerializeField] private SpherePooler _pool;
    [SerializeField] private int _minRemoveDelay;
    [SerializeField] private int _maxRemoveDelay;

    public void ReleaseSphere(Sphere sphere)
    {
        StartCoroutine(ReleaseCount(sphere));
    }

    private IEnumerator ReleaseCount(Sphere sphere)
    {
        float randomValue = Random.Range(_minRemoveDelay, _maxRemoveDelay);
        WaitForSeconds explodeDelay = new WaitForSeconds(randomValue);

        sphere.StartFade(randomValue);

        yield return explodeDelay;

        sphere.Explode();
        _pool.ReleaseObject(sphere);
    }
}