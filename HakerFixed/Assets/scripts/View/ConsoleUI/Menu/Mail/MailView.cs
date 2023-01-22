using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MailView : MonoBehaviour
{
    [SerializeField] private TMP_Text _content;
    [SerializeField] private GameObject _isReadeble;
    [SerializeField] private Button _buttonClick;
    
    public int MailIndex;

    public event UnityAction<int, MailView> ShowAllData;

    private void OnEnable()
    {
        _buttonClick.onClick.AddListener(OnPointerDown);
    }

    private void OnDisable()
    {
        _buttonClick.onClick.RemoveListener(OnPointerDown);
    }

    public void SetBaseInformation(MailData mailToSetPreviusData, int mailIndex)
    {
        _content.text = $"Click on me to view mail\nTitle: {mailToSetPreviusData.Title}";
        MailIndex = mailIndex;

        if (mailToSetPreviusData.IsReadeble)
        {
            _isReadeble.SetActive(false);
        }
        else
        {
            _isReadeble.SetActive(true);
        }
    }

    public void OnPointerDown()
    {
        ShowAllData?.Invoke(MailIndex, this);
    }
}
