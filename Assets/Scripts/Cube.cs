using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour 
{
    private Renderer _renderer;
    private bool _isColorChanged = false;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetDefaultColor();
    }

    public void SetRandomColor()
    {
        Color randomColor = Random.ColorHSV();

        _renderer.material.color = randomColor;
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = Color.white;
    }

    public bool IsColorChanged()
    {
        return _isColorChanged;
    }

    public void ChangeStatus()
    {
        _isColorChanged = !_isColorChanged;
    }
}