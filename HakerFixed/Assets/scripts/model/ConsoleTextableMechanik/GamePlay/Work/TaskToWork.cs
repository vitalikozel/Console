using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskToWork
{
    [SerializeField] private PlayerData _playerData;

    public string Title;
    public string Description;
    public int id;
    public int StatusBonus;
    public MashineToBreakProtection MashineForTask = new MashineToBreakProtection();
    public List<FileToLooks> FileWhatNeedDelete = new List<FileToLooks>();

    protected bool _ComplateTask;

    private float _moneyBust;
    private FileToLooks currentCheckFolder;
    private FileToLooks _bank;
    private List<FileToLooks> empty = new List<FileToLooks>();

    public TaskToWork(float moneyBustProcent, int statusBonus)
    {
        _moneyBust = moneyBustProcent;
        StatusBonus = statusBonus;
    }

    public virtual bool WorkIsFinished()
    {
        return _ComplateTask;
    }

    public virtual void VerificatioOfFulfilledConditions(MashineToBreakProtection defaultPepople, conclusionViewCommnd view)
    {
        FileToLooks file = new FileToLooks("main", defaultPepople.MainDirectoryMashine, "");
        CheckFilesInFolder(file, view);
    }

    public virtual void GenerateInformation()
    {
        int randomTheme = Random.Range(1, 3);

        switch (randomTheme)
        {
            case 1:
                Title = "Delete necessary files from the machine. I'll give you more information if you accept the offer.";
                Description = "I need to delete some banking files from my partner's computer. Since I will sue him and do not want the _playerDataManipulator to appear somewhere in court";
                break;

            case 2:
                Title = "Remove defamatory documents";
                Description = "I'm the judge who handled the case in which the murderer of two people was tried. I can only say one thing, the defending crow won";
                break;

            case 3:
                Title = "Delete court documents";
                Description = "I am an office employee who made a little mistake in the calculations, for which I was accused of something that I did not do. Could you please help me delete the necessary files, as the computer is already in the queue for verification. My files are called v.macalister";
                break;
        }
    }

    public virtual int MoneyBust()
    {
        int currectBust = 0;

        _playerData.LoadData();

        currectBust = System.Convert.ToInt32((_playerData.Data.LvlExpieriens * _moneyBust) / 100);

        if (currectBust <= 1)
            currectBust = 2;

        _playerData.AddTakeMoney(currectBust);

        _playerData.SaveData();

        return currectBust;
    }

    public virtual void GenerateDefaultTask()
    {
        MashineForTask = new MashineToBreakProtection();
        MashineForTask.GenerateDefaultRandomParametrs(_playerData);

        List<FileToLooks> newFolder = new List<FileToLooks>()
        {
            new FileToLooks("_save.txt", empty, "govno"),
        };

        List<FileToLooks> BankFolder = new List<FileToLooks>()
        {
            new FileToLooks("return _playerDataManipulator", newFolder, "", true),
            new FileToLooks("bank message.os", empty, "Vladimir: Well, it's sad that you are such a stupid deer and fell for my divorce. Considering that I have been working in the field of scam for a long _startTime, it's a pity that you fell for it. But even so, you are a complete loser who was easy to breed for grandmas ahhaha! \nYou: I hate you I will go to court and sue you and this correspondence will be proof that you deceived me for 20,000 bitcoins. But it was my last money and now I have to be homeless + I also have cancer. That's why I don't hate you"),
        };

        _bank = new FileToLooks("bank", BankFolder, "", true);

        FileWhatNeedDelete = new List<FileToLooks>()
        {
            newFolder[0],
            BankFolder[1],
        };

        MashineForTask.MainDirectoryMashine.Add(_bank);
    }

    private void CheckFilesInFolder(FileToLooks folder, conclusionViewCommnd viewToResultCommand)
    {
        currentCheckFolder = folder;

        int i = 0;
        Stack<FileToLooks> folderToCheck = new Stack<FileToLooks>();
        Stack<int> lastIndexChecker = new Stack<int>();
        folderToCheck.Push(folder);
        lastIndexChecker.Push(i);

        while (true)
        {
            viewToResultCommand.ConclusionText($"current check file: {currentCheckFolder.Name}");
            
            if(i >= currentCheckFolder.Files.Count)
            {
                if (folderToCheck.Count == 0)
                {
                    return;
                }
                else
                {
                    viewToResultCommand.ConclusionText($"back to folder: {folderToCheck.Peek()}");
                    currentCheckFolder = folderToCheck.Pop();
                    i = lastIndexChecker.Pop();
                }

                continue;
            }

            if (i < currentCheckFolder.Files.Count && !currentCheckFolder.Files[i].IsFolder)
            {
                viewToResultCommand.ConclusionText($"check file: {currentCheckFolder.Files[i].Name}");
                if (FileToCheck(currentCheckFolder.Files[i]))
                    return;

                i++;
                continue;
            }

            if (currentCheckFolder.Files[i].IsFolder)
            {
                viewToResultCommand.ConclusionText($"go to folder: {currentCheckFolder.Files[i].Name}");
                folderToCheck.Push(currentCheckFolder);
                lastIndexChecker.Push(i + 1);
                currentCheckFolder = currentCheckFolder.Files[i];
                i = 0;
                continue;
            }
        }
    }

    private bool FileToCheck(FileToLooks file)
    {
        for (int i = 0; i < FileWhatNeedDelete.Count; i++)
        {
            if (file.Name != FileWhatNeedDelete[i].Name)
            {
                _ComplateTask = true;
            }
            else
            {
                _ComplateTask = false;
                return true;
            }
        }

        return false;
    }
}
