using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IpLogger : Program
{
    [SerializeField] private PlayerData _playerData;

    private Command[] _commands;
    private List<MashineToBreakProtection> _mashines = new List<MashineToBreakProtection>();

    private void Start()
    {
        _commands = new Command[]
    {
            new Search(".search", _playerData, _mashines, " - found mashines to hack"),
        new LookIpMashine(".ip.", _mashines, "<number> - show ip mashine with this number"),
        new LookPersonalInformation(".info.", _mashines, "<number> - Shows the user information found on the computer most correctly."),
        new LookHistoryInformation(".history.", _mashines, "<number> - show history browser"),
        new LookStatus(".status.", _mashines, "<number> - show status mashine"),
        new ClearAllConnectionMashine(".clear.all.mashine", " - clear all mashine"),
        new ClearAllMashine(".clear.all.mashine.connection", "", true)
    };
    }

    public override void TransformOnThisAplication()
    {
        SetCurrectCommandsAplication(_commands);
    }
}

public class Search : Command
{
    private PlayerData _playerData;
    private List<MashineToBreakProtection> _mashinesForViewInformation;

    public Search(string command, PlayerData data, List<MashineToBreakProtection> mashines, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
        _mashinesForViewInformation = mashines;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        GlobalDataAndTimer.StartWorkTask(0.6f, Random.Range(5, 8), "Search currect mashine", true);

        int elementCount = Random.Range(1, 4);

        for (int i = 0; i < elementCount; i++)
        {
            MashineToBreakProtection mashineProtection = new MashineToBreakProtection();
            mashineProtection.GenerateDefaultRandomParametrs(_playerData);
            GlobalDataAndTimer.MashinesToHack.Add(mashineProtection);
            _mashinesForViewInformation.Add(mashineProtection);
        }

        viewToResultCommand.ConclusionText($"{_mashinesForViewInformation.Count} count mashines!");

        return 0;
    }
}

public class LookIpMashine : Command
{
    private List<MashineToBreakProtection> _mashinesToViewInfo;

    public LookIpMashine(string command, List<MashineToBreakProtection> mashines, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mashinesToViewInfo = mashines;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (argument == string.Empty)
            return 2;

        int index = Mathf.Abs(System.Convert.ToInt32(argument)) - 1;
        
        if (index < _mashinesToViewInfo.Count)
        {
            if (_mashinesToViewInfo[index].ProtectionLevel <= 2)
            {
                viewToResultCommand.ConclusionText($"ip mashine: {_mashinesToViewInfo[index].ip}");
            }
            else
            {
                viewToResultCommand.ConclusionText($"The protection level must be < 2 to view ip adress but is: {_mashinesToViewInfo[index].ProtectionLevel}");
                return 2;
            }
        }

        return 0;
    }
}

public class LookPersonalInformation : Command
{
    private List<MashineToBreakProtection> _mashinesToViewInfo;
    public LookPersonalInformation(string command, List<MashineToBreakProtection> mashines, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mashinesToViewInfo = mashines;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (argument == string.Empty)
            return 2;

        int index = Mathf.Abs(System.Convert.ToInt32(argument)) - 1;

        if (index < _mashinesToViewInfo.Count)
        {
            viewToResultCommand.ConclusionText($"NameLoginSystemOs: <color=#60D6D6>{_mashinesToViewInfo[index].NameLoginSystemOs}</color>, second name: <color=#60D6D6>{_mashinesToViewInfo[index].SecondNameLoginSystemOS}</color>");

            //GlobalDataAndTimer.CurrentConnectionMashine = GlobalDataAndTimer.MashinesToHack[index];
            //GlobalDataAndTimer.Attack.StartEnteringCommandAttackWithCurrectIndex(2);
            //START ENTER COMMANDS TO GET INFORMATION
        }
        else
        {
            viewToResultCommand.ConclusionText($"The protection level is too high {_mashinesToViewInfo[index].ProtectionLevel}");
            return 2;
        }

        return 0;
    }
}

public class LookHistoryInformation : Command
{
    private List<MashineToBreakProtection> _mashinesToViewInfo;
    public LookHistoryInformation(string command, List<MashineToBreakProtection> mashines, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mashinesToViewInfo = mashines;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (argument == string.Empty)
            return 2;

        int index = Mathf.Abs(System.Convert.ToInt32(argument)) - 1;

        if (index < _mashinesToViewInfo.Count)
            if (_mashinesToViewInfo[index].ProtectionLevel <= 2)
            { 
                viewToResultCommand.ConclusionText($"History browser: You can found in file system current mashine");
            }
            else
            {
                viewToResultCommand.ConclusionText($"The protection level is too high {_mashinesToViewInfo[index].ProtectionLevel}");
                return 2;
            }

        return 0;
    }
}

public class LookStatus : Command
{
    private List<MashineToBreakProtection> _mashinesToViewInfo;
    public LookStatus(string command, List<MashineToBreakProtection> mashines, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mashinesToViewInfo = mashines;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (argument == string.Empty)
            return 2;

        int index = Mathf.Abs(System.Convert.ToInt32(argument)) - 1;

        if (index < _mashinesToViewInfo.Count)
            if (_mashinesToViewInfo[index].ProtectionLevel <= 2)
            { 
                viewToResultCommand.ConclusionText($"StatusInChrome: {_mashinesToViewInfo[index].StatusInChrome}");
            }
            else
            {
                viewToResultCommand.ConclusionText($"The protection level is too high {_mashinesToViewInfo[index].ProtectionLevel}");
                return 2;
            }

        return 0;
    }
}
public class ShowMoneyBtc : Command
{
    public ShowMoneyBtc(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (argument == string.Empty)
            return 2;

        int index = Mathf.Abs(System.Convert.ToInt32(argument)) - 1;

        if (index < GlobalDataAndTimer.MashinesToHack.Count)
            if (GlobalDataAndTimer.MashinesToHack[index].ProtectionLevel <= 2)
            { 
                viewToResultCommand.ConclusionText($"Estimated Income: {GlobalDataAndTimer.MashinesToHack[index].MoneyWhosWasAddedToPlayer}");
            }
            else
            {
                viewToResultCommand.ConclusionText($"The protection level is too high {GlobalDataAndTimer.MashinesToHack[index].ProtectionLevel}");
                return 2;
            }

        return 0;
    }
}

public class ClearAllConnectionMashine : Command
{
    public ClearAllConnectionMashine(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        viewToResultCommand.ConclusionText("Do you want clear all connection mashine ? <color=red>This may cause the game to not work properly!</color> \nIf you are sure about this then enter the command: .clear.all.mashine.connection");
        return 0;
    }
}

public class ClearAllMashine : Command
{
    public ClearAllMashine(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        GlobalDataAndTimer.MashinesToHack.Clear();
        viewToResultCommand.ConclusionText("<color=red>All connections mashine clear!</color>");
        return 0;
    }
}