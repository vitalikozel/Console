using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Chrome : Program
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextAsset[] _articalsForForum;
    [SerializeField] private conclusionViewCommnd _view;
    [SerializeField] private GlobalAplicationParametrs _taskWorker;

    public Command[] Commands;

    private void Start()
    {
        Commands = new Command[]
    {
            new ShowForum(".forum.", _articalsForForum, "<argument/search query/tag> - Used to search for information on a public forum"),
        new BaseAplicatinos(".base", " - aplications what you can install"),
        new ForumToTakeTask(".work.", playerData, " - here you can found contracts (.work.contracts - show available contracts) (.work.contract -v - show currect contract) (.work.<id contract> -take - take currect contract) (.work.<id contract> -fin - enter when you finish contract)"),
        new ForumToTakeTask(".contract", playerData, " - here you can look your current contract"),
        new InfoAboutAplication(".inf", " - info about aplication"),
        new Debug("debug", "this aplication dosent work", true),
        };
    }

    //public ShopChrome(string codeName, int cost, int expieriensToBuy, bool isBuying = false) : base(codeName, cost, expieriensToBuy, isBuying)
    //{
    //}

    public override void TransformOnThisAplication()
    {
        SetCurrectCommandsAplication(Commands);
    }
}

public class InfoAboutProgram : Command
{
    public InfoAboutProgram(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        viewToResultCommand.ConclusionText("------");
        viewToResultCommand.ConclusionText("Iplogger is a program that parses _playerDataManipulator from a specified site. Pulling up available apishniks on which money will probably lie and add to the general archive of available machines. You must count the number of these machines yourself. But with each exit, the list of available cars is erased. But you can also reset this list in the main application.");
        viewToResultCommand.ConclusionText("------");
        return 0;
    }
}

public class BaseAplicatinos : Command
{
    public BaseAplicatinos(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        viewToResultCommand.ClearConsole();
        viewToResultCommand.ConclusionText($"Starting load");
        GlobalDataAndTimer.StartWorkTask(UnityEngine.Random.Range(0.1f, 0.6f), UnityEngine.Random.Range(3, 7), "Load _playerDataManipulator", true, "------");
        viewToResultCommand.ClearConsole();
        viewToResultCommand.ConclusionText($"Programs what you can install:");
        viewToResultCommand.ConclusionTextList($"Program name: miner cost: free");
        viewToResultCommand.ConclusionTextList($"Program name: wpopu.ru cost: 35 experience: 54");
        viewToResultCommand.ConclusionTextList($"Program name: iplogger cost: 56 experience: 61");
        viewToResultCommand.ConclusionText($"Other programs, you can find ways to install them in the directory of hacked machines");
        viewToResultCommand.ConclusionText();
        return 0;
    }
}

public class ShowForum : Command
{
    private List<ArticleTemplate> _articels = new List<ArticleTemplate>();
    private TextAsset[] _articlesToCheck;

