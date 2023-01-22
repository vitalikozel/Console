using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private PlayerData _playerDataManipulator;
    [SerializeField] private HealthBarView _healthBarView;
    [SerializeField] private GameObject _parentToSpawn;
    [SerializeField] private RectTransform _parentSpawnEnemyClick;
    [SerializeField] private TMP_Text _textHealthPlayer;
    [SerializeField] private EnterButton _submitEnterButton;
    [SerializeField] private KeyBoardButtonsController _gameSystem;
    [SerializeField] private conclusionViewCommnd _view;

    public EnemyTemplate SpawnEnemyToParent(EnemyTemplate _enemyToSpawn)
    {
        _textHealthPlayer.text = $"Your health: {_playerDataManipulator.Data.Health}";
        var spawningEnemy = Instantiate(_enemyToSpawn, _parentToSpawn.transform);
        spawningEnemy.Init(_playerDataManipulator, _healthBarView, _parentSpawnEnemyClick, _textHealthPlayer, _submitEnterButton, _gameSystem, _view);

        return spawningEnemy;
    }

    public void Clear()
    {
        int nbChildren = _parentToSpawn.transform.childCount;

        for (int i = 0; i < +nbChildren; i++)
        {
            Destroy(_parentToSpawn.transform.GetChild(i).gameObject);
        }
    }
}
