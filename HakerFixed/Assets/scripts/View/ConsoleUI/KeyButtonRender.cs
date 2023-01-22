using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KeyButtonRender : MonoBehaviour
{
    [SerializeField] private TMP_Text _textSymbol;

    private Button _keyButtonComponent;
    private KeyButton _keyButton;

    private void Awake()
    {
        _keyButtonComponent = GetComponent<Button>();
    }

    public void RenderCurrentSymbol(char symbol)
    {
        _textSymbol.text = symbol.ToString();
    }
}
