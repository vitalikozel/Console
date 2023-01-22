using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MashineToBreakProtection
{
    public bool Proxy;
    public bool FireWall;
    public bool NeedOpenProxy;
    public bool IsContractMashine;
    public bool IsScamBTC;

    public bool[] OpennedPorts =
    {
        false, ///port 21
        false, ///port 22
        false,  ///port 80
        false ///port 443
    };

    public int ProtectionLevel;
    
    public int MoneyWhosWasAddedToPlayer;
    public int ExpierienWhosWasAddedToPlayer;
    public int CountNeedsOpensPorts;

    public string ip;
    public string NameLoginSystemOs;
    public string SecondNameLoginSystemOS;
    public string StatusInChrome;

    public Computer PcMashineSystem = new Computer();

    public Queue SecretCommands;
    public List<FileToLooks> MainDirectoryMashine = new List<FileToLooks>();

    private List<FileToLooks> empty = new List<FileToLooks>();
    private string[] _randomNameFiles =
    {
        "config.txt",
        "_playerDataManipulator.txt",
        "bank-statement.os",
        "orange youtube-query History :)).txt",
        "YoU CaNt OpEn ThIs FiLe BeCoUs TiTlE iS vErY long and I make here Upper cAsE alphabet. -hahahahh",
        "All commands.txt",
        "How to hack console????",
        "Hack money version of console game.apk",
        "not virus.exe"
    };

    public void SetAllParametrsComputerIndex(int index)
    {
        PcMashineSystem = new Computer(index, index, index, 0);
    }

    public void BreakMashine(PlayerData playerData)
    {
        ProtectionLevel = 0;

        playerData.LoadData();
        playerData.Data.CountBreakMashine += 1;
        playerData.SaveData();
    }

    public void GenerateDefaultRandomParametrs(PlayerData playerData)
    {
        ip = $"{Random.Range(0, 255)}.{Random.Range(0, 255)}.{Random.Range(0, 255)}";

        SetRandomCharacteristics(playerData.Data.LvlExpieriens);
        CreateDefaultFolderAndRandomFiles();
    }

    private void SetRandomCharacteristics(int expieriens)
    {
        if (Random.Range(1, 100) >= 60)
        {
            OpennedPorts[3] = true;
            NeedOpenProxy = true;
            CountNeedsOpensPorts = Random.Range(0, 2);

            MoneyWhosWasAddedToPlayer = System.Convert.ToInt32((expieriens * Random.Range(2, 2.3f)) / 100);
            ExpierienWhosWasAddedToPlayer = 1;

            ProtectionLevel = Random.Range(1, 3);

            RandomInforationPerson randomInfo = new RandomInforationPerson();

            NameLoginSystemOs = randomInfo.RandomFirstName();
            SecondNameLoginSystemOS = randomInfo.RandomSecondName();
            StatusInChrome = randomInfo.RandomStatus();
        }
        else
        {
            CountNeedsOpensPorts = 2;
            NeedOpenProxy = true;
            CountNeedsOpensPorts = Random.Range(1, 3);

            ProtectionLevel = 1;

            MoneyWhosWasAddedToPlayer = System.Convert.ToInt32((expieriens * Random.Range(2, 2.45f)) / 100);

            NameLoginSystemOs = "Danik";
            SecondNameLoginSystemOS = "HeLovePinuses";
        }
    }

    private void CreateDefaultFolderAndRandomFiles(bool isHistory = false)
    {
        FileToLooks newHistoryFilefinal = new FileToLooks("new text.txt", empty, "If anyone reads this message, I’ll immediately say that there is no one to blame for what happened and that it happened only to myself. Of course, I could also just talk, but I did not have the courage, so I apologize in advance. To be honest, this persecution is already specifically tired. Farewell and live in joy, your V. Mark");
        FileToLooks addHistoryFile = new FileToLooks("message.txt", empty, "Hi Mark. How are you ? What's new ? You didn't reply to my messages long enough. But otherwise, everything is fine with us, your sister has already gone to school, I hope that you are not very bored away from us. How are things at the new school?");
        FileToLooks addHistoryFileFather = new FileToLooks("message school.txt", empty, "Hello dear Andrew Viltovich. We sincerely apologize from our entire team that we did not prevent the misfortune. For this, you will receive compensation from our educational institution. The perpetrators have long been punished. Once again, we offer our deepest apologies.");
        FileToLooks addHistoryFileFriend = new FileToLooks("message from genry.txt", empty, "Hello. I heard that your son committed suicide. This is a great tragedy both for us and for the whole city. I couldn't even imagine him doing something like that. My condolences");

        List<FileToLooks> documents = new List<FileToLooks>()
        {
            new FileToLooks("namepass.os", empty, "NameLoginSystemOs: Gregory, SecondNameLoginSystemOS: Abobes"),
            new FileToLooks("settings.os", empty, ".start .play.<argument> .telegram .break.connection"),
            new FileToLooks("name system.os", empty, "236498273 4982734872134787 2937648 72367"),
            new FileToLooks("cods.os", empty, "01 001 0010 010 010 0100 1000100 100 1001 0"),
            new FileToLooks("main-system.os", empty, "Unfortunately, I say goodbye to this world. Since I can't repay my debts, but at least I can pay for the treatment of my son with cancer. I apologize in advance to everyone I borrowed from.")
        };

        List<FileToLooks> first = new List<FileToLooks>()
        {
            new FileToLooks("documents", documents, ""),
            new FileToLooks("Nothink important.os", empty, "How u can open this file >:) ?"),
            new FileToLooks("browser.inf", empty, "BCL is a statement of fact, not a promise issued by a bank to meet financial obligations. to which you agreed. When you have paid off your loan"),
            new FileToLooks("mail.inf", empty, "Hello, I know that you have already earned quite a lot from this. When will you pay me back?"),
        };

        List<FileToLooks> mainSystemFolder = new List<FileToLooks>()
        {
            new FileToLooks("main files", first, ""),
            new FileToLooks("dont read this file.txt", empty, "why you read this file :)"),
            new FileToLooks("stupid dialog.txt", empty, "\nDo you know what the most popular thing Alexander Bel tried to invent? \n- Not \n- I do not know either :))"),
        };

        if (isHistory)
        {
            if(Random.Range(0, 100) > 15)
            {
                int rand = Random.Range(1, 4);

                switch (rand)
                {
                    case 1:
                        documents.Add(newHistoryFilefinal);
                        break;

                    case 2:
                        first.Add(addHistoryFileFriend);
                        break;

                    case 3:
                        mainSystemFolder.Add(addHistoryFileFather);
                        break;

                    case 4:
                        documents.Add(addHistoryFile);
                        break;
                }
            }
        }

        List<FileToLooks> browserFolder = new List<FileToLooks>();
        GenerateRandomFilesInCurrectFolder(browserFolder);

        FileToLooks system = new FileToLooks("main.system", mainSystemFolder, "", true);
        FileToLooks browser = new FileToLooks("browser", browserFolder, "", true);

        MainDirectoryMashine.Add(system);
        MainDirectoryMashine.Add(browser);
    }

    private void GenerateRandomFilesInCurrectFolder(List<FileToLooks> folder)
    {
        int countFiles = Random.Range(1, 9);
        
        for (int i = 0; i < countFiles; i++)
        {
            folder.Add(new FileToLooks(_randomNameFiles[Random.Range(0, _randomNameFiles.Length)], empty, $"{Random.Range(100, 9999)}.{Random.Range(100, 9999)} {Random.Range(100, 9999)}.{Random.Range(100, 9999)}"));
        }
    }
}

public class RandomInforationPerson
{
    private string[] _fisrtNamesOs =
    {
        "Edgar",
        "Danik",
        "Artur",
        "Gigachad",
        "Obeme",
        "Abobus",
        "Niggierenio",
        "Wiggierenio",
        "Pigga",
        "Artiom",
        "Zio"
    };

    private string[] _secondsNamesOs =
    {
        "Love",
        "Abobus",
        "Bebros",
        "Megarorshak",
        "Amogus",
        "Edgar",
        "The rock"
    };

    private string[] _statusOs =
    {
        "Haker",
        "User",
        "Admin"
    };

    public string RandomFirstName(int index = -1)
    {
        if(index >= 0)
        {
            return _fisrtNamesOs[index];
        }

        return _fisrtNamesOs[Random.Range(0, _fisrtNamesOs.Length)];
    }

    public string RandomSecondName(int index = -1)
    {
        if (index >= 0)
        {
            return _secondsNamesOs[index];
        }

        return _secondsNamesOs[Random.Range(0, _secondsNamesOs.Length)];
    }

    public string RandomStatus(int index = -1)
    {
        if (index >= 0)
        {
            return _statusOs[index];
        }

        return _statusOs[Random.Range(0, _statusOs.Length)];
    }
}
