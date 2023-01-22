using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfromationAboutEnemy : MonoBehaviour
{
    [SerializeField] private TMP_Text _attackParamrtrs;
    [SerializeField] private TMP_Text _baseParametrs;
    [SerializeField] private TMP_Text _commandsToAttack;
    [SerializeField] private TMP_Text _playerAbilitys;
    [SerializeField] private TMP_Text _enemyName;

    [SerializeField] private Button _continueFightWhenReadInformation;

    [SerializeField] private PlayerData _playerDataManipulator;
    [SerializeField] private TutorialPannelMechanikcs _tutorial;

    private void OnEnable()
    {
        _continueFightWhenReadInformation.onClick.AddListener(ContinueFight);
    }

    private void OnDisable()
    {
        _continueFightWhenReadInformation.onClick.RemoveListener(ContinueFight);
    }

    public IEnumerator Init(EnemyTemplate enemyInfo, string[] playerAbilitys)
    {
        this.gameObject.SetActive(true);

        _baseParametrs.text = $"" +
            $"Base parametr information \n" +
            $"\n" +
            $"----\n" +
            $"Max enemy <color=#186961>HEALTH</color>: <i>{enemyInfo.Health}<i>\n" +
            $"Max enemy <color=#3995DC>ARMOR</color>: <i>{enemyInfo.Armor}</i>\n" +
            $"----\n" +
            $"<u>BTC</u> % getting +-<color=red>{enemyInfo.BTCMoneyProcentGetting}</color>\n" +
            $"<u>EXP</u> % getting +-<color=red>{enemyInfo.EXPProcentGetting}</color>\n" +
            $"<u>Status</u> % getting +<color=green>{enemyInfo.SocialStatusProcentGetting}</color>\n" +
            $"----\n";

        _attackParamrtrs.text = $"" +
            $"Base attack information\n" +
            $"----\n" +
            $"Min damage:<color=#F9F1A4>{enemyInfo.MinDamage}</color>:\n" +
            $"Max damage:<color=#60D6D6>{enemyInfo.MaxDamage}</color>:\n" +
            $"----\n" +
            $"Max couldown to <color=red>attack</color>: <i>{enemyInfo.MaxCouldDownStartTimeAttack}</i>\n" +
            $"Min time to attack: <color=red>{enemyInfo.MinTimeAttack}</color>\n" +
            $"Max time to attack: <color=green>{enemyInfo.MaxTimeAttack}</color>\n" +
            $"----\n" +
            $"Types count attack: easy: <color=green>{enemyInfo.CountDefaultAttaks}</color>, midle: <color=orange>{enemyInfo.CountMidleAttaks}</color>, hard: <color=red>{enemyInfo.CountHardAttaks}</color>\n" +
            $"----";

        _enemyName.text = $": {enemyInfo.Name} :";

        _commandsToAttack.text = $"" +
            $"Commands to <b><color=red>attack</color></b> enemy:\n" +
            $"\n" +
            $"----\n";


        foreach (string command in enemyInfo.CommandToTakeDamageEnemy)
        {
            _commandsToAttack.text += $"{command}\n----\n";
        }

        _playerAbilitys.text = $"Your <color=red>exploites</color>:" +
            $"\n" +
            $"----\n";

        foreach (string infoAboutAbility in playerAbilitys)
        {
            _playerAbilitys.text += infoAboutAbility + "\n";
        }

        yield return null;
    }

    public void ContinueFight()
    {
        GlobalAplicationParametrs.IsBusy = false;
        this.gameObject.SetActive(false);

        if (_playerDataManipulator.Prolog.FinishedTutorial != true)
        {
            _tutorial.StartDefaultTutorial();
        }
    }
}
