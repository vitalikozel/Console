using UnityEngine;

public class ClickEnemyTimer : ClickEnemy
{
    [SerializeField] private float _procentTimeClick = 50;

    public float ProcentTimeClick => (LifeTime * _procentTimeClick) / 100;

    protected override void CheckCurrectClick()
    {
        if(_TimeBetwin <= (LifeTime * _procentTimeClick) / 100)
        {
            _CurrectClick = true;
        }
    }
}
