using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KeyButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] KeyButtonRender render;

    private char _keySymbol = 'q';

    public char KeySymbol => _keySymbol;

    public event UnityAction<KeyButton> Click;

    private void OnEnable()
    {
        Click += WhatMustDoButton;
    }

    private void OnDisable()
    {
        Click -= WhatMustDoButton;
    }

    public void SetCurrentSymbol(char symbol)
    {
        _keySymbol = symbol;
    }

    public void Init()
    {
        render.RenderCurrentSymbol(_keySymbol);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Click.Invoke(this);
    }

    protected virtual void WhatMustDoButton(KeyButton key)
    {

    }
}