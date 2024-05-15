using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private CubeRemover _cubeRemover;
    [SerializeField] private SphereRemover _sphereRemover;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
        {
            _cubeRemover.ReleaseCube(cube);
        }

        else if (collision.collider.TryGetComponent(out Sphere sphere))
        {
            _sphereRemover.ReleaseSphere(sphere);
        }
    }
}