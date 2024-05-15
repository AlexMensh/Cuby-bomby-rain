using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Sphere : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Renderer _renderer;
    private float visibleAlpha = 1f;
    private float invisibleAlpha = 0f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void StartFade(float fadeDelay)
    {
        StartCoroutine(Fade(fadeDelay));
    }

    public void Explode()
    {
        foreach (Rigidbody explodingObject in GetExplodingObjects())
        {
            explodingObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private IEnumerator Fade(float fadeDelay)
    {
        float timerValue = 0f;
        Color newColor = _renderer.material.color;

        while (timerValue < fadeDelay)
        {
            newColor.a = Mathf.Lerp(visibleAlpha, invisibleAlpha, timerValue / fadeDelay);

            _renderer.material.color = newColor;

            timerValue += Time.deltaTime;

            yield return null;
        }
    }

    private List<Rigidbody> GetExplodingObjects()
    {
        Collider[] detectedItems = Physics.OverlapSphere(transform.position, _explosionRadius);
        var detectedItemsRigidbodys = new List<Rigidbody>();

        foreach (Collider item in detectedItems)
        {
            Rigidbody attachedRigidbody = item.attachedRigidbody;

            if (attachedRigidbody != null)
            {
                detectedItemsRigidbodys.Add(attachedRigidbody);
            }
        }

        return detectedItemsRigidbodys;
    }
}