    public ShowForum(string command, TextAsset[] articels, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _articlesToCheck = articels;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        viewToResultCommand.ClearConsole();

        if (flag.Contains("-v"))
        {
            for (int i = 0; i < _articels.Count; i++)
            {
                if(argument.Contains(_articels[i].Id.ToString()))
                {
                    viewToResultCommand.ConclusionText("");
                    viewToResultCommand.ConclusionText($"Title: {_articels[i].Title}");
                    viewToResultCommand.ConclusionText($"Id: {_articels[i].Id}");
                    viewToResultCommand.ConclusionText("------");
                    viewToResultCommand.ConclusionText($"Content: \n{_articels[i].Content}");
                    return 0;
                }
            }
        }

        int maxCountWord = 0;

        _articels.Clear();
        for (int i = 0; i < _articlesToCheck.Length; i++)
        {
            string toCheck = _articlesToCheck[i].text.ToLower();
            int currectCheckSymbol = 0;
            int countWords = 0;

            for (int j = 0; j < toCheck.Length; j++)
            {
                while (true)
                {
                    if (currectCheckSymbol >= argument.Length)
                    {
                        countWords++;
                        currectCheckSymbol = 0;
                        break;
                    }

                    if (toCheck[j] == argument[currectCheckSymbol])
                    {
                        currectCheckSymbol++;
                        break;
                    }
                    else
                    {
                        currectCheckSymbol = 0;
                        break;
                    }
                }
            }

            if(countWords >= 1)
            {
                int countSubstring = 22 * countWords;
                string colorArgument = "<color=orange>" + argument + "</color>";
                string colorArticleText = _articlesToCheck[i].text.Replace(argument, colorArgument);

                string[] firstLine = _articlesToCheck[i].text.Split('\n');
                string subStringArticleText = firstLine[0].Replace("\n", "");

                if (countWords >= maxCountWord)
                {
                    maxCountWord = countWords;
                    _articels.Insert(0, new ArticleTemplate(UnityEngine.Random.Range(999, 9999), countWords, _articlesToCheck[i].name, colorArticleText, subStringArticleText));
                }
                else
                {
                    _articels.Add(new ArticleTemplate(UnityEngine.Random.Range(999, 9999), countWords, _articlesToCheck[i].name, colorArticleText, subStringArticleText));
                }

            }
        }

        var sortedDict = _articels.OrderBy(x => x.CountArgumentWords).ToList();
        sortedDict.Reverse();

        viewToResultCommand.ConclusionText($"On request: <b>{argument}</b> results received: <b><i>{sortedDict.Count}</i><b>");
        viewToResultCommand.ConclusionText($"<u>------</u>");

        if(sortedDict.Count <= 0)
        {
            viewToResultCommand.ConclusionText("Hint not found! Try to formulate your request differently");
            return 0;
        }

        foreach (var art in sortedDict)
        {
            viewToResultCommand.ConclusionText();
            viewToResultCommand.ConclusionText($"<b>Title: <color=#12A10D>{art.Title}</color></b>\n------ \n<i><u>The number of mentions of your request: <color=#3995DC>{art.CountArgumentWords}</color>, Id: <color=#881697>{art.Id}</color></u></i>\nContent: <color=#F2F2F2>{art.ContentForView}...</color>\n------ \n");
            viewToResultCommand.ConclusionText();
        }

        return 0;
    }

    private class ArticleTemplate
    {
        public int Id;
        public int CountArgumentWords;
        public string Title;
        public string Content;
        public string ContentForView;

        public ArticleTemplate(int id, int countArgumentWords, string title, string content, string contentForView)
        {
            Id = id;
            CountArgumentWords = countArgumentWords;
            Title = title;
            Content = content;
            ContentForView = contentForView;
        }
    }
}

public class Debug : Command
{
    public Debug(string command, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs workTimeSleep)
    {
        return 0;
    }
}

public class ForumToTakeTask : Command
{
    private PlayerData _playerData;

    private List<TaskToWork> _generateTasks = new List<TaskToWork>();

    private TaskToWork[] _awalibleTypeTask =
    {
        new TaskToWork(2.6f, 2),
        new ScamAllMoney(2.2f, 1),
        new ProbeCurrentMashine(2.4f, 1)
    };

    public ForumToTakeTask(string command, PlayerData data, string discription = "", bool isDebugCommand = false) : base(command, discription, isDebugCommand)
    {
        _playerData = data;
    }

