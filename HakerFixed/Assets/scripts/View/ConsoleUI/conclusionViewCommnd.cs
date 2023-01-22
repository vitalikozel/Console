using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class conclusionViewCommnd : MonoBehaviour
{
    [SerializeField] private RectTransform _consoleTextView;
    [SerializeField] private TMP_Text _consoleText;
    [SerializeField] private TMP_Text _currentProgramText;
    [SerializeField] private GlobalAplicationParametrs _taskWorker;
    [SerializeField] private ScrollRect _scrollRect;

    private string _currentConectionIp;

    public Program Program;

    public void SetCurrectConnectionData(Program program, string ipConnection)
    {
        _currentConectionIp = ipConnection;
        Program = program;

        _currentProgramText.text = $"{Program.CodeName}>>";
    }

    public void ConclusionText()
    {
        if (GlobalAplicationParametrs.IsBusy != true)
            AddString("\n");
        else
            StartCoroutine(TryAddTextToConsoleWindow("", ""));
    }

    public void ConclusionTextSymbolAnimation(string textToEnter)
    {
        StartCoroutine(TryAddAnimateTextToConsoleWindow(textToEnter));
    }

    public void ConclusionText(string textToEnter, string addingPrefix = "")
    {
        string currectText = textToEnter.Replace("False", "<color=red>FALSE X</color>");
        string currectTextFinalColor = currectText.Replace("True", "<color=green>TRUE V</color>");

        if (GlobalAplicationParametrs.IsBusy != true)
            AddString($"\n{addingPrefix}{currectTextFinalColor}");
        else
            StartCoroutine(TryAddTextToConsoleWindow(currectTextFinalColor, addingPrefix));
    }

    public void ConclusionTextList(string textToEnter)
    {
        ConclusionText(textToEnter, " * ");
    }

    public void ConclusionTextUserCommand(string command)
    {
        AddString($"\n<color=orange><{_currentConectionIp}$></color><{Program.CodeName}> {command}");
    }

    public void AddTextAnimation(string textToAdd)
    {
        AddString($"\n<#> {textToAdd}");
    }

    public void AddSymbol(char symbol)
    {
        AddString(symbol.ToString());
    }

    public void ClearConsole()
    {
        _consoleText.text = "";
    }

    public void SetTextSize(int size)
    {
        _consoleText.fontSize = size;
    }

    public void SetLastTextLastScroll()
    {
        StartCoroutine(SetCurrentViewPort());
    }

    private IEnumerator SetCurrentViewPort()
    {
        yield return new WaitForSeconds(0.1f);
        _scrollRect.verticalNormalizedPosition = 0;
    }

    private void AddString(string text)
    {
        _consoleText.text += text;
        SetLastTextLastScroll();
    }

    private void AddChar(char text)
    {
        _consoleText.text += text;
        SetLastTextLastScroll();
    }

    private IEnumerator TryAddTextToConsoleWindow(string currentText, string addingPrefix)
    {
        while (true)
        {
            if (GlobalAplicationParametrs.IsBusy != true)
            {
                AddString($"\n{addingPrefix}{currentText}");
                SetLastTextLastScroll();
                break;
            }

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator TryAddAnimateTextToConsoleWindow(string currentText)
    {
        while (true)
        {
            if (GlobalAplicationParametrs.IsBusy != true)
            {
                GlobalAplicationParametrs.IsBusy = true;

                foreach (var symbol in currentText)
                {
                    AddChar(symbol);
                    yield return new WaitForSeconds(0.003f);
                }

                GlobalAplicationParametrs.IsBusy = false;
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
