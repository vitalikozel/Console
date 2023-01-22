using BreakEnemyProtection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wpopuru : Program
{
    [SerializeField] private PlayerData _data;
    [SerializeField] private BreakFireWall _wall;
    [SerializeField] private BreakProxy _proxy;
    [SerializeField] private Probe _probe;

    private Command[] _commands;

    private void Start()
    {
        _commands = new Command[]
    {
            new InfoAboutWpopuRu(".inf", " - view tutorial about this program"),
            new SeachDefaultMashines(".search", _data, " - found mashine"),
            new Ping(".ping.", "ip adress mashine to hack - check all info"),
        new AutoConnection(".auto", " - auto connection to last save mashine"),
        new AttackFireWall(".fire", " - starting hack firebase connection mashine", _wall),
        new AttackProxy(".proxy", _proxy, " - starting hack proxy (if open firebase)"),
        new BreakProbe(".probe", _data, _probe, " - hack ports (if open proxy and open all 'port to hack')"),
        new ScamBtc(".scam.btc", _data, " - try scam BTC with active virus"),
        new BreakConnectionWithCurrentMashine(".break", " - Break connection with current connection mashine"),
        new LookDirectory(".cw", " - view currect diretory"),
        new csToFile(".cs.", "<folder>/<filename> -<flag> (flags: '-v' to view content file, '-rm' delete file/folder and return you to the main director) arguments: 'main' - return to main derictory 'ls' - show content current directory"),
    };
    }

    public override void TransformOnThisAplication()
    {
        SetCurrectCommandsAplication(_commands);
    }
}

public class InfoAboutWpopuRu : Command
{
    public InfoAboutWpopuRu(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        viewToResultCommand.ConclusionText($"---Tutorial---");
        viewToResultCommand.ConclusionText($"Here u can hack protection mashine. To do this u must fulfill all conditions. Deffault algorytm to hack protection:");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"1. '.search' - search 1 mashine to protection hack. And he displayed ip protection mashine to hack");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"2. '.ping.enter here ip adress from '.search' command' - Connect to protection mashine with current ip adress (example how to use command .ping.123.123.123)");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"3. '.fire' - start hack firewall. Another window will be displayed in which the protection method and the parameter for hacking will be written. You will need to enter commands that fit the way the machine is protected. Next, protection methods and commands for bypassing them will be written.");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"4. '.proxy' - start hack proxy. To bypass the proxy, you will need to delete some files. They can be viewed if you enter the '.ls' command. After that, you will be shown a list of files to be deleted. You must enter the command '.rm.file name' instead of the file name argument enter the name of the file you want to delete. All files must be deleted");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"5. '.probe' - start hack port (proxy and firewall must be true). Everything here is the same as with the firewall. Just enter commands for a specific protection method");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText($"---Protection methods and commands to hack them---");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"File broke");
        viewToResultCommand.ConclusionText($"Commands what u must enter after you start break protection:");
        viewToResultCommand.ConclusionText($"1. '.cat.pass'");
        viewToResultCommand.ConclusionText($"2. '.cs.pass'");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"File save");
        viewToResultCommand.ConclusionText($"1. '.probe'");
        viewToResultCommand.ConclusionText($"2. '.active'");
        viewToResultCommand.ConclusionText($"3. '.save'");
        viewToResultCommand.ConclusionText($"----");

        return 0;
    }
}

public class SeachDefaultMashines : Command
{
    private PlayerData _playerData;

    public SeachDefaultMashines(string command, PlayerData data, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        GlobalDataAndTimer.StartWorkTask(0.6f, Random.Range(5, 8), "Search currect mashine", true);

        MashineToBreakProtection mashineProtection = new MashineToBreakProtection();
        mashineProtection.GenerateDefaultRandomParametrs(_playerData);
        GlobalDataAndTimer.MashinesToHack.Add(mashineProtection);

        viewToResultCommand.ConclusionText($"ip adress mashine to hack: {mashineProtection.ip} count mashines!");

        return 0;
    }
}

