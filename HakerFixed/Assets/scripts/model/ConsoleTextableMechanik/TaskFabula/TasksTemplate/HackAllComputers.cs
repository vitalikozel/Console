using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackAllComputers : Task
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private MailsLoader _loaderMails; 
    [SerializeField] private GlobalAplicationParametrs _worker;

    private MashineToBreakProtection[] _defaultMashinesToHack = 
    {
        new MashineToBreakProtection(),
        new MashineToBreakProtection(),
        new MashineToBreakProtection(),
        new MashineToBreakProtection()
    };

    public override void ActivateAllDataForStartCurrentWorkingTask()
    {
        AddStandartDefaultMahsinesToHack();
    }

    private void AddStandartDefaultMahsinesToHack()
    {
        for (int i = 0; i < _defaultMashinesToHack.Length; i++)
        {
            _defaultMashinesToHack[i].SetAllParametrsComputerIndex(i);
        }

        _worker.MashinesToHack.AddRange(_defaultMashinesToHack);
    }

    public override bool Check()
    {
        CheckCurrentTask();

        return IsFinished;
    }

    protected override void CheckCurrentTask()
    {
        _playerData.LoadData();

        for (int i = 0; i < _defaultMashinesToHack.Length; i++)
        {
            if (_defaultMashinesToHack[i].ProtectionLevel != 0)
            {
                IsFinished = false;
                return;
            }
        }

        IsFinished = true;

        _playerData.LoadData();

        _playerData.AddTakeMoney(Convert.ToInt32((_playerData.Data.LvlExpieriens * BustMoenyProcent) / 100));
        _playerData.AddTakeLvlExpieiens(2);
        _playerData.AddTakeStatus(1);

        _playerData.SaveData();

        _loaderMails.AddTexableMail(new MailData("You finish first task", "Well, I can congratulate you on the fact that you completed your first task. So your next task is to complete a few orders/contracts in chrome. After that, you may then return my trust in you. By the way, I increased your status, so maybe now you will have more opportunities. Well, I can congratulate you on the fact that you completed your first task. So your next task is to complete a few orders/contracts in chrome. After that, you may then return my trust in you. By the way, I increased your status, so maybe now you will have more opportunities. After that I will contact you myself" +
                $"\nYou get: <color=green>{Convert.ToInt32((_playerData.Data.LvlExpieriens * BustMoenyProcent) / 100)}</color>. Check profile", true));
    }
}
