using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MailData
{
    public string Title;
    public string Content;
    public bool IsReadeble;
    public bool IsFinishTask;
    public bool IsMailTask;
    public bool IsCustomMail;

    public MailData(string title, string content, bool isCustomMail = false, bool isMailTask = false, bool isRedeble = false, bool isFinishTask = false)
    {
        Title = title;
        Content = content;
        IsReadeble = isRedeble;
        IsFinishTask = isFinishTask;
        IsMailTask = isMailTask;
        IsCustomMail = isCustomMail;
    }

    public virtual bool CheckIsFinishTask()
    {
        return false;
    }
}
