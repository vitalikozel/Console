using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Scrollbar _imageBar;
    [SerializeField] private TMP_Text _healthText;

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _playerData.Data.LvlMaxHealth;
    }

    private void OnEnable()
    {
        _maxHealth = _playerData.Data.LvlMaxHealth;
    }

    public void UpdateDataHealthBar()
    {
        _imageBar.size = _playerData.Data.Health / _maxHealth;
        _healthText.text = $"PC health: {_playerData.Data.Health}";
    }
}
