using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private CubeRemover _cubeRemover;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
        {
            _cubeRemover.ReleaseCube(cube);
        }
    }
}