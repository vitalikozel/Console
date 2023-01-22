using UnityEngine;
using UnityEngine.UI;

public class ProgessBarChangeView : MonoBehaviour
{
    [SerializeField] private Image _render;
    [SerializeField] private ClickEnemyTimer _enemy;
    public float _startTime;
    public float _betwinTime;

    private void Start()
    {
        _startTime = _enemy.ProcentTimeClick;
    }

    private void FixedUpdate()
    {
        _betwinTime += Time.fixedDeltaTime;

        _render.fillAmount = _betwinTime / _startTime;
    }
}
