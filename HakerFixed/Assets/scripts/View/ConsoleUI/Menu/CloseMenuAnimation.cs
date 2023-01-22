using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuAnimation : MonoBehaviour
{
    [SerializeField] private conclusionViewCommnd _view;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private RectTransform _objectToTransform;
    [SerializeField] private float _speed;
    [SerializeField] private int _offsetOpenLeft = 700;

    private bool _blockAnimation;

    public void Open()
    {
        _view.ConclusionText("open");

        if (!_blockAnimation)
        {
            StartCoroutine(_transformEnable());
        }
    }

    public void Close()
    {
        _view.ConclusionText("close");

        if (!_blockAnimation)
        {
            StartCoroutine(_transformDisable());
        }
    }

    private IEnumerator _transformDisable()
    {
        _blockAnimation = true;

        for (float i = 0; _objectToTransform.offsetMin.x != -_offsetOpenLeft; i += Time.deltaTime / 3)
        {
            _objectToTransform.SetLeft(-Mathf.Lerp(0, _offsetOpenLeft, _curve.Evaluate(i) * _speed));
            _objectToTransform.SetRight(Mathf.Lerp(0, _offsetOpenLeft, _curve.Evaluate(i) * _speed));

            yield return null;
        }

        _blockAnimation = false;
    }

    private IEnumerator _transformEnable()
    {
        _blockAnimation = true;

        for (float i = 0; _objectToTransform.offsetMin.x != 0; i += Time.deltaTime / 3)
        {
            _objectToTransform.SetLeft(-Mathf.Lerp(_offsetOpenLeft, 0, _curve.Evaluate(i) * _speed));
            _objectToTransform.SetRight(Mathf.Lerp(_offsetOpenLeft, 0, _curve.Evaluate(i) * _speed));

            yield return null;
        }

        _blockAnimation = false;
    }
}
