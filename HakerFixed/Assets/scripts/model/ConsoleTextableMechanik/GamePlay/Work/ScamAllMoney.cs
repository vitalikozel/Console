using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScamAllMoney : TaskToWork
{
    public ScamAllMoney(float moneyBustProcent, int statusBonus) : base(moneyBustProcent, statusBonus)
    {
    }

    public override void GenerateInformation()
    {
        int randomInfo = Random.Range(1, 2);

        switch (randomInfo)
        {
            case 1:
                Title = "Steal all the money from my partner's account as he is going to sue me";
                Description = "The situation is this. He and I started to run a business selling cryptocurrencies. Both took loans, I plowed like a slave, and at that _startTime he played the fool and drank all the money of clients. Of course, I'm not sure about this, but there are quite a few factors that point to this. Therefore, I ask you to steal from him all the money that you can keep for yourself.";
                break;

            case 2:
                Title = "The problem with cryptocurrencies";
                Description = "I am a cryptocurrency trader and they tried to squeeze everything I have from me. Therefore, I ask you to steal all the assets from my computer, since the relevant authorities have already become involved in them.";
                break;
        }
    }

    public override void VerificatioOfFulfilledConditions(MashineToBreakProtection defaultPepople, conclusionViewCommnd view)
    {
        if (defaultPepople.IsScamBTC)
        {
            _ComplateTask = true;
        }
        else
        {
            _ComplateTask = false;
        }
    }
}
