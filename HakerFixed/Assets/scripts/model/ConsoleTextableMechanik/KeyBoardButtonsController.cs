using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyBoardButtonsController : MonoBehaviour
{
    [SerializeField] private conclusionViewCommnd _viewCommand;
    [SerializeField] private GlobalAplicationParametrs _taskWorker;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private InertialAds _inertialAds;
    [SerializeField] private EnterButton _enterKeyBoradButton;

    [SerializeField] private float _timeToShowAds = 30;

    private Program _currectProgram;
    private Program _programToCheck;
    private string _programCodeName;

    private void Start()
    {
        _playerData.LoadData();

        _currectProgram = _playerData.GetFirstOrDefaultPlayerProgram();
        _currectProgram.TransformOnThisAplication();
        _viewCommand.SetCurrectConnectionData(_currectProgram, _playerData.CurrentMashineIp);

        StartCoroutine(AdsShowLoop(_timeToShowAds));

        SubmitEnterButtonForEnteringCommandConsole();
    }

    public void UnSubmitEnterButtonForEnteringCommandInConsole()
    {
        _enterKeyBoradButton.ClickEnterButtonWithCommand -= CheckCurrectCommand;
    }

    public void SubmitEnterButtonForEnteringCommandConsole()
    {
        _enterKeyBoradButton.ClickEnterButtonWithCommand += CheckCurrectCommand;
    }

    public void CheckCurrectCommand(string command)
    {
        if (GlobalAplicationParametrs.IsBusy == true)
        {
            _viewCommand.AddTextAnimation("<color=red>System already busy!</color>");
            return;
        }

        _viewCommand.ConclusionTextUserCommand(command);

        if (command != "")
        {
            CheckChangeCommand(command);
        }
    }

    private void CheckChangeCommand(string command)
    {
        if (command.Contains(".cd."))
        {
            string programToChange = command.Replace(".cd.", "");
            bool isCurrectProgrammName = false;

            _playerData.LoadData();

            for (int i = 0; i < _playerData.CountInstallingPrograms; i++)
            {
                _programToCheck = _playerData.GetProgramUnderIndex(i);
                _programCodeName = _programToCheck.CodeName.ToLower();

                if (programToChange == _programCodeName && _programToCheck.Save.IsBuyed)
                {
                    _currectProgram = _programToCheck;
                    _currectProgram.TransformOnThisAplication();

                    _viewCommand.ClearConsole();
                    _viewCommand.SetCurrectConnectionData(_currectProgram, _playerData.CurrentMashineIp);
                    _viewCommand.ConclusionText($"Programm changed on {_currectProgram.CodeName}");

                    _currectProgram.ShowAllCommandsToCurrentProgram(_viewCommand);

                    isCurrectProgrammName = true;
                    break;
                }
                else
                {
                    isCurrectProgrammName = false;
                }
            }

            if (!isCurrectProgrammName)
                _viewCommand.ConclusionText("Error in the argument<program name>");
        }
        else
        {
            CheckCurrectCommandsException(command.ToLower(), _taskWorker);
        }
    }

    private void CheckCurrectCommandsException(string command, GlobalAplicationParametrs workTimeSleep)
    {
        int commandSuccesStart = _currectProgram.SendEnterCommand(command, _viewCommand, workTimeSleep);

        if (commandSuccesStart == 1)
            _viewCommand.ConclusionText("<b>Null command exception.</b>", "<~>");

        if (commandSuccesStart  == 2)
            _viewCommand.ConclusionText("<b>Error argument/flag exception. Check the currectness of the entered argument command!</b>", "<>");

        if (commandSuccesStart == 3)
            _viewCommand.ConclusionText("<b>System already busy!</b>", "<$>");

        if(commandSuccesStart == 4)
            _viewCommand.ConclusionText("<b>Unknown system error. The program returned error parameter 0!</b>", "<!>");

        _viewCommand.SetLastTextLastScroll();
    }

    private IEnumerator AdsShowLoop(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            _inertialAds.ShowAds();
        }
    }
}
