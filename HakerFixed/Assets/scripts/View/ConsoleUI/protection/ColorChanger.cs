using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(ClickEnemy))]
public class ColorChanger : MonoBehaviour
{
    public Color[] _colors;


    private Image _renderer;
    private ClickEnemy _clickEnemy;
    private float _colorLifeTime = 0;
    private float _valueToSet = 0;
    private int _colorIndex = 0;

    private void Start()
    {
        _renderer = GetComponent<Image>();
        _clickEnemy = GetComponent<ClickEnemy>();
        _colorLifeTime = _clickEnemy.LifeTime / _colors.Length;
    }

    private void FixedUpdate()
    {
        ChangeColorDefault();
    }

    protected virtual void ChangeColorDefault()
    {
        _valueToSet += Time.fixedDeltaTime / _colorLifeTime;

        _renderer.color = Color.Lerp(_renderer.color, _colors[_colorIndex], _valueToSet);

        if (_valueToSet > 1)
        {
            _valueToSet = 0;
            if (_colorIndex + 1 < _colors.Length)
            {
                _colorIndex++;
            }
            else
            {
                _colorIndex = 0;
            }
        }
    }
}
