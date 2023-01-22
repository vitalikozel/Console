using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskMinerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _countHealth;
    [SerializeField] private Scrollbar _healthBar;
    [SerializeField] private Image _moneyIcon;

    [SerializeField] private Sprite[] _randomIconMoney;

    public float _maxHealth = 0;

    public void Active()
    {
        _moneyIcon.sprite = _randomIconMoney[Random.Range(0, _randomIconMoney.Length)];
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        GetComponent<Animator>().Play("Disable");
    }

    public void SetTitle(string title)
    {
        _title.text = "Current calculate value: " + title;
    }

    public void SetCountHealthText(int count)
    {
        _countHealth.text = $"{GlobalAplicationParametrs.CalculateNormalTextValue(count)}|HP";
    }

    public void SetHealthBar(float currentHealth)
    {
        _healthBar.size = currentHealth / _maxHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
    }
}
