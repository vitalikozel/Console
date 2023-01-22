using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRectPosition : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private int _countRectPOsitionOffset = 700;
    
    public void SetViewPosition()
    {
        if(gameObject.TryGetComponent(out SetDataProfile data))
        {
            data.UpdateData();
        }

        if(gameObject.TryGetComponent(out MailsLoader mails))
        {
            mails.LoadMailsCheck();
        }

        _rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void SetDisablePosition()
    {
        _rectTransform.anchoredPosition = new Vector2(-_countRectPOsitionOffset, _countRectPOsitionOffset);
    }
}
