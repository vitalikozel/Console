using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAplicationParametrs : MonoBehaviour
{
    [SerializeField] private conclusionViewCommnd _view;
    public ProtectionSystem Protect;

    public ShowAds Ads;
    public AnimationController AnimationController;
    public TaskToWork CurrentTaskToWork;
    public MashineToBreakProtection CurrentConnectionMashine;
    public List<MashineToBreakProtection> MashinesToHack = new List<MashineToBreakProtection>();
    public int CurrentIndexMashne = 0;
    public string CurrentIpSet;

    public static bool IsBusy;

    public void LoadAnimationSymbol(char symbol, float timeStep, int symbolCount)
    {
        StartCoroutine(StartAnimationSymbol(symbol, timeStep, symbolCount));
    }

    public void StartWorkTask(float waitTime, int countUpdate, string message, bool isLoadingCharacterAnimation = false, string finishMessage = "---!FinishTask!---")
    {
        StartCoroutine(startWork(waitTime, countUpdate, message, finishMessage, isLoadingCharacterAnimation));
    }

    public void StartWorkTask(float waitTime, int countUpdate, string message, string finishMessage)
    {
        StartCoroutine(startWork(waitTime, countUpdate, message, finishMessage));
    }

    public static string CalculateNormalTextValue(int value)
    {
        string[] nameValue =
        {
            "",
            "K",
            "KK",
            "B",
            "BB",
            "T",
            "TT"
        };

        if (value == 0) return "0";

        int i = 0;
        int valueTextCalculate = value;
        while (i + 1 < nameValue.Length && valueTextCalculate >= 1000)
        {
            valueTextCalculate /= 1000;
            i++;
        }

        return value.ToString(format: "###,###,###") + " " + nameValue[i];
    }

    private IEnumerator StartAnimationSymbol(char symbol, float timeStep, int count)
    {
        IsBusy = true;
        for (int i = 0; i < count; i++)
        {
            _view.AddSymbol(symbol);
            yield return new WaitForSeconds(timeStep);
        }
        IsBusy = false;
    }

    private IEnumerator startWork(float waitTime, int countUpdate, string message, string finishMessage, bool isLoadingString = false, int countAnimationSymbol = 6, float timeStep = 0.1f)
    {
        IsBusy = true;

        for (int i = 1; i <= countUpdate; i++)
        {
            _view.AddTextAnimation($"{message}: {i}/{countUpdate}");

            if (isLoadingString)
            {
                for (int f = 0; f < countAnimationSymbol; f++)
                {
                    _view.AddSymbol('.');
                    yield return new WaitForSeconds(timeStep);
                }

                yield return new WaitForSeconds(timeStep * countAnimationSymbol);
            }
            else
            {
                yield return new WaitForSeconds(waitTime);
            }
        }

        _view.AddTextAnimation(finishMessage);
        IsBusy = false;
    }
}
