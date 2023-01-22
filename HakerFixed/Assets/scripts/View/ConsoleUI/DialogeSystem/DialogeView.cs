using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeView : MonoBehaviour
{
    [SerializeField] private Animator _animatorAvatar;
    [SerializeField] private SetTextView _textView;
    [SerializeField] private Button _buttonToSkipDialog;

    private DialogMessage[] _dialogsToShow;
    private int _currectIndex = 0;

    private void OnEnable()
    {
        _buttonToSkipDialog.onClick.AddListener(ShowNextText);
    }

    private void OnDisable()
    {
        _buttonToSkipDialog.onClick.RemoveListener(ShowNextText);
    }

    private void ShowNextText()
    {
        if (_textView.IsPlayeble)
        {
            return;
        }

        if(_currectIndex >= _dialogsToShow.Length)
        {
            EndingShowsAllDialogs();
            return;
        }

        switch (_dialogsToShow[_currectIndex].CurrectEmotion)
        {
            case 0:
                ShowIdleEmotion();
                break;
            case 1:
                ShowWowEmtion();
                break;
            case 2:
                ShowhmmmEmtion();
                break;
            case 3:
                ShowAngryEmtion();
                break;
        }

        _textView.SetText(_dialogsToShow[_currectIndex].text);
        _currectIndex++;
    }

    private void ShowIdleEmotion()
    {
        _animatorAvatar.Play("idle");
    }

    private void ShowWowEmtion()
    {
        _animatorAvatar.Play("wow");
    }

    private void ShowhmmmEmtion()
    {
        _animatorAvatar.Play("hmmm");
    }

    private void ShowAngryEmtion()
    {
        _animatorAvatar.Play("angry");
    }

    private void EndingShowsAllDialogs()
    {
        GlobalAplicationParametrs.IsBusy = false;
        gameObject.SetActive(false);
    }

    public void SetCurrectArrayTextToView(DialogMessage[] textToView)
    {
        _currectIndex = 0;
        _dialogsToShow = textToView;
        ShowNextText();
    }
}