public class Ping : Command
{
    public Ping(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {

        viewToResultCommand.ConclusionText($"Connection to {GlobalDataAndTimer.CurrentIpSet}>");
        GlobalDataAndTimer.LoadAnimationSymbol('.', 0.1f, 10);

        if (CheckAndSetCurrentPeopleIndex(argument, GlobalDataAndTimer))
        {
            GlobalDataAndTimer.AnimationController.ActiveSuccessPannel();
            viewToResultCommand.ConclusionText();
            viewToResultCommand.ConclusionText($"---User {GlobalDataAndTimer.CurrentIpSet} protection---");
            viewToResultCommand.ConclusionTextList($"Protection level: {GlobalDataAndTimer.CurrentConnectionMashine.ProtectionLevel}");
            viewToResultCommand.ConclusionTextList($"Ports to hack: {GlobalDataAndTimer.CurrentConnectionMashine.CountNeedsOpensPorts}");
            viewToResultCommand.ConclusionTextList($"Proxy to hack: {GlobalDataAndTimer.CurrentConnectionMashine.NeedOpenProxy}");
            viewToResultCommand.ConclusionText();
            viewToResultCommand.ConclusionText($"Open protection:");
            viewToResultCommand.ConclusionTextList($"Proxy: {GlobalDataAndTimer.CurrentConnectionMashine.Proxy}");
            viewToResultCommand.ConclusionTextList($"Fire wall: {GlobalDataAndTimer.CurrentConnectionMashine.FireWall}");
            viewToResultCommand.ConclusionTextList($"Possible income: {GlobalDataAndTimer.CurrentConnectionMashine.MoneyWhosWasAddedToPlayer} BTC");
            viewToResultCommand.ConclusionText();
            viewToResultCommand.ConclusionText($"Open ports:");
            viewToResultCommand.ConclusionTextList($"port 21#: - {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[0]}");
            viewToResultCommand.ConclusionTextList($"port 22#: - {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[1]}");
            viewToResultCommand.ConclusionTextList($"port 80#: - {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[2]}");
            viewToResultCommand.ConclusionTextList($"port 443#: - {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[3]}");
            viewToResultCommand.ConclusionText();
        }
        else
        {
            return 2;
        }

        return 0;
    }

    private bool CheckAndSetCurrentPeopleIndex(string enterIp, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        for (int i = 0; i < GlobalDataAndTimer.MashinesToHack.Count; i++)
        {
            if (GlobalDataAndTimer.MashinesToHack[i].ip == enterIp)
            {
                GlobalDataAndTimer.CurrentConnectionMashine = GlobalDataAndTimer.MashinesToHack[i];
                return true;
            }
        }

        return false;
    }
}

public class AutoConnection : Command
{
    public AutoConnection(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {

        viewToResultCommand.ConclusionText($"Connection to {GlobalDataAndTimer.CurrentIpSet}>");
        GlobalDataAndTimer.LoadAnimationSymbol('.', 0.1f, 10);

        if (GlobalDataAndTimer.CurrentConnectionMashine == null)
            return 2;

        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText("---User _playerDataManipulator protection---");
        viewToResultCommand.ConclusionTextList($"Protection level: {GlobalDataAndTimer.CurrentConnectionMashine.ProtectionLevel}");
        viewToResultCommand.ConclusionTextList($"Need open Ports: {GlobalDataAndTimer.CurrentConnectionMashine.CountNeedsOpensPorts}");
        viewToResultCommand.ConclusionTextList($"Need open Proxy: {GlobalDataAndTimer.CurrentConnectionMashine.NeedOpenProxy}");
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText($"Open ports:");
        viewToResultCommand.ConclusionTextList($"Proxy: {GlobalDataAndTimer.CurrentConnectionMashine.Proxy}");
        viewToResultCommand.ConclusionTextList($"Fire wall: {GlobalDataAndTimer.CurrentConnectionMashine.FireWall}");
        viewToResultCommand.ConclusionTextList($"Possible income: {GlobalDataAndTimer.CurrentConnectionMashine.MoneyWhosWasAddedToPlayer} BTC");
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionTextList($"ftp port#: 21 - {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[0]}");
        viewToResultCommand.ConclusionTextList($"ssh port#: - 22 {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[1]}");
        viewToResultCommand.ConclusionTextList($"http port#: 80 - {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[2]}");
        viewToResultCommand.ConclusionTextList($"https port#: 443 - {GlobalDataAndTimer.CurrentConnectionMashine.OpennedPorts[3]}");
        viewToResultCommand.ConclusionText();

        return 0;
    }
}

public class Info : Command
{
    public Info(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText("---Info---");
        viewToResultCommand.ConclusionText("Parametr errors:");
        viewToResultCommand.ConclusionText(" - 0| Default attack port closed");
        viewToResultCommand.ConclusionText(" - 1| Attacking machine have firewall");
        viewToResultCommand.ConclusionText(" - 2| Problem width port");
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText("---Description---");
        viewToResultCommand.ConclusionText("This is a common program for attacking simple unprotected machines. With it," +
            " you have the opportunity to get money or experience or nothing at all. But in order for it to work," +
            " you need to rent a system that cannot be changed to your own." +
            " But it does not cost that much, but if there is not enough money in your account," +
            " then you will not be able to attack or look for attacked cars. Tutorial how tou can watch file system in telegram channel (in somethink file system have very good files or commands for install his):)" +
            "\n flag to edit current file in telegram");
        viewToResultCommand.ConclusionText();

        return 0;
    }
}

public class BreakConnectionWithCurrentMashine : Command
{
    public BreakConnectionWithCurrentMashine(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (argument.Contains("connection"))
        {
            viewToResultCommand.ConclusionText($"You break connection with mashine: {GlobalDataAndTimer.CurrentConnectionMashine.ip}");
            GlobalDataAndTimer.CurrentConnectionMashine = null;
            return 0;
        }

        return 0;
    }
}

public class ScamBtc : Command
{
    PlayerData _playerData;

