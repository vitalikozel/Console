using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeCurrentMashine : TaskToWork
{
    public ProbeCurrentMashine(float moneyBustProcent, int statusBonus) : base(moneyBustProcent, statusBonus)
    {
    }

    public override void GenerateInformation()
    {
        int randomInfo = Random.Range(1, 2);

        switch (randomInfo)
        {
            case 1:
                Title = "Hack my friend's computer";
                Description = "I borrowed a fairly large amount for my friend and, unfortunately, after a while he disappeared. Since he's been missing for quite an impressive amount of _startTime, I'd like to make sure he's all right. Trying to hack it to at least get access to the file system.";
                break;

            case 2:
                Title = "<color=orange>Organizing a bot does not require your help!</color>";
                Description = "We are an organization of internet liberators. We ask you to help us in the fight against controllers that are trying to hack us. Start attacking all possible vehicles of theirs to get their attention. We ask you for help. Thanks in advance.";
                break;
        }
    }

    public override void VerificatioOfFulfilledConditions(MashineToBreakProtection defaultPepople, conclusionViewCommnd view)
    {
        if(defaultPepople.ProtectionLevel == 0)
        {
            _ComplateTask = true;
        }
        else
        {
            _ComplateTask = false;
        }
    }
}
