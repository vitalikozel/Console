using UnityEngine;
using UnityEngine.Events;

public class EnterButton : KeyButton
{
    [SerializeField] private InputSymbolsToInputField _inputField;

    public event UnityAction<string> ClickEnterButtonWithCommand;

    private string oldCommand = ".cd.";

    protected override void WhatMustDoButton(KeyButton key)
    {
        CheckEnteringProgramCommand();
    }

    private void CheckEnteringProgramCommand()
    {
        if (_inputField.GetCurrectCommand() == "")
        {
            _inputField.SetText(oldCommand);
        }
        else
        {
            ClickEnterButtonWithCommand.Invoke(_inputField.GetCurrectCommand());
            oldCommand = _inputField.GetCurrectCommand();
            _inputField.ClearInputField();
        }
    }
}
