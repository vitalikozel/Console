using UnityEngine;
using TMPro;

public class InputSymbolsToInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Animator _cursor;

    public void AddSymbol(KeyButton key)
    {
        _inputField.text += key.KeySymbol;
        _cursor.Play("Disable");
    }

    public void AddSymbol(char symbol)
    {
        _inputField.text += symbol;
    }

    public void SetText(string text)
    {
        _inputField.text = text;
    }

    public void ClearInputField()
    {
        _cursor.Play("Cursor");
        _inputField.text = "";
    }

    public string GetCurrectCommand()
    {
        return _inputField.text;
    }
}
