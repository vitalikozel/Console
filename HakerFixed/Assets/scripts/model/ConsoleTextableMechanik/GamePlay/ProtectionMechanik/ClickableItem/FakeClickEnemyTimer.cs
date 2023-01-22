using UnityEngine;

public class FakeClickEnemyTimer : ClickEnemy
{
    protected override void CheckCurrectClick()
    {
        _CurrectClick = false;
    }
}
