using System.Collections;
using UnityEngine;
using TMPro;

public class ProtectionSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleProjection;
    [SerializeField] private AttackTemplateForProtection[] _attacksTemplate;
    [SerializeField] private RectTransform _parent;
    [SerializeField] private AnimationMechanikGroup _mechnikGroup;
    [SerializeField] private conclusionViewCommnd _view;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _maxStartTimeAttack = 5;

    private AttackTemplateForProtection _currentAttack;
    private float _betwinTimeAttack;

    private void Start()
    {
        _maxStartTimeAttack = _maxStartTimeAttack * 60;
        _betwinTimeAttack = _maxStartTimeAttack;
    }

    private void Update()
    {
        if(_betwinTimeAttack <= 0)
        {
            StartAttack();
            _betwinTimeAttack = Random.Range(2, _maxStartTimeAttack);
        }
        else
        {
            _betwinTimeAttack -= Time.deltaTime;
        }
    }

    public void StartAttack()
    {
        _mechnikGroup.StartGame();
        _currentAttack = _attacksTemplate[Random.Range(0, _attacksTemplate.Length)];
        StartCoroutine(Attacks());
    }

    private void StartAttack(AttackTemplateForProtection attackFabuleTemplate)
    {
        _mechnikGroup.StartGame();
        _currentAttack = attackFabuleTemplate;
        StartCoroutine(Attacks());
    }

    private IEnumerator Attacks()
    {
        _titleProjection.text = _currentAttack.Title;

        for (int i = 0; i < _currentAttack.CountAttacks; i++)
        {
            int Xposition = System.Convert.ToInt32(_parent.sizeDelta.x / 2);
            int Yposition = System.Convert.ToInt32(_parent.sizeDelta.y / 2);

            _view.ConclusionText($"X current:{Xposition}, Y{Yposition}");
            _view.ConclusionText($"X:{_parent.sizeDelta.x}, Y{_parent.sizeDelta.y}");
            
            ClickEnemy enemy = Instantiate(_currentAttack.Enemys[Random.Range(0, _currentAttack.Enemys.Length)], _parent); 
            enemy.LifeTime = Random.Range(_currentAttack.MinClickLifeTime, _currentAttack.MaxClickLifeTime);
            enemy.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-Xposition, Xposition), Random.Range(-Yposition, Yposition));
            enemy.Finish += CheckClickEnemyTimer;

            yield return new WaitForSeconds(Random.Range(0.01f, 1.2f));
        }

        while (true)
        {
            if (_parent.childCount <= 0)
            {
                break;
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }

        _view.ConclusionText("You successfully repelled the attack. For more information go to telegram :D");
        _mechnikGroup.FinishGame();
    }

    private void CheckClickEnemyTimer(bool isClick, ClickEnemy enemy)
    {
        if (!isClick)
        {
            enemy.Finish -= CheckClickEnemyTimer;

            StopAllCoroutines();

            _playerData.LoadData();

            _playerData.AddTakeStatus(-_currentAttack.StatusDamage);
            _playerData.AddTakeMoney(-_currentAttack.MoneyDamage);

            _playerData.SaveData();

            LoseGame();
        }
        else
        {
            enemy.Finish -= CheckClickEnemyTimer;
            Destroy(enemy.gameObject);
        }
    }

    private void LoseGame()
    {
        int nbChildren = _parent.childCount;

        for (int i = 0; i <+ nbChildren; i++)
        {
            Destroy(_parent.GetChild(i).gameObject);
        }

        _view.ConclusionText($"Unfortunately, you were unable to prevent the attack on your car. You lost: {_currentAttack.MoneyDamage} btc, status: {_currentAttack.StatusDamage}");
        _mechnikGroup.FinishGame();
    }
}
