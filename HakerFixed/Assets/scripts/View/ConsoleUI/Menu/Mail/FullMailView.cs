using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullMailView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _fullTextMail;
    [SerializeField] private SetRectPosition _setOffPosition;

    public int DataToChangeIndexMailList;

    public void SetStandartData(MailData mail, int indexCulling)
    {
        DataToChangeIndexMailList = indexCulling;
        _title.text = mail.Title;

        if (mail.IsFinishTask && mail.IsMailTask)
        {
            _fullTextMail.text = mail.Content + " \n<color=red>Its task already finished</color>";
        }
        else
        {
            _fullTextMail.text = mail.Content;
        }
    }

    public void CloseView()
    {
        _setOffPosition.SetDisablePosition();
    }
}
