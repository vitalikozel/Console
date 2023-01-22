using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Terminal : Program
{
    [SerializeField] private PlayerData _playerDataManipulator;
    [SerializeField] private GameObject _tutorialMenuNotepadPannel;
    [SerializeField] private conclusionViewCommnd _viewTextToScreenConsole;
    [SerializeField] private GlobalAplicationParametrs _tasks;
    [SerializeField] private MailsLoader _mail;
    [SerializeField] private TaskLisener _taskLoader;
    [SerializeField] private TextAsset _tutorialIplogger;
    [SerializeField] private DialogeViewCurrectHero _dialog;
    [SerializeField] private StartFightClicker _startFightClicker;
    [SerializeField] private Animator _animator;
    [SerializeField] private TutorialPannelMechanikcs _tutorial;
    public Command[] _commands;

    private void Start()
    {
        _commands = new Command[]
        {
            new InstallProgram(".pkg.install.", "<program name> - instaling program or show installation conditions", _playerDataManipulator, _mail),
            new ShowAllPlayerData(".profile", _playerDataManipulator, " - show all instalig programs"),

            new RemoveAllSave(".rm.all", " - Remove all _save|"),
            new Discord(".dc", " - translate you on discord channel"),
            new Telegram(".tg", " - translate you on Telegramm channel"),
            new ClearConsole(".cls"),
            new SetFontSize(".size.", "<size> - set view text size"),
            new ClearMails(".cl.m", _mail, " - clear all mails (<color=red>dont use if you not finished History Task</color>)"),

            new AddMoneyDebug("oralcumshoot", "debug", true, _playerDataManipulator),
            new AddExpieriensDebug("yourcockissmal", _playerDataManipulator, "debug", true),
            new StartProtection("pt", "debug", true),
            new StartAttack("at", "debug", true),
            new OffTelegram(".off.200", _playerDataManipulator, "delete all _save", true),
            new TakeLittleBust(".weakness", "delete _save", true),
            new AddBreakMashine(".add.break", _playerDataManipulator, "", true),
            new AddStandartTextableMail(".mail.t", _dialog, _mail, "", true),
            new AddStandartWorkingMail(".mail.w", _taskLoader, _mail, "", true),
            new AddCustomMail(".mail.c", _mail, "", true),
            new StartFight("sf", _playerDataManipulator, _animator, _startFightClicker, "", true),
        };

        _playerDataManipulator.LoadData();
    }

    private void ActivateTerminalTutorial()
    {
        _playerDataManipulator.LoadPrologData();

        if (_playerDataManipulator.Prolog.FinishedTutorial != true)
        {
            _viewTextToScreenConsole.ClearConsole();

            InstallTerminalAplication();
            ShowDialog();
            ActivePannelsTutorial();

            _viewTextToScreenConsole.ConclusionText("<u><i><color=#00ffffff>This is what clickable links will look like</color></i></u>");
            _viewTextToScreenConsole.ConclusionText();
            _mail.AddTexableMail(new MailData(_taskLoader.CurrentTask.Title, _taskLoader.CurrentTask.Description.text, true, true, false, false));

            _viewTextToScreenConsole.ConclusionTextSymbolAnimation("\n" +
                "!The input field is always active. Just start typing commands!\n" +
                "GO READ MAIL! menu => (button) mails\n" +
                "GO READ MAIL! menu => (button) mails\n" +
                "GO READ MAIL! menu => (button) mails\n" +
                "GO READ MAIL! menu => (button) mails\n" +
                "GO READ MAIL! menu => (button) mails\n" +
                "In first mail was your main task and tutorial about hack mashines and protection\n" +
                "\n");
        }
    }

    private void ActivePannelsTutorial()
    {
        if (PlayerPrefs.HasKey(CodeName + "tutorial") != true)
        {
            _tutorial.StartDefaultTutorial();
            PlayerPrefs.SetString(CodeName + "tutorial", "a");
        }
    }

    private void InstallTerminalAplication()
    {
        if (!_playerDataManipulator.GetProgramUnderIndex(0).Save.IsBuyed)
        {
            _commands[0].Doing(_viewTextToScreenConsole, "terminal", "", _tasks);
            _viewTextToScreenConsole.ConclusionText("Please wait for installation.");
            _viewTextToScreenConsole.ConclusionText();
        }
    }

    private void ShowDialog()
    {
        if (_playerDataManipulator.Prolog.StartDialog != true)
        {
            DialogMessage[] dialogs =
            {
                    new DialogMessage(0, "Hey hacker.I am stem, the neural assistant."),
                    new DialogMessage(2, "I think you already know that the evil corporation has captured most of the computers"),
                    new DialogMessage(0, "Since you are here, it means that you care about the fate of ordinary users"),
                    new DialogMessage(2, "If you want to know how deep the rabbit hole goes, go and read your mail."),
                    new DialogMessage(0, "And be sure to read what is written in the terminal."),
                    new DialogMessage(0, "Good luck :)")
                };

            _dialog.ViewHeroStemDialog(dialogs);

            _playerDataManipulator.Prolog.StartDialog = true;
            _playerDataManipulator.SaveData();
        }
    }

    public override void TransformOnThisAplication()
    {
        SetCurrectCommandsAplication(_commands);
        ActivateTerminalTutorial();
    }
}

