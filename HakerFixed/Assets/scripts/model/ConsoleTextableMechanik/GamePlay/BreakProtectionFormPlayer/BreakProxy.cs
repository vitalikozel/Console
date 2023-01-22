using BreakEnemyProtection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakEnemyProtection
{
    public class BreakProxy : MonoBehaviour
    {
        [SerializeField] private CommandToBreakProtection[] _commandsToEnter;
        [SerializeField] private conclusionViewCommnd _viewConsoleText;
        [SerializeField] private KeyBoardButtonsController _buttonsController;
        [SerializeField] private GlobalAplicationParametrs _globalData;
        [SerializeField] private EnterButton _enterButton;
        [SerializeField] private BreakProtectionLogView1 _breakProtectionLogView;
        [SerializeField] private string Title = "Start overwriting on files";
        [SerializeField] private string ParametrToHack = "FireWall";
        [SerializeField] private List<string> _filesToDelete;


        private int _isSuccessHack = 0;

        public void StartBreakProxy()
        {
            _buttonsController.UnSubmitEnterButtonForEnteringCommandInConsole();
            _enterButton.ClickEnterButtonWithCommand += CheckCommand;

            _breakProtectionLogView.Active();
            _breakProtectionLogView.SetTitle($"{Title}");
            _breakProtectionLogView.SetParametrToHack($"- parametr to hack: <color=#009DFF>{ParametrToHack}</color>");

            StartCoroutine(Animation());
        }

        private void CheckCommand(string enteringCommand)
        {
            GlobalAplicationParametrs.IsBusy = true;

            string[] command = enteringCommand.Split('.');
            string argument = "";


            if (command.Length == 3)
            {
                argument = command[2];
            }

            if (command[1] == "ls")
            {
                _breakProtectionLogView.AddNextMessageToLogView($"Files to delete:");

                foreach (string fileToDelete in _filesToDelete)
                {
                    _breakProtectionLogView.AddNextMessageToLogView($"<color=white>{fileToDelete}</color>");
                }
                _breakProtectionLogView.AddNextMessageToLogView($"----");
            }

            if (command[1] == "rm")
            {
                bool isSuccess = false;

                foreach (string fileToDelete in _filesToDelete)
                {
                    if (argument.Contains(fileToDelete))
                    {
                        _filesToDelete.Remove(fileToDelete);
                        _breakProtectionLogView.AddNextMessageToLogView($"{fileToDelete} - <color=green>success</color> <color=white>deleted</color>");
                        isSuccess = true;
                        break;
                    }
                }

                if(isSuccess != true)
                {
                    _breakProtectionLogView.AddNextMessageToLogView($"<color=red>error</color> <color=white>name</color>");
                }
            }

            if (_filesToDelete.Count != 0)
            {
                return;
            }

            _viewConsoleText.ConclusionText("<color=green>Success</color> proxy is open!");

            _globalData.CurrentConnectionMashine.Proxy = true;

            GlobalAplicationParametrs.IsBusy = false;
            _breakProtectionLogView.Disable();

            _enterButton.ClickEnterButtonWithCommand -= CheckCommand;
            _buttonsController.SubmitEnterButtonForEnteringCommandConsole();
        }

        private IEnumerator Animation()
        {
            while (true)
            {
                _breakProtectionLogView.AddNextMessageToLogView($"{System.DateTime.Now.Minute}:{System.DateTime.Now.Second}:terminal>>");

                yield return new WaitForSeconds(Random.Range(2f, 3f));
            }
        }
    }

}