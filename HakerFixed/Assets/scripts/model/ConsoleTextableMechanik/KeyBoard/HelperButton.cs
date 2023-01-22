using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelperButton : KeyButton
{
    [SerializeField] private conclusionViewCommnd _view;
    [SerializeField] private TMP_InputField _inputField;

    private int previusCountSybols = 0;
    private string _command;
    private int procent;

    protected override void WhatMustDoButton(KeyButton key)
    {
        InputCurrectCommand();
    }

    private void InputCurrectCommand()
    {
        string[] commands = _view.Program.commandsToString().ToArray();

        string input = _inputField.text;
        int countSymbols = 0;

        for (int i = 0; i < commands.Length; i++)
        {
            string command = commands[i];
            string commandToInput = input;

            for (int j = 0; j < command.Length; j++)
            {
                for (int k = 0; k < commandToInput.Length; k++)
                {
                    if (commandToInput[k] == command[j])
                    {
                        countSymbols++;
                        commandToInput = commandToInput.Remove(k, 1);
                        break;
                    }
                }
            }

            if (countSymbols >= previusCountSybols)
            {
                previusCountSybols = countSymbols;
                _command = command;
            }

            countSymbols = 0;
        }

        _inputField.text = _command;
        previusCountSybols = 0;
    }
}