    public ScamBtc(string command, PlayerData data, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (GlobalDataAndTimer.CurrentConnectionMashine == null)
            return 2;

        string ipConnectionMashine = GlobalDataAndTimer.CurrentConnectionMashine.ip;

        if(GlobalDataAndTimer.CurrentConnectionMashine.ProtectionLevel != 0)
        {
            viewToResultCommand.ConclusionText($"Protection level hight. Ip mashine: {ipConnectionMashine}");
            return 2;
        }

        GlobalDataAndTimer.StartWorkTask(Random.Range(0.1f, 0.6f), Random.Range(3, 5), $"Try drop virus to mashine: {ipConnectionMashine}", true, "");
        viewToResultCommand.ConclusionText($"Attempt to hide virus processes from antiviruses: {ipConnectionMashine}: true");
        viewToResultCommand.ConclusionText($"Attempted hacking TCP/IP model: {ipConnectionMashine}: true");

        if (GlobalDataAndTimer.CurrentConnectionMashine.IsScamBTC)
            return 0;

        if (Random.Range(1, 100) >= 50)
        {
            GlobalDataAndTimer.CurrentConnectionMashine.IsScamBTC = true;

            _playerData.LoadData();

            _playerData.AddTakeMoney(GlobalDataAndTimer.CurrentConnectionMashine.MoneyWhosWasAddedToPlayer);
            _playerData.AddTakeLvlExpieiens(GlobalDataAndTimer.CurrentConnectionMashine.ExpierienWhosWasAddedToPlayer);

            _playerData.SaveData();

            viewToResultCommand.ConclusionText();
            viewToResultCommand.ConclusionText($"Succes you hack mashine: {GlobalDataAndTimer.CurrentConnectionMashine.ip}");
            viewToResultCommand.ConclusionText($"Succes you give: {_playerData.Data.BTC} BTC");
            GlobalDataAndTimer.CurrentConnectionMashine.MoneyWhosWasAddedToPlayer -= GlobalDataAndTimer.CurrentConnectionMashine.MoneyWhosWasAddedToPlayer;
            GlobalDataAndTimer.CurrentConnectionMashine.ExpierienWhosWasAddedToPlayer -= GlobalDataAndTimer.CurrentConnectionMashine.ExpierienWhosWasAddedToPlayer;
        }
        else
        {
            viewToResultCommand.ConclusionText($"Error mashine: {ipConnectionMashine}. Try again");
        }

        return 0;
    }
}

public class BreakProbe : Command
{
    private PlayerData _data;
    private Probe _probe;

    public BreakProbe(string command, PlayerData data, Probe probe, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _data = data;
        _probe = probe;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (GlobalDataAndTimer.CurrentConnectionMashine == null)
            return 2;

        if(GlobalDataAndTimer.CurrentConnectionMashine.FireWall != true)
        {
            viewToResultCommand.ConclusionText($"Error FIREWALL is LOCKED - must be TRUE");
        }

        if(GlobalDataAndTimer.CurrentConnectionMashine.Proxy != true)
        {
            viewToResultCommand.ConclusionText($"Error PROXY is LOCKED - must be TRUE");
        }

        _probe.StartProbe();

        return 0;
    }
}

public class AttackProxy : Command
{
    private BreakProxy _proxy;

    public AttackProxy(string command, BreakProxy proxy, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _proxy = proxy;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (GlobalDataAndTimer.CurrentConnectionMashine == null)
            return 2;

        if (!GlobalDataAndTimer.CurrentConnectionMashine.FireWall)
        {
            GlobalDataAndTimer.StartWorkTask(1, 4, "Try hack/found file transfer protocol", "Ups, unknown system error. The machine is not distracted by commands, perhaps some necessary ports were closed");
            return 4;
        }

        _proxy.StartBreakProxy();

        GlobalDataAndTimer.StartWorkTask(1, 6, "Try hack proxy", true);
        viewToResultCommand.ConclusionText("Successfully, one of the required ports has been opened.");
        viewToResultCommand.ConclusionText();

        GlobalDataAndTimer.CurrentConnectionMashine.Proxy = true;

        return 0;
    }
}

public class AttackFireWall : Command
{
    private BreakFireWall _breakFireWall;

