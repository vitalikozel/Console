using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textToView;

    public bool IsPlayeble;

    private string _allDialogView;

    public void SetText(string textToView)
    {
        _allDialogView = textToView;
        _textToView.text = "";

        StartCoroutine(TextGeneratorCarutine());
    }

    private IEnumerator TextGeneratorCarutine()
    {
        IsPlayeble = true;
        foreach (char currectChar in _allDialogView)
        {
            _textToView.text += currectChar;
            yield return new WaitForSeconds(0.003f);
        }

        IsPlayeble = false;
    }
}
