using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMechanikGroup : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _controllerGroup;

    public void StartGame()
    {
        _controllerGroup.SetActive(true);
        _animator.Play("Start");
    }

    public void FinishGame()
    {
        _animator.Play("Finish");
    }
}