public class StartFight : Command
{
    private StartFightClicker _startnFightClicker;
    private PlayerData _playerData;
    private Animator _animator;

    public StartFight(string command, PlayerData playerData, Animator animator, StartFightClicker startnFightClicker, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _startnFightClicker = startnFightClicker;
        _animator = animator;
        _playerData = playerData;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if(_playerData.Data.Health <= 10)
        {
            viewToResultCommand.ConclusionText($"Your health: {_playerData.Data.Health} is low. Please raise your health to at least > 10");
        }
        else
        {
            viewToResultCommand.ConclusionText($"You start attack on pc");
            _startnFightClicker.StartRandomFight();
        }
        
        return 0;
    }
}

public class AddBreakMashine : Command
{
    private PlayerData _playerData;

    public AddBreakMashine(string command, PlayerData playerData, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = playerData;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        _playerData.LoadData();

        _playerData.AddTakeHealth(120);
        _playerData.Data.CountBreakMashine += 1;

        _playerData.SaveData();
        return 0;
    }
}

public class ClearMails : Command
{
    private MailsLoader _mail;

    public ClearMails(string command, MailsLoader mail, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mail = mail;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        _mail.ClearAllPreviusMailsView();
        _mail.ClearAllMails();
        return 0;
    }
}

public class AddStandartTextableMail : Command
{
    private MailsLoader _mail;
    private DialogeViewCurrectHero _dialoge;

    public AddStandartTextableMail(string command, DialogeViewCurrectHero dialoge, MailsLoader mail, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mail = mail;
        _dialoge = dialoge;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        DialogMessage[] dialogs =
        {
            new DialogMessage(0, "Hello"),
            new DialogMessage(2, "You are probably at a loss and do not understand what is happening but I will say in short."),
            new DialogMessage(0, "I installed you another interface module that will be responsible for the conclusion of my dialogs"),
            new DialogMessage(2, "What for ?"),
            new DialogMessage(0, "Firstly, I will be dripping a manet for this, as there is already a built -in miner here"),
            new DialogMessage(0, "And secondly, the freaks put a rather strong protection on all housing and circles of terminals"),
            new DialogMessage(0, "So now I can deal with such modules"),
            new DialogMessage(0, "But do not worry all the data is transmitted with encrypted"),
            new DialogMessage(0, "Therefore, we can communicate calmly here and on any packs"),
            new DialogMessage(0, "I don't have much _startTime to make you up to the burning"),
            new DialogMessage(0, "Therefore, as you read these everything, go to the menu (icon with 3 bonds)"),
            new DialogMessage(0, "Next, choose the section mail and there already read a new message from me"),
            new DialogMessage(0, "There will be all instructions"),
            new DialogMessage(0, "Good luck :)"),
        };

        _dialoge.ViewHeroStemDialog(dialogs);
        _mail.AddTexableMail(new MailData("Test textable mail", "Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 "));
        return 0;
    }
}

