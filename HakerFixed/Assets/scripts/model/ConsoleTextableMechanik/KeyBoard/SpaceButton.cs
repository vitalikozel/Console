using UnityEngine;

public class SpaceButton : KeyButton
{
    [SerializeField] private InputSymbolsToInputField _inputField;

    protected override void WhatMustDoButton(KeyButton key)
    {
        _inputField.AddSymbol(' ');
    }
}
