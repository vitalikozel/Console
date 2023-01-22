using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Backspacebutton : KeyButton
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Animator _cursor;

    private int _symbolIndex;

    protected override void WhatMustDoButton(KeyButton key)
    {
        _symbolIndex = _inputField.text.Length;

        if(_symbolIndex > 0)
        {
            _symbolIndex--;

            _inputField.text = _inputField.text.Remove(_symbolIndex);

            if(_symbolIndex <= 0)
            {
                _cursor.Play("Cursor");
            }
        }
        else
        {
            _cursor.Play("Cursor");
        }
    }
}
