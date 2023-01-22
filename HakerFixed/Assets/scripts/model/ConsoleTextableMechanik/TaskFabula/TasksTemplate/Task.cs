using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private MailsLoader _loaderMails;
    [SerializeField] private TaskLisener _taskLisener; 

    public int Id;
    public string Title;
    public TextAsset Description;
    public bool IsFinished;

    public float BustMoenyProcent;
    public float BustExpieriensesProcent;

    public virtual void ActivateAllDataForStartCurrentWorkingTask()
    {

    }

    public virtual bool Check()
    {
        CheckCurrentTask();

        return IsFinished;
    }

    protected virtual void CheckCurrentTask()
    {
        _playerData.LoadData();

        if(_playerData.Data.LvlExpieriens >= 20)
        {
            IsFinished = true;

            _playerData.LoadData();

            _playerData.AddTakeMoney(Convert.ToInt32((_playerData.Data.LvlExpieriens * BustMoenyProcent) / 100));
            _playerData.AddTakeLvlExpieiens(2);
            _playerData.AddTakeStatus(1);

            _playerData.SaveData();

            _loaderMails.AddTexableMail(new MailData("Next task to work", "Unfortunately, the new task is not yet ready for release, but you can subscribe to the telegram channel in which information will be released about when the new story task will be ready", true));

            //_taskLisener.SetNextTask();
            //_loaderMails.AddTexableMail(new MailData("Next task to work", Description.text, true, true));
        }
        else
        {
            IsFinished = false;
        }
    }
}
