using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFightClicker : MonoBehaviour
{
    [SerializeField] private SpecialAttackTemplate[] _abilitesToCheck;
    [SerializeField] private EnemyTemplate _enemyToFight;
    [SerializeField] private conclusionViewCommnd _view;
    [SerializeField] private SpawnEnemy _spawnEnemy;
    [SerializeField] private SpawnAbilites _spawnAbilites;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _consoleAnimator;
    [SerializeField] private PlayerData _playerDataManipulator;
    [SerializeField] private ShowInfromationAboutEnemy _viewInfoAboutEnemy;
    [SerializeField] private TutorialPannelMechanikcs _tutorialPannels;

    public void StartRandomFight()
    {
        if (PlayerPrefs.HasKey("tutorialAboutFightMashine") != true)
        {
            _tutorialPannels.StartDefaultTutorial();
            PlayerPrefs.SetString("tutorialAboutFightMashine", "true");
        }

        GlobalAplicationParametrs.IsBusy = true;
        _animator.Play("StartFight", 0, 0.002f);
        EnemyTemplate enemy = _spawnEnemy.SpawnEnemyToParent(_enemyToFight);
        _spawnAbilites.SpawnAvalibleAbilitis(enemy);

        enemy.Finish += FinishAttack;

        StartCoroutine(_viewInfoAboutEnemy.Init(enemy, _spawnAbilites.GetListAbilitysToView()));
    }

    private void FinishAttack(EnemyTemplate enemy)
    {
        _animator.Play("FinishFight", 0, 0.002f);
        _consoleAnimator.Play("Active", 0, 0.002f);
        GlobalAplicationParametrs.IsBusy = false;
    }

    public void ClearData()
    {
        _spawnEnemy.Clear();
        _spawnAbilites.Clear();
    }
}
