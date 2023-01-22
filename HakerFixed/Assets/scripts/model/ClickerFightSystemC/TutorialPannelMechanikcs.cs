using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPannelMechanikcs : MonoBehaviour
{
    [SerializeField] private GameObject[] _tutorialPannes;
    [SerializeField] private Button _showNextTutorialPannelButton;
    [SerializeField] private PlayerData _playerDataManipulator;

    private int _indexCurrentTutorialPannel = -1;

    public event UnityAction ShowNextOrFinishTotorialPannel;

    private void OnEnable()
    {
        _showNextTutorialPannelButton.onClick.AddListener(ClickShowNextTutorialPannel);
    }

    private void OnDisable()
    {
        _showNextTutorialPannelButton.onClick.RemoveListener(ClickShowNextTutorialPannel);
    }

    private void ClickShowNextTutorialPannel()
    {
        ShowNextOrFinishTotorialPannel.Invoke();
    }

    public void StartDefaultTutorial()
    {
        StartCoroutine(WaitForBusyObject());
    }

    private IEnumerator WaitForBusyObject()
    {
        while (true)
        {
            if(GlobalAplicationParametrs.IsBusy != true)
            {
                GlobalAplicationParametrs.IsBusy = true;

                gameObject.GetComponent<Animator>().Play("Open");
                ShowNextOrFinishTotorialPannel += ActiveTutorialPannelWithOrder;

                yield return new WaitForSeconds(0.8f);

                ActiveTutorialPannelWithOrder();
                break;
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    private void ActiveTutorialPannelWithOrder()
    {
        OffAllPannels();

        _indexCurrentTutorialPannel++;

        if (_indexCurrentTutorialPannel >= _tutorialPannes.Length)
        {
            FinishTutorial();
            return;
        }
        
        _tutorialPannes[_indexCurrentTutorialPannel].SetActive(true);

        Time.timeScale = 0;
    }

    public void FinishTutorial()
    {
        ShowNextOrFinishTotorialPannel -= ActiveTutorialPannelWithOrder;

        GlobalAplicationParametrs.IsBusy = false;
        Time.timeScale = 1;

        gameObject.GetComponent<Animator>().Play("Close");
        OffAllPannels();

        _playerDataManipulator.Prolog.FinishedTutorial = true;
        _playerDataManipulator.SaveData();
    }

    private void OffAllPannels()
    {
        for (int i = 0; i < _tutorialPannes.Length; i++)
        {
            _tutorialPannes[i].SetActive(false);
        }
    }
}
