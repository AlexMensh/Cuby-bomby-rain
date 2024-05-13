using System.Collections;
using UnityEngine;

public class SphereRemover : MonoBehaviour
{
    [SerializeField] private SpherePooler _pool;
    [SerializeField] private int _minRemoveDelay;
    [SerializeField] private int _maxRemoveDelay;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Sphere sphere))
        {
            StartCoroutine(ExploeDelay(sphere));
        }
    }

    private IEnumerator ExploeDelay(Sphere sphere)
    {
        float randomValue = Random.Range(_minRemoveDelay, _maxRemoveDelay);
        WaitForSeconds explodeDelay = new WaitForSeconds(randomValue);

        sphere.StartFade(randomValue);

        yield return explodeDelay;

        sphere.Explode();
        _pool.ReleaseObject(sphere);
    }
}