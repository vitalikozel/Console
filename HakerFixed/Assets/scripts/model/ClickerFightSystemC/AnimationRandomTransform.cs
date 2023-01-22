using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationRandomTransform : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _move;

    private Vector2 _randomVector;

    public void StartMotion()
    {
        transform.localPosition = Vector2.zero;

        int randomX = Random.Range(0, 100) >= 50 ? Random.Range(3, 5) : -Random.Range(3, 5);
        int randomY = Random.Range(0, 100) >= 50 ? Random.Range(3, 5) : -Random.Range(3, 5);

        _randomVector = new Vector2(randomX, randomY);
        _animator.Play("textFadeGiveDamagePlayer", 0, 0.02f);
        _move = true;
    }

    public void StopMotion()
    {
        _move = false;
    }

    private void Update()
    {
        if (!_move) return;

        transform.Translate(_randomVector * Time.deltaTime);
    }
}
