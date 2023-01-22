using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakEnemyProtection
{
    public class BreakFireWall : MonoBehaviour
    {
        [SerializeField] private CommandToBreakProtection[] _commandsToEnter;
        [SerializeField] private conclusionViewCommnd _viewConsoleText;
        [SerializeField] private KeyBoardButtonsController _buttonsController;
        [SerializeField] private GlobalAplicationParametrs _globalData;
        [SerializeField] private EnterButton _enterButton;
        [SerializeField] private BreakProtectionLogView1 _breakProtectionLogView;
        [SerializeField] private string Title = "Start overwriting on files";
        [SerializeField] private string ParametrToHack = "FireWall";


        private int _indexEnteringCommand = 0;

        public void StartBreakFireWall()
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

            if (enteringCommand == _commandsToEnter[_indexEnteringCommand].CodeName)
            {
                _breakProtectionLogView.AddNextMessageToLogView($"<color=white>{_commandsToEnter[_indexEnteringCommand].Description}</color>");
                _indexEnteringCommand++;
            }
            else
            {
                _breakProtectionLogView.AddNextMessageToLogView(_commandsToEnter[_indexEnteringCommand].ErrorEnteringCommand);
            }

            if (_indexEnteringCommand < _commandsToEnter.Length)
            {
                return;
            }

            _globalData.CurrentConnectionMashine.FireWall = true;

            _viewConsoleText.ConclusionText("<color=green>Success</color> firewall is open!");

            GlobalAplicationParametrs.IsBusy = false;
            _breakProtectionLogView.Disable();

            _enterButton.ClickEnterButtonWithCommand -= CheckCommand;
            _buttonsController.SubmitEnterButtonForEnteringCommandConsole();
        }

        private IEnumerator Animation()
        {
            while (true)
            {
                string helpMessage = _commandsToEnter[_indexEnteringCommand].Hint;

                if (Random.Range(0, 100) < 85)
                {
                    helpMessage = "uncnow error";
                }

                _breakProtectionLogView.AddNextMessageToLogView($"{System.DateTime.Now.Minute}:{System.DateTime.Now.Second}:terminal>> {helpMessage}");

                yield return new WaitForSeconds(Random.Range(2f, 3f));
            }
        }
    }

}