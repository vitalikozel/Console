using UnityEngine;

public class SpawnKeyboard : MonoBehaviour
{
    [SerializeField] private KeyButton _key;
    [SerializeField] private RectTransform _parentForKeys;
    [SerializeField] private InputSymbolsToInputField _inputSymbolsToInputField;
    [SerializeField] private RectTransform _keyLineTamplate;

    [SerializeField] private string[] _keyBoard;

    private void Start()
    {
        CreateKeyBoard();
    }

    private void CreateKeyBoard()
    {
        RectTransform keyLine = new RectTransform();

        for (int i = 0; i < _keyBoard.Length; i++)
        {
            keyLine = Instantiate(_keyLineTamplate, _parentForKeys);
            
            for (int j = 0; j < _keyBoard[i].Length; j++)
            {
                KeyButton key = AddKeysToLine(keyLine, _keyBoard[i][j]);
                key.Click += _inputSymbolsToInputField.AddSymbol;
            }
        }
    }

    private KeyButton AddKeysToLine(RectTransform keyLineTamplate, char keySymbol)
    {
        var keyTamplate = Instantiate(_key, keyLineTamplate);
        keyTamplate.SetCurrentSymbol(keySymbol);
        keyTamplate.Init();

        return keyTamplate;
    }
}
