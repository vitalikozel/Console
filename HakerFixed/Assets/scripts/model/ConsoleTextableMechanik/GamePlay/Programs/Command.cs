using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    private string _command;
    private string _discription;
    private bool _debug;

    public string CurrectCommand => _command;
    public string Discription => _discription;
    public bool Debug => _debug;

    public Command(string command, string discription = "", bool isDebugCommand = false)
    {
        _command = command.ToLower();
        _discription = discription;
        _debug = isDebugCommand;
    }

    protected string[] Flags =
    {
        "-v",
        "-rm",
        "-ch"
    };

    protected void SetOldFlags()
    {
        Flags = new string[]
        {
            "-v",
            "-rm",
            "-ch"
        };
    }

    protected virtual IEnumerator ActiveFromTimer()
    {
        yield return null;
    }

    public abstract int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer);
}
