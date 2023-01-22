using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class notification : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _enterButton;

    private void Start()
    {
        _enterButton.onClick.AddListener(SetTextNorification);
    }

    public void Open()
    {
        _animator.Play("OpenNotificationPannel");
    }

    public void Close()
    {
        _animator.Play("CloseNotificatinoPannel");
    }

    private void SetTextNorification()
    {
        if (_inputField.text.Contains(".cls"))
        {
            _text.text = "Enter .cls to clear notification";
            _inputField.text = string.Empty;
        }
        else
        {
            _text.text += $"\n {_inputField.text}";
            _inputField.text = string.Empty;
        }
    }
}
