using System;
using UnityEngine;

public class Miner : Program
{
    [SerializeField] private PlayerData _playerDataManipulator;
    [SerializeField] private TaskMinerView _taskView;
    [SerializeField] private AnimationCurve _updateExpieriens;
    [SerializeField] private AnimationCurve _updateSolution;
    [SerializeField] private AnimationCurve _collectMoney;
    [SerializeField] private TutorialPannelMechanikcs _tutorial;

    private Command[] _commands;

    private void Start()
    {
        _commands = new Command[]
    {
            new StartMiner(".mining", _playerDataManipulator, _taskView, _collectMoney, _updateExpieriens, _tutorial, $" - Mining"),
            new AddExpierienses(".exp", _playerDataManipulator, _updateExpieriens, $" - Update EXP"),
            new AddSolution(".sol", _playerDataManipulator, _updateSolution, $" - Update SOLUTION"),
            new ShowAllInforamtion(".inf", _playerDataManipulator, _updateExpieriens, _collectMoney, _updateSolution, $" - Show information")
        };
    }

    public override void TransformOnThisAplication()
    {
        SetCurrectCommandsAplication(_commands);
    }
}

public class ShowAllInforamtion : Command
{
    private PlayerData _playerDataManipulator;
    private AnimationCurve _updateExpieriens;
    private AnimationCurve _collectMoney;
    private AnimationCurve _updateSolution;

    public ShowAllInforamtion(string command, PlayerData playerDataManipulator, AnimationCurve updateExpieriens, AnimationCurve collectMoney, AnimationCurve updateSolution, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerDataManipulator = playerDataManipulator;
        _updateExpieriens = updateExpieriens;
        _collectMoney = collectMoney;
        _updateSolution = updateSolution;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        int collectMoneyForOneWork = (int)_collectMoney.Evaluate(_playerDataManipulator.Data.LvlExpieriens);
        int needMoneyForNextLevel = (int)_updateExpieriens.Evaluate(_playerDataManipulator.Data.LvlExpieriens + 1);
        int needMoneyForSolution = (int)_updateSolution.Evaluate(_playerDataManipulator.Data.LvlSolution + 1.2f);

        viewToResultCommand.ConclusionText($"Abilitys to minig:");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"Mining - compite task to get BTC \nEnter '.mining' and start click on enter to solution tasks. \nWhen you solve a certain number of problems you will receive a reward");
        viewToResultCommand.ConclusionText($"Collect money from one task: <color=green>+</color>{collectMoneyForOneWork}|BTC");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"Update EXP - to get more BTC from task");
        viewToResultCommand.ConclusionText($"LVL exp: {_playerDataManipulator.Data.LvlExpieriens}");
        viewToResultCommand.ConclusionText($"Need: <color=red>-</color>{needMoneyForNextLevel}|BTC \nTo start collect money: <color=green>+</color>{collectMoneyForOneWork + 1}|BTC");
        viewToResultCommand.ConclusionText($"----");
        viewToResultCommand.ConclusionText($"Update SOLUTION - to take more solution tasks");
        viewToResultCommand.ConclusionText($"LvlSolution tasks: {_playerDataManipulator.Data.LvlSolution}");
        viewToResultCommand.ConclusionText($"Need: <color=red>-</color>{needMoneyForSolution}|BTC \nTo start take solution tasks: <color=green>+</color>{_playerDataManipulator.Data.LvlSolution + 1}");
        viewToResultCommand.ConclusionText($"----");

        return 0;
    }
}

public class ShowBonusAds : Command
{


    public ShowBonusAds(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        throw new NotImplementedException();
    }
}

public class StartMiner : Command
{
    private PlayerData _playerData;
    private TaskToMiner _task;
    private TaskMinerView _taskView;
    private AnimationCurve _collectMoney;
    private AnimationCurve _expieriensCorve;
    private TutorialPannelMechanikcs _tutorial;

    private string _codeName;

    private class TaskToMiner
    {
        public string Title;
        public int CountHealth;

        private string[] _titles =
        {
            "LiteCoin",
            "BitCoin",
            "VitCoin",
        };

        public void GenerateRandomTitle()
        {
            Title = _titles[UnityEngine.Random.Range(0, _titles.Length)];
        }
    }

