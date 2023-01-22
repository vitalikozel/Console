using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SupportSpecialAttackView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text _nameAttackButton;
    [SerializeField] private TMP_Text _countPlayerTakeHealthDamage;
    [SerializeField] private TMP_Text _countEnemyTakeHealthDamage;
    [SerializeField] private Image _render;

    private bool _canUse = true;

    public float _betwinTime = 0;
    public float RestartEndAttackTime;

    public event UnityAction _click;

    public void Init(SpecialAttackTemplate specialAttack)
    {
        _nameAttackButton.text = specialAttack.Title;
        _countPlayerTakeHealthDamage.text = $"-{specialAttack.PlayerHealthDamage.ToString()} hp";
        _countEnemyTakeHealthDamage.text = $"-{specialAttack.GlobalDamage.ToString()} dm";

        _render.fillAmount = 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_canUse)
        {
            _click.Invoke();
            StartCoroutine(RestartAbilites());
        }
    }

    private IEnumerator RestartAbilites()
    {
        _canUse = false;
        _render.fillAmount = 0;
        _betwinTime = 0;

        while (true)
        {
            if (_betwinTime >= RestartEndAttackTime)
            {
               _canUse = true;
                break;
            }
            else
            {
                _betwinTime += Time.fixedDeltaTime;

                _render.fillAmount = _betwinTime / RestartEndAttackTime;
            }

            yield return new WaitForFixedUpdate();
        }

    }
}
