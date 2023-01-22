using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProtectClickAttack : MonoBehaviour, IPointerDownHandler
{
    private PlayerData _playerData;
    private HealthBarView _healthBar;
    private TMP_Text _textHealthPlayer;

    private Vector2 _position;

    private bool _isMoveble;
    private bool _isClicked;
    private int _damage;

    [SerializeField] private Animator _animator;

    [SerializeField] private float _minTimeToClick = 4f;
    [SerializeField] private float _maxTimeToClick = 4.0f;

    private void Update()
    {
        if (_isMoveble)
        {
            transform.Translate(_position * Time.deltaTime);
        }
    }

    public virtual void Init(PlayerData playerData, HealthBarView healthBar, TMP_Text textHealthPlayer)
    {
        _playerData = playerData;
        _healthBar = healthBar;
        _textHealthPlayer = textHealthPlayer;
    }

    public void Active(int damage, float X, float Y)
    {
        _damage = damage;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-X, X), Random.Range(-Y, Y));
        StartCoroutine(timeToClick());
    }

    public void ActiveMotion(int damage)
    {
        _damage = damage;
        _isMoveble = true;
        StartCoroutine(timeToClick());
    }

    public virtual void PlayerNotClickedOnButton()
    {
        _animator.Play("protectionClickButtonDeath", 0, 0.002f);
    }

    public void TakeDamagePlayer()
    {
        _playerData.AddTakeHealth(-_damage);
        _healthBar.UpdateDataHealthBar();
        _textHealthPlayer.text = $"Your health: {_playerData.Data.Health}";
    }

    public void ClickFinishedReset()
    {
        transform.position = new Vector2(0, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isMoveble = false;
        _isClicked = true;
        _animator.Play("click");
    }

    private IEnumerator timeToClick()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeToClick, _maxTimeToClick));

        if (!_isClicked)
        {
            PlayerNotClickedOnButton();
        }
    }
}
