using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeViewCurrectHero : MonoBehaviour
{
    [SerializeField] private DialogeView[] _views;
    [SerializeField] private GlobalAplicationParametrs _taskWorker;

    public void ViewHeroStemDialog(DialogMessage[] textToView, bool isWorkTimer = false)
    {
        StartCoroutine(_waitForEnableDialog(0, textToView));
    }

    private IEnumerator _waitForEnableDialog(int indexToEnable, DialogMessage[] textToView)
    {
        while (true)
        {
            if (GlobalAplicationParametrs.IsBusy != true)
            {
                GlobalAplicationParametrs.IsBusy = true;

                _views[0].gameObject.SetActive(true);
                _views[0].SetCurrectArrayTextToView(textToView);
                break;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