public class AddStandartWorkingMail : Command
{
    private MailsLoader _mail;
    private TaskLisener _tasks;

    public AddStandartWorkingMail(string command, TaskLisener taskLisener, MailsLoader mail, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mail = mail;
        _tasks = taskLisener;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        _tasks.CurrentTask = _tasks.HistoryTasks[0];
        _mail.AddTexableMail(new MailData(_tasks.CurrentTask.Title, _tasks.CurrentTask.Description.text, true, true, false, false));
        return 0;
    }
}

public class AddCustomMail : Command
{
    private MailsLoader _mail;

    public AddCustomMail(string command, MailsLoader mail, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _mail = mail;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        _mail.AddTexableMail(new MailData("Test textable custom", "Dick 2 Dick 2 Dick 2 Dick 2 Dick 2 Dick 2 Dick 2 Dick 2 Dick 2 Dick 2 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 Dick 1 ", true));
        return 0;
    }
}

public class StartAttack : Command
{
    public StartAttack(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        //GlobalDataAndTimer.Attack.StartEnteringCommandAttack();
        //START BASE ENTERING ATTACK FOR TEST
        return 0;
    }
}

public class StartProtection : Command
{
    public StartProtection(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        GlobalDataAndTimer.Protect.StartAttack();
        return 0;
    }
}

public class TakeLittleBust : Command
{
    public TakeLittleBust(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        GlobalDataAndTimer.Ads.ShowRewardedAds();
        GlobalDataAndTimer.Ads.SetViewConclusion(viewToResultCommand);
        return 0;
    }
}

public class OffTelegram : Command
{
    PlayerData _playerData;

    public OffTelegram(string command, PlayerData data, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        _playerData.LoadData();
        _playerData.Data.Telegram = true;
        _playerData.SaveData();

        viewToResultCommand.ConclusionText("Telegram Ads off. Have a nice game :))");

        return 0;
    }
}

public class Telegram : Command
{
    public Telegram(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        Application.OpenURL("https://t.me/patatocorp");
        viewToResultCommand.ConclusionText("Translate...");
        return 0;
    }
}

public class Discord : Command
{
    public Discord(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        Application.OpenURL("https://discord.com/invite/V6CrSw6y5z");
        viewToResultCommand.ConclusionText("Translate...");
        return 0;
    }
}

public class ClearConsole : Command
{
    public ClearConsole(string command) : base(command)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        viewToResultCommand.ClearConsole();
        return 0;
    }
}

public class InstallProgram : Command
{
    private PlayerData _playerMonipulation;
    private MailsLoader _mail;

    public InstallProgram(string command, string discription, PlayerData data, MailsLoader mail) : base(command, discription)
    {
        _playerMonipulation = data;
        _mail = mail;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        _playerMonipulation.LoadData();

        int currectBuyedIndex = GetCurrentBuyProgramIndex(argument);

        if (currectBuyedIndex <= -1)
        {
            viewToResultCommand.ConclusionText("Current program already install");
            return 2;
        }

        bool succesBuy = _playerMonipulation.BuyCurrectProgramm(currectBuyedIndex);
        string errorTextBuying = _playerMonipulation.GetCountNeedPlayerParametrsForBouing(currectBuyedIndex);

        if (succesBuy)
        {
            _playerMonipulation.UpdateDataCurrentPrograms();
        }
        else
        {
            viewToResultCommand.ConclusionText(errorTextBuying);
            return 2;
        }

        _playerMonipulation.SaveData();

        viewToResultCommand.ConclusionText($"Successfully: <color=green>{_playerMonipulation.GetProgramUnderIndex(currectBuyedIndex).Cost}</color>|BTC and <color=green>{_playerMonipulation.GetProgramUnderIndex(currectBuyedIndex).ExpieriensToBuy}</color>|EXP");
        workTimeSleep.StartWorkTask(UnityEngine.Random.Range(0.4f, 1.2f), 11, "Instaling...", $"Program: {argument} - sucess instaled");

        return 0;
    }

