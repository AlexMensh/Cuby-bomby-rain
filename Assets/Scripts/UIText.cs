using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cubesText;
    [SerializeField] private TextMeshProUGUI _spheresText;
    [SerializeField] private TextMeshProUGUI _activeCubesText;
    [SerializeField] private TextMeshProUGUI _activeSpheresText;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private SphereSpawner _sphereSpawner;

    private void Start()
    {
        _cubesText.text = "0";
        _spheresText.text = "0";
        _activeCubesText.text = "0";
        _activeSpheresText.text = "0";
    }

    private void Update()
    {
        _cubesText.text = _cubeSpawner.GetPooledCubesAmount().ToString();
        _spheresText.text = _sphereSpawner.GetPooledSphereAmount().ToString();
        _activeCubesText.text = _cubeSpawner.GetActiveCubesCount().ToString();
        _activeSpheresText.text = _sphereSpawner.GetActiveSpheresCount().ToString();
    }
}