using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private SetRectPosition[] _pannels;
    [SerializeField] private Image[] _buttonToChanges;

    public void ActivePannelIndex(int index)
    {
        OffAllPannels();
        OffAllButtonsColor();
        _pannels[index].SetViewPosition();
        _buttonToChanges[index].color = Color.white;
    }

    public void OffAllButtonsColor()
    {
        for (int i = 0; i < _buttonToChanges.Length; i++)
        {
            _buttonToChanges[i].color = new Color(0.4f, 0.4f, 0.4f);
        }
    }

    public void OffAllPannels()
    {
        foreach (var pannel in _pannels)
        {
            pannel.SetDisablePosition();
        }
    }
}