    private int GetCurrentBuyProgramIndex(string argument)
    {
        for (int i = 0; i < _playerMonipulation.CountInstallingPrograms; i++)
        {
            if(_playerMonipulation.GetProgramUnderIndex(i).CodeName == argument)
            {
                if (!_playerMonipulation.GetProgramUnderIndex(i).Save.IsBuyed) 
                {
                    return i;
                }
            }
        }

        return -1;
    }
}

public class ShowAllPlayerData : Command
{
    PlayerData _playerDataManipulator;
    
    public ShowAllPlayerData(string command, PlayerData data, string discription = "") : base(command, discription)
    {
        _playerDataManipulator = data;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        _playerDataManipulator.LoadData();

        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText("Player _playerDataManipulator:");
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionTextList($"Btc balance: {GlobalAplicationParametrs.CalculateNormalTextValue(_playerDataManipulator.Data.BTC)}");
        viewToResultCommand.ConclusionTextList($"Lvl Exp: {GlobalAplicationParametrs.CalculateNormalTextValue(_playerDataManipulator.Data.LvlExpieriens)}");
        viewToResultCommand.ConclusionTextList($"Lvl Status count: {GlobalAplicationParametrs.CalculateNormalTextValue(_playerDataManipulator.Data.LvlStatus)}");
        viewToResultCommand.ConclusionTextList($"Lvl Mashine count: {GlobalAplicationParametrs.CalculateNormalTextValue(_playerDataManipulator.Data.LvlMashine)}");
        viewToResultCommand.ConclusionTextList($"Break mashine count: {GlobalAplicationParametrs.CalculateNormalTextValue(_playerDataManipulator.Data.CountBreakMashine)}");
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText("Programs:");
        viewToResultCommand.ConclusionText();

        for (int i = 0; i < _playerDataManipulator.CountInstallingPrograms; i++)
        {
            if (_playerDataManipulator.GetInstallProgram(i).Save.IsBuyed)
                viewToResultCommand.ConclusionTextList($"Program name: <color=#3A77FE>{_playerDataManipulator.GetInstallProgram(i).CodeName.ToLower()}</color>");
        }
        viewToResultCommand.ConclusionText();

        return 0;
    }
}

public class AddMoneyDebug : Command
{
    PlayerData _playerData;

    public AddMoneyDebug(string command, string discription, bool isDebugCommand, PlayerData data) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        _playerData.LoadData();
        _playerData.AddTakeMoney(1000);
        _playerData.SaveData();

        viewToResultCommand.ConclusionText($"succes money count: {_playerData.Data.BTC}");

        return 0;
    }
}

public class AddExpieriensDebug : Command
{
    PlayerData _playerData;

    public AddExpieriensDebug(string command, PlayerData data, string discription, bool isDebugCommand) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        _playerData.LoadData();
        _playerData.AddTakeLvlExpieiens(1000);
        _playerData.SaveData();

        viewToResultCommand.ConclusionText($"succes money count: {_playerData.Data.BTC}");

        return 0;
    }
}

public class RemoveAllSave : Command
{
    public RemoveAllSave(string command, string discription) : base(command, discription)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        PlayerPrefs.DeleteAll();
        viewToResultCommand.ConclusionText($"_save deleate");

        return 0;
    }
}
public class SetFontSize : Command
{
    public SetFontSize(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        viewToResultCommand.SetTextSize(Convert.ToInt32(argument));
        viewToResultCommand.ConclusionText($"Font size set at: {argument}");
        return 0;
    }
}
public class InfoAboutAplication : Command
{
    public InfoAboutAplication(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        viewToResultCommand.ConclusionText();
        viewToResultCommand.ConclusionText("---Info---");
        viewToResultCommand.ConclusionText("This is main aplication and have base commands");
        viewToResultCommand.ConclusionText("------");
        viewToResultCommand.ConclusionText();
        return 0;
    }
}