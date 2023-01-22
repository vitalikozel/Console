using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakProtectionLogView1 : MonoBehaviour
{
    [SerializeField] private GameObject _viewPannel;
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _titleForBreakTypeProtection;
    [SerializeField] private TMP_Text _paramtrToHack;
    [SerializeField] private TMP_Text _logBar;

    public void AddNextMessageToLogView(string textToEnter)
    {
        _logBar.text += $"{textToEnter}\n";
    }

    public void SetTitle(string title)
    {
        _titleForBreakTypeProtection.text = title;
    }

    public void SetParametrToHack(string title)
    {
        _paramtrToHack.text = title;
    }

    public void Active()
    {
        _viewPannel.SetActive(true);
    }

    public void Disable()
    {
        _animator.Play("Finish");
    }
}
