using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTemplate : MonoBehaviour
{
    public int Id;
    public int GlobalDamage;
    public int ArmorEnemyDamage;
    public int HowLongEnemyWasTakeGlobalDamage;
    public int PlayerHealthDamage;
    public float HowOfftenEnemyWasTakeGlobalDamage;

    public string Title;

    [SerializeField] private SupportSpecialAttackView _view;

    private PlayerData _playerDataManipulation;
    private EnemyTemplate _enemy;

    public void Init(EnemyTemplate enemy, PlayerData playerDataManipulator)
    {
        _playerDataManipulation = playerDataManipulator;
        _enemy = enemy;

        _view.Init(this);
        _view._click += TakeDamage;
    }

    private void TakeDamage()
    {
        _enemy.TakeDamage(this);
    }
}