    public override int Doing(conclusionViewCommnd viewToResultCommand, string argument, string flag, GlobalAplicationParametrs GlobalDataAndTimer)
    {
        if (argument.Contains("contract") && flag.Contains("-v"))
        {
            if(GlobalDataAndTimer.CurrentTaskToWork == null)
            {
                viewToResultCommand.ConclusionText("You dont take contract");
                return 2;
            }

            viewToResultCommand.ClearConsole();
            viewToResultCommand.ConclusionText("------");
            viewToResultCommand.ConclusionText($"Id: {GlobalDataAndTimer.CurrentTaskToWork.id}\nYou accepted contract: {GlobalDataAndTimer.CurrentTaskToWork.Title}. \nDiscription: {GlobalDataAndTimer.CurrentTaskToWork.Description}");

            for (int j = 0; j < GlobalDataAndTimer.CurrentTaskToWork.FileWhatNeedDelete.Count; j++)
            {
                viewToResultCommand.ConclusionTextList($"You must delete this files: {GlobalDataAndTimer.CurrentTaskToWork.FileWhatNeedDelete[j].Name}");
            }

            viewToResultCommand.ConclusionText($"Ip mashine: {GlobalDataAndTimer.CurrentTaskToWork.MashineForTask.ip}");
            viewToResultCommand.ConclusionText("------");
            viewToResultCommand.ConclusionText();
            return 0;
        }

        if (GlobalDataAndTimer.CurrentConnectionMashine != null)
        {
            viewToResultCommand.ConclusionText($"You are connected to an unknown machine: {GlobalDataAndTimer.CurrentConnectionMashine.ip}. Break the connection.");
            return 2;
        }

        if (flag.Contains("-fin"))
        {
            if (argument == GlobalDataAndTimer.CurrentTaskToWork.id.ToString())
            {
                GlobalDataAndTimer.CurrentTaskToWork.VerificatioOfFulfilledConditions(GlobalDataAndTimer.CurrentTaskToWork.MashineForTask, viewToResultCommand);

                if (GlobalDataAndTimer.CurrentTaskToWork.WorkIsFinished())
                {
                    TaskToWork currentTask = GlobalDataAndTimer.CurrentTaskToWork;
                    int money = currentTask.MoneyBust();

                    _playerData.LoadData();

                    _playerData.FinishTaskFormChrome.Add(currentTask.Title);
                    _playerData.AddTakeMoney(money);
                    _playerData.AddTakeStatus(currentTask.StatusBonus);

                    viewToResultCommand.ConclusionText($"You finish task and get bust: {money}");
                    viewToResultCommand.ConclusionText($"Currect balance: {_playerData.Data.BTC}");
                    viewToResultCommand.ConclusionText($"Currect exp balance: {_playerData.Data.LvlExpieriens}");
                    viewToResultCommand.ConclusionText($"Currect status balance: {_playerData.Data.LvlStatus}");
                    GlobalDataAndTimer.CurrentTaskToWork = null;

                    _playerData.SaveData();
                    return 0;
                }
                else
                {
                    viewToResultCommand.ConclusionText($"You have not fulfilled the terms of the contract");
                    return 3;
                }
            }
            else
            {
                return 2;
            }
        }

        if (GlobalDataAndTimer.CurrentTaskToWork != null)
        {
            viewToResultCommand.ConclusionText($"You already have task.");
            return 2;
        }

        if (flag.Contains("-take"))
        {
            bool isFoundNumber = true;

            for (int i = 0; i < _generateTasks.Count; i++)
            {
                if (argument.Contains(_generateTasks[i].id.ToString()))
                {
                    _generateTasks[i].GenerateDefaultTask();

                    GlobalDataAndTimer.CurrentTaskToWork = _generateTasks[i];
                    GlobalDataAndTimer.MashinesToHack.Add(_generateTasks[i].MashineForTask);

                    viewToResultCommand.ClearConsole();
                    viewToResultCommand.ConclusionText();
                    viewToResultCommand.ConclusionText("------");
                    viewToResultCommand.ConclusionText($"You accepted contract: {GlobalDataAndTimer.CurrentTaskToWork.Title}. \nDiscription: {GlobalDataAndTimer.CurrentTaskToWork.Description}");

                    for (int j = 0; j < GlobalDataAndTimer.CurrentTaskToWork.FileWhatNeedDelete.Count; j++)
                    {
                        viewToResultCommand.ConclusionTextList($"You must delete this files: {GlobalDataAndTimer.CurrentTaskToWork.FileWhatNeedDelete[j].Name}");
                    }

                    viewToResultCommand.ConclusionText($"Ip mashine: {GlobalDataAndTimer.CurrentTaskToWork.MashineForTask.ip}");
                    viewToResultCommand.ConclusionText("------");
                    viewToResultCommand.ConclusionText();
                    return 0;
                }
                else
                {
                    isFoundNumber = false;
                }
            }

            if (!isFoundNumber)
            {
                viewToResultCommand.ConclusionText($"Error contract number");
                return 2;
            }
        }

        if (argument.Contains("contracts"))
        {
            _generateTasks = new List<TaskToWork>();

            for (int i = 0; i < 3; i++)
            {
                _generateTasks.Add(GetCurrentRandomContract());
                _generateTasks[i].id = UnityEngine.Random.Range(1000, 9999);
                viewToResultCommand.ConclusionText($"Contract id: {_generateTasks[i].id} Title: {_generateTasks[i].Title}");
            }

            return 0;
        }

        return 2;
    }

    private TaskToWork GetCurrentRandomContract()
    {
        TaskToWork taskToWork = _awalibleTypeTask[UnityEngine.Random.Range(0, _awalibleTypeTask.Length)];
        taskToWork.GenerateInformation();

        return taskToWork;
    }
}