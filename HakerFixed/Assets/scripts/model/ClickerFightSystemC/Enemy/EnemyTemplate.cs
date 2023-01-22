using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyTemplate : MonoBehaviour
{
    private PlayerData _playerData;
    private HealthBarView _playerHealthBarView;
    private TMP_Text _healthTextPlayer;
    private StartFightClicker _startFightClicker;
    private RectTransform _parentSpawnEnemy;
    private EnterButton _submitEnterButton;
    private KeyBoardButtonsController _buttons;
    private conclusionViewCommnd _view;

    private int _textToSpawnPoolIndex = 0;
    private int MoneyBust;
    private int ExpiriensBust;
    private int SocialStatusBust;

    private float _maxHealth;
    private float _maxArmor;
    private float _maxCouldDownStartTimeAttack;
    private string _randomEnteringCommandToAttack;

    [SerializeField] private GameObject _textToSpawn;
    [SerializeField] private GameObject _parentSpawnTextObjects;
    [SerializeField] private TMP_Text _parametr;
    [SerializeField] private TMP_Text _logActionTextInformation;
    [SerializeField] private TMP_Text _backgroundImageAnimation;
    [SerializeField] private TMP_Text _textToEnteringTakeDamageEnemyCommand;
    [SerializeField] private Animator _animator;
    [SerializeField] private ProtectClickAttack[] _defaultAttacks;
    [SerializeField] private ProtectClickAttack[] _midleComplectivityAttacks;
    [SerializeField] private ProtectClickAttack[] _hardComplectivityAttacks;

    [SerializeField] private float _procentToChanseToChangeCommandToAttacl = 30;

    public string Name = "Pigga";
    public int Health = 100;
    public int Armor = 100;
    public int SocialStatus = 10;
    public int MinTimeAttack = 1;
    public int MaxTimeAttack = 4;
    public int MinDamage = 1;
    public int MaxDamage = 1;

    public float BTCMoneyProcentGetting = 2f;
    public float EXPProcentGetting = 1.55f;
    public float SocialStatusProcentGetting = 0.5f;

    public string[] CommandToTakeDamageEnemy;

    public event UnityAction<EnemyTemplate> Finish;

    public int CountDefaultAttaks => _defaultAttacks.Length;
    public int CountMidleAttaks => _midleComplectivityAttacks.Length;
    public int CountHardAttaks => _hardComplectivityAttacks.Length;
    public float MaxCouldDownStartTimeAttack => _maxCouldDownStartTimeAttack;

    public void Init(PlayerData dataPlayer, HealthBarView healthBar, RectTransform parentSpawnEnemy, TMP_Text healthTextPlayer, EnterButton enterButton, KeyBoardButtonsController gameSystem, conclusionViewCommnd view)
    {
        _playerData = dataPlayer;
        _playerHealthBarView = healthBar;
        _parentSpawnEnemy = parentSpawnEnemy;
        _healthTextPlayer = healthTextPlayer;
        _submitEnterButton = enterButton;
        _buttons = gameSystem;
        _view = view;

        _buttons.UnSubmitEnterButtonForEnteringCommandInConsole();
        _submitEnterButton.ClickEnterButtonWithCommand += CheckCommandToAttackEntering;

        _maxArmor = Armor;
        _maxHealth = Health;

        _playerData.LoadData();

        ExpiriensBust = _playerData.CalculateProcentValue(EXPProcentGetting);
        MoneyBust = _playerData.CalculateProcentValue(BTCMoneyProcentGetting);
        SocialStatusBust = _playerData.CalculateProcentValue(SocialStatusProcentGetting);

        StartCoroutine(LaunchActionsWhenSystemNotBusy());
    }

    public void SetRandomEnteringCommand()
    {
        _randomEnteringCommandToAttack = CommandToTakeDamageEnemy[Random.Range(0, CommandToTakeDamageEnemy.Length)];
        _textToEnteringTakeDamageEnemyCommand.text += $"{_randomEnteringCommandToAttack} - <color=#B3009D>change command to attack</color> \n";
    }

    public void CheckCommandToAttackEntering(string command)
    {
        int damage = _playerData.Data.LvlSolution;

        if (command == _randomEnteringCommandToAttack)
        {
            TakeDamage(damage);
            _logActionTextInformation.text += $"you take damage: [-{damage}]\n";
        }
        else
        {
            _view.ConclusionText($"!<color=red>Error entering command</color>!");
            _logActionTextInformation.text += $"!: error command :!\n";
        }
    }

    public void TakeDamage(SpecialAttackTemplate specialAttack)
    {
        if (specialAttack.HowLongEnemyWasTakeGlobalDamage > 0)
        {
            StartCoroutine(LoopTimerHealthEnemyDamage(specialAttack.GlobalDamage, specialAttack.HowOfftenEnemyWasTakeGlobalDamage, specialAttack.HowLongEnemyWasTakeGlobalDamage));
        }

        _playerData.AddTakeHealth(-specialAttack.PlayerHealthDamage);
        _playerData.SaveData();

        if (_playerData.Data.Health <= 0)
        {
            LoosePlayerHealth();
            return;
        }

        _playerHealthBarView.UpdateDataHealthBar();

        _logActionTextInformation.text += "---";
        _logActionTextInformation.text += $"[{specialAttack.Title}]\n";
        _logActionTextInformation.text += $"overide damage: [<color=green>-{specialAttack.ArmorEnemyDamage}</color>] :\n";
        _logActionTextInformation.text += $"you take: [-<b><color=red>{specialAttack.PlayerHealthDamage}</color></b>] :damage \n";
        _logActionTextInformation.text += $"[-{specialAttack.GlobalDamage}] :<color=#186961>global</color> exploit damage \n";
        _logActionTextInformation.text += "---";

        TakeArmor(specialAttack.ArmorEnemyDamage);
        TakeDamage(specialAttack.GlobalDamage);
    }

    private IEnumerator LaunchActionsWhenSystemNotBusy()
    {
        while (true)
        {
            if(GlobalAplicationParametrs.IsBusy == true)
            {
                yield return new WaitForSeconds(1);
                continue;
            }
            else
            {
                GlobalAplicationParametrs.IsBusy = true;
                break;
            }
        }

        SetRandomEnteringCommand();
        StartCoroutine(TakeDamagePlayer());
        StartCoroutine(AnimationHealthArmorTextBars());
        StartCoroutine(LoopDamageCommandAnimation());
    }

    private void TakeDamage(int damage)
    {
        if (Armor >= 0)
        {
            TakeArmor(damage);
        }

        if (Armor <= 0)
        {
            TakeHealth(damage);
        }

        _animator.Play("takeDamage", 0, 0.02f);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void TakeHealth(int damage)
    {
        Health -= damage;

        float health = Health;

        if(Health <= 0)
        {
            Health = 0;
            return;
        }

        _parametr.text += $"Proccessor: <color=white>{health}</color>: <b><color=red>invalid access</color></b> <color=red>-{damage}</color>\n";
    }

    private void TakeArmor(int damage)
    {
        Armor -= damage;

        float armor = Armor;

        if (Armor <= 0)
        {
            Armor = 0;
            return;
        }

        string finishCountArmorArrowsView = "";

        int countArrows = System.Convert.ToInt32((armor * 5) / _maxArmor);

        for (int i = 0; i < countArrows; i++)
        {
            finishCountArmorArrowsView += "|";
        }

        _parametr.text += $"Overload: [<color=#00ffffff>{Armor}</color>] -- [<color=red>{armor}</color>] -- \n";
    }

    private void CalculateHealthColorDamage(int damage)
    {
        if(Health - damage <= 0)
        {
            return;
        }

        Color color;

        int procent = damage * 100 / Health;

        if (procent <= 25)
        {
            color = new Color(255, 255, 255);
        }
        else
        {
            color = new Color(255, 96, 0);
        }
    }

    public void ClearClicableEnemys()
    {
        int nbChildren = _parentSpawnEnemy.transform.childCount;

        for (int i = 0; i < +nbChildren; i++)
        {
            Destroy(_parentSpawnEnemy.transform.GetChild(i).gameObject);
        }
    }

    public virtual void Die()
    {
        _view.ConclusionText($"<color=green>Successfully</color>. You get: +{MoneyBust}|BTC, +{ExpiriensBust} |EXP, +{SocialStatusBust} |Social status");
        _view.ConclusionText($"Your current rank hasn't changed. To get next rank hack: {Random.Range(5, 10)} mashines!");

        _playerData.LoadData();

        _playerData.AddTakeMoney(MoneyBust);
        _playerData.AddTakeStatus(SocialStatusBust);
        _playerData.AddTakeLvlExpieiens(ExpiriensBust);

        ChangeFukctionEnterButton();
        ClearClicableEnemys();
        Finish.Invoke(this);
    }

    public virtual void LoosePlayerHealth()
    {
        _view.ConclusionText($"<color=red>Not successful</color>. You loose: -{MoneyBust}|BTC, -{ExpiriensBust} |EXP, -{SocialStatusBust} |Social status");
        _view.ConclusionText($"You have lost your rank. To return it, hack successfully about  {Random.Range(5, 10)} machines!");

        _playerData.LoadData();

        _playerData.AddTakeMoney(-MoneyBust);
        _playerData.AddTakeStatus(-SocialStatusBust);
        _playerData.AddTakeLvlExpieiens(-ExpiriensBust);

        _playerData.SaveData();

        ChangeFukctionEnterButton();
        ClearClicableEnemys();
        Finish.Invoke(this);
    }

    public virtual void ChangeFukctionEnterButton()
    {
        _submitEnterButton.ClickEnterButtonWithCommand -= CheckCommandToAttackEntering;
        _buttons.SubmitEnterButtonForEnteringCommandConsole();
    }

    private IEnumerator LoopDamageCommandAnimation()
    {
        while (true)
        {
            if (Random.Range(0, 100) >= _procentToChanseToChangeCommandToAttacl)
            {
                SetRandomEnteringCommand();
            }

            _backgroundImageAnimation.text += Random.Range(0, 1) + Random.Range(0, 1);
            _textToEnteringTakeDamageEnemyCommand.text += $"{_randomEnteringCommandToAttack} - <color=#60D6D6>command for attack</color>!\n";
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    private IEnumerator AnimationHealthArmorTextBars()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            _parametr.text += $"<color=green>Proccessor</color>: <color=#186961>{Health}</color>: [-0]-<b><color=red>invalid access</color></b>\n";
            yield return new WaitForSeconds(0.2f);
            _parametr.text += $"Overload: [<color=#3995DC>{Armor}</color>] --[<color=#C19B00>0</color>]--\n";
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    private IEnumerator LoopTimerHealthEnemyDamage(int damage, float timerAttack, float howLongWasAttack)
    {
        float timeLongAttack = howLongWasAttack;

        while (timeLongAttack < howLongWasAttack)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(timerAttack);
            timeLongAttack -= 1;
        }
    }

    private IEnumerator TakeDamagePlayer()
    {
        while (true)
        {
            if(_playerData.Data.Health <= 0)
            {
                LoosePlayerHealth();
                yield return null;
            }

            _animator.Play("punch", 0, 0.02f);

            float Xposition = System.Convert.ToInt32(_parentSpawnEnemy.sizeDelta.x / 2);
            float Yposition = System.Convert.ToInt32(_parentSpawnEnemy.sizeDelta.y / 2);

            int attackType = Random.Range(1 ,3);

            switch (attackType)
            {
                case 1:
                    _logActionTextInformation.text += $":<color=#186961><color=green>standart</color> enemy attack</color>:\n";
                    SpawnEnemyAttackPoint(_defaultAttacks, Xposition, Yposition);
                    break;

                case 2:
                    _logActionTextInformation.text += $":<color=#186961><color=orange>midle</color> enemy attack</color>:\n";
                    SpawnEnemyAttackPoint(_midleComplectivityAttacks, Xposition, Yposition);
                    break;

                case 3:
                    _logActionTextInformation.text += $":<color=#186961><color=red>hard</color> enemy attack</color>:\n";
                    SpawnEnemyAttackPoint(_hardComplectivityAttacks, Xposition, Yposition);
                    break;
            }

            _maxCouldDownStartTimeAttack = Random.Range(MinTimeAttack, MaxTimeAttack);

            yield return new WaitForSeconds(_maxCouldDownStartTimeAttack);
        }
    }

    private void SpawnEnemyAttackPoint(ProtectClickAttack[] attacksTypes, float Xposition, float Yposition)
    {
        ProtectClickAttack attack = Instantiate(attacksTypes[Random.Range(0, attacksTypes.Length - 1)], _parentSpawnEnemy);
        attack.Init(_playerData, _playerHealthBarView, _healthTextPlayer);
        attack.Active(MaxDamage, Xposition, Yposition);
    }
}
