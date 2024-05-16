using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour 
{
    private Renderer _renderer;
    private bool _isColorChanged = false;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _renderer = GetComponent<Renderer>();
        SetDefaultColor();
    }

    public void SetRandomColor()
    {
        Color randomColor = Random.ColorHSV();

        _renderer.material.color = randomColor;
        _isColorChanged = true;
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = Color.white;
        _isColorChanged = false;
    }

    public bool IsColorChanged()
    {
        return _isColorChanged;
    }
}