    public AttackFireWall(string command, string discription, BreakFireWall breakFireWall, bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _breakFireWall = breakFireWall;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (GlobalDataAndTimer.CurrentConnectionMashine == null)
            return 2;

        GlobalDataAndTimer.StartWorkTask(1, Random.Range(3, 6), "Scanning the firewall subnet to find vulnerable machines and then hacking them", true, "");
        viewToResultCommand.ConclusionText();

        _breakFireWall.StartBreakFireWall();

        return 0;
    }
}

public class LookDirectory : Command
{
    public LookDirectory(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (GlobalDataAndTimer.CurrentConnectionMashine == null)
            return 2;

        viewToResultCommand.ConclusionText($"Directory: {GlobalDataAndTimer.CurrentConnectionMashine.ip}");

        foreach (var folder in GlobalDataAndTimer.CurrentConnectionMashine.MainDirectoryMashine)
        {
            viewToResultCommand.ConclusionText($"{folder.Name} - folder");
        }

        return 0;
    }
}

public class csToFile : Command
{
    List<FileToLooks> currentFolder;

    string flagToDeleateFile = "-rm";
    string flagToEditFile = "-ch";

    public csToFile(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (GlobalDataAndTimer.CurrentConnectionMashine == null)
            return 2;

        if (currentFolder == null)
            currentFolder = GlobalDataAndTimer.CurrentConnectionMashine.MainDirectoryMashine;

        if (GlobalDataAndTimer.CurrentConnectionMashine.ProtectionLevel >= 1)
            return 2;


        viewToResultCommand.ClearConsole();

        foreach (var folder in currentFolder)
        {
            if (argument.Contains(folder.Name) && flag.Contains(flagToDeleateFile))
            {
                currentFolder.Remove(folder);
                currentFolder = GlobalDataAndTimer.CurrentConnectionMashine.MainDirectoryMashine;
                viewToResultCommand.ConclusionText($"You deleate file/folder: {folder.Name}");
                return 0;
            }

            if (argument.Contains(folder.Name) && !folder.IsFolder && flag.Contains(flagToEditFile))
            {
                string contentWithFlag = argument.Replace($"{folder.Name}", "");
                string currentDiscriptionFile = contentWithFlag.Replace(flag, "");

                folder.Description = currentDiscriptionFile;

                viewToResultCommand.ConclusionText("------");
                viewToResultCommand.ConclusionText($"Edit file: {folder.Name} \nContent: {folder.Description}");
                viewToResultCommand.ConclusionText("------");
                return 0;
            }

            if (argument.Contains(folder.Name) && folder.IsFolder)
            {
                currentFolder = folder.Files;
                viewToResultCommand.ConclusionText($"Current folder: {folder.Name}");
                viewToResultCommand.ConclusionText("------");
                foreach (var files in folder.Files)
                {
                    if(files.IsFolder)
                        viewToResultCommand.ConclusionTextList($"{files.Name} - folder");
                    else
                        viewToResultCommand.ConclusionTextList($"{files.Name} - file");
                }
                viewToResultCommand.ConclusionText("------");
                return 0;
            }
            
            if (argument.Contains(folder.Name) && !folder.IsFolder)
            {
                viewToResultCommand.ConclusionText("------");
                viewToResultCommand.ConclusionText($"Open file: {folder.Name} \nContent: {folder.Description} - file");
                viewToResultCommand.ConclusionText("------");
                return 0;
            }
        }

        if (argument.Contains("ls"))
        {
            viewToResultCommand.ConclusionText($"Directory: {GlobalDataAndTimer.CurrentConnectionMashine.ip}");

            viewToResultCommand.ConclusionText($"------");
            foreach (var folder in currentFolder)
            {
                viewToResultCommand.ConclusionTextList($"{folder.Name}");
            }
            viewToResultCommand.ConclusionText($"------");
            return 0;
        }

        if (argument.Contains("main"))
        {
            currentFolder = GlobalDataAndTimer.CurrentConnectionMashine.MainDirectoryMashine;
            viewToResultCommand.ConclusionText($"you in main directory");
            return 0;
        }

        return 2;
    }
}