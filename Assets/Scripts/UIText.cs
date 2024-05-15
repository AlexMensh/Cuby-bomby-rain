using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cubesText;
    [SerializeField] private TextMeshProUGUI _spheresText;
    [SerializeField] private TextMeshProUGUI _toPoolText;
    [SerializeField] private TextMeshProUGUI _activeObjectsText;
    [SerializeField] private CubePooler _cubePooler;
    [SerializeField] private SpherePooler _spherePooler;

    private void Start()
    {
        _cubesText.text = "0";
        _spheresText.text = "0";
        _activeObjectsText.text = "0";
        _toPoolText.text = _cubePooler.GetCubesToPool().ToString();
    }

    private void Update()
    {
        _activeObjectsText.text = (_cubePooler.GetActiveCubesCount() + _spherePooler.GetActiveSpheresCount()).ToString();
        _cubesText.text = _cubePooler.GetPooledCubesAmount().ToString();
        _spheresText.text = _spherePooler.GetPooledSphereAmount().ToString();
    }
}