    public StartMiner(string command, PlayerData data, TaskMinerView taskView, AnimationCurve collectMoney, AnimationCurve expieriensCorve, TutorialPannelMechanikcs tutorial, string codeName, string discription = "") : base(command, discription)
    {
        _playerData = data;
        _taskView = taskView;
        _collectMoney = collectMoney;
        _codeName = codeName;
        _tutorial = tutorial;
        _expieriensCorve = expieriensCorve;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        if (PlayerPrefs.HasKey(_codeName + "tutorial") != true)
        {
            _tutorial.StartDefaultTutorial();
            PlayerPrefs.SetString(_codeName + "tutorial", "a");
        }

        if (_task == null)
        {
            _task = new TaskToMiner();
            _task.GenerateRandomTitle();
            _task.CountHealth = (int)_expieriensCorve.Evaluate(_playerData.Data.LvlExpieriens + 0.05f);

            _taskView.SetTitle(_task.Title);
            _taskView.SetCountHealthText(_task.CountHealth);
            _taskView.SetHealthBar(_task.CountHealth);
            _taskView.SetMaxHealth(_task.CountHealth);

            _taskView.Active();
        }

        int damage = 0;
        if(UnityEngine.Random.Range(0, 100) > 85)
        {
            damage = _playerData.Data.LvlSolution * 2;
        }
        else
        {
            damage = _playerData.Data.LvlSolution;
        }

        _task.CountHealth -= damage;
        viewToResultCommand.ConclusionText($":{DateTime.Now.Second}:SOLUTION>>: <color=red>+{damage}</color>|{_task.Title}");

        _taskView.SetCountHealthText(_task.CountHealth);
        _taskView.SetHealthBar(_task.CountHealth);

        if (_task.CountHealth <= 0)
        {
            AddBitcoinToPlayer(viewToResultCommand);
            workTimeSleep.StartWorkTask(1, 1, "Collected money");
            _taskView.Disable();
            _task = null;
        }

        return 0;
    }

    private void AddBitcoinToPlayer(conclusionViewCommnd viewToResultCommand)
    {
        _playerData.LoadData();
        int collectMoneyForOneWork = (int)_collectMoney.Evaluate(_playerData.Data.LvlExpieriens);
        _playerData.AddTakeMoney(collectMoneyForOneWork);
        _playerData.SaveData();

        viewToResultCommand.ConclusionText($"Add {collectMoneyForOneWork}");
        viewToResultCommand.ConclusionText($"---You mining: {collectMoneyForOneWork}|BTC---");
        viewToResultCommand.ConclusionText($"---Your balance: <color=green>{_playerData.Data.BTC}</color>|BTC---");
    }
}

public class AddExpierienses : Command
{
    private PlayerData _playerData;
    private AnimationCurve _updateExpieriens;

    public AddExpierienses(string command, PlayerData data, AnimationCurve updateExpieriens, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
        _updateExpieriens = updateExpieriens;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        _playerData.LoadData();

        int needMoneyForNextLevel = (int)_updateExpieriens.Evaluate(_playerData.Data.LvlExpieriens + 1);

        if (_playerData.Data.BTC < needMoneyForNextLevel)
        {
            viewToResultCommand.ConclusionText($"You must have: {needMoneyForNextLevel}|BTC to update");
            return 2;
        }

        _playerData.AddTakeMoney(-needMoneyForNextLevel);
        _playerData.AddTakeHealth(10);
        _playerData.AddTakeLvlExpieiens(1);

        viewToResultCommand.ConclusionText($"Adding health your mashine: +{10}");
        viewToResultCommand.ConclusionText($"You pay: -{needMoneyForNextLevel}|BTC");
        viewToResultCommand.ConclusionText($"BTC Balance: {_playerData.Data.BTC}|BTC");
        viewToResultCommand.ConclusionText($"Adding: +{1}|EXP LVL");
        viewToResultCommand.ConclusionText($"EXP Lvl: {_playerData.Data.LvlExpieriens}");

        _playerData.SaveData();

        viewToResultCommand.ConclusionText("---Success---");

        return 0;
    }
}

public class AddSolution : Command
{
    private PlayerData _playerData;
    private AnimationCurve _updateSolution;

    public AddSolution(string command, PlayerData data, AnimationCurve updateSolution, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
        _updateSolution = updateSolution;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        _playerData.LoadData();

        int needMoneyForSolution = (int)_updateSolution.Evaluate(_playerData.Data.LvlSolution + 1.2f);

        if (_playerData.Data.BTC < needMoneyForSolution)
        {
            viewToResultCommand.ConclusionText($"You must have: {needMoneyForSolution}|BTC to update");
            return 2;
        }

        _playerData.AddTakeMoney(-needMoneyForSolution);
        _playerData.Data.LvlSolution++;

        viewToResultCommand.ConclusionText($"Adding health your mashine: +{10}");
        viewToResultCommand.ConclusionText($"You pay: -{needMoneyForSolution}|BTC");
        viewToResultCommand.ConclusionText($"BTC Balance: {_playerData.Data.BTC}|BTC");
        viewToResultCommand.ConclusionText($"Adding: +{1}|SOLUTION LVL");
        viewToResultCommand.ConclusionText($"SOLUTION Lvl: {_playerData.Data.LvlSolution}");

        _playerData.SaveData();

        viewToResultCommand.ConclusionText("---Success---");

        return 0;
    }
}


public class MinerInfo : Command
{
    public MinerInfo(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        throw new NotImplementedException();
    }
}