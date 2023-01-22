using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTaskButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TaskCheker _checkerMainTask;
    [SerializeField] private MailsLoader _loader;
    [SerializeField] private FullMailView _view;

    [SerializeField] private GameObject _finshTaskAnimator;
    [SerializeField] private GameObject _errorTaskAnimator;

    private void OnEnable()
    {
        _button.onClick.AddListener(Check);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Check);
    }

    private void Check()
    {
        if (_checkerMainTask.CurrentTaskFinished())
        {
            _finshTaskAnimator.SetActive(true);

            _loader._listMailsData.MailsList[_view.DataToChangeIndexMailList].IsFinishTask = true;
            _view.CloseView();

            _loader.SaveMailsData();
        }
        else
        {
            _errorTaskAnimator.SetActive(true);
        }
    }
}
