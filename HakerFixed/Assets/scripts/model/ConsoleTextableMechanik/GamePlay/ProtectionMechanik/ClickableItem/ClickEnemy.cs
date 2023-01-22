using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEnemy : MonoBehaviour, IPointerDownHandler
{
    public float LifeTime = 5;

    public float _TimeBetwin = 5;
    protected bool _CurrectClick = false;

    private bool _isFinished = false;

    public event UnityAction<bool, ClickEnemy> Finish;

    private void Start()
    {
        _TimeBetwin = LifeTime;
    }

    private void FixedUpdate()
    {
        if(!_isFinished)
            FinishedTime();
    }

    private void FinishEvent()
    {
        Finish?.Invoke(_CurrectClick, this);
    }


    private void FinishedTime()
    {
        if (_TimeBetwin <= 0)
        {
            _isFinished = true;
            FinishEvent();
        }
        else
        {
            _TimeBetwin -= Time.fixedDeltaTime;
        }
    }

    protected virtual void CheckCurrectClick()
    {
        _CurrectClick = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CheckCurrectClick();
        FinishEvent();
    }
}
