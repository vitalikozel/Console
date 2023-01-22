using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MailsLoader : MonoBehaviour
{
    [SerializeField] private conclusionViewCommnd _view;
    [SerializeField] private GameObject _viewAllPreviusMailsPannel;
    [SerializeField] private RectTransform _viewFullTextableMail;
    [SerializeField] private RectTransform _viewFullWorkingMail;
    [SerializeField] private GameObject _contentToLoadMails;
    [SerializeField] private GameObject _notificationAboutMail;
    [SerializeField] private GameObject _notificationImageMenuButton;
    [SerializeField] private MailView _mailView;
    [SerializeField] private FullMailView _fullMailViewTextable;
    [SerializeField] private FullMailView _fullMailViewWorking;
    [SerializeField] private TaskLisener _taskHistory;

    public Mails _listMailsData = new Mails();

    [Serializable]
    public class Mails
    {
        public List<MailData> MailsList = new List<MailData>();
        public int VersionAplication = 0;
    }

    private void OnEnable()
    {
        if (_contentToLoadMails.transform.childCount == 0)
        {
            LoadAllMailsView();
        }
    }

    public void LoadMailsCheck()
    {
        if (_contentToLoadMails.transform.childCount == 0)
        {
            LoadAllMailsView();
        }
    }

    private void LoadAllMailsView()
    {
        LoadMailsData();

        for (int i = 0; i < _listMailsData.MailsList.Count; i++)
        {
            InstanceViewMail(i);
        }
    }

    public void AddTexableMail(MailData mailToAdd)
    {
        LoadMailsData();

        bool addMail = true;

        if (mailToAdd.IsCustomMail)
        {
            for (int i = 0; i < _listMailsData.MailsList.Count; i++)
            {
                if (mailToAdd.Title == _listMailsData.MailsList[i].Title && mailToAdd.Content == _listMailsData.MailsList[i].Content)
                {
                    addMail = false;
                    break;
                }
            }
        }

        if(addMail == true)
        {
            _notificationAboutMail.SetActive(true);
            _notificationImageMenuButton.SetActive(true);
            _view.ConclusionText($"You have one mail");

            _listMailsData.MailsList.Add(mailToAdd);
            InstanceViewMail(_listMailsData.MailsList.Count - 1);
            SaveMailsData();
        }
    }

    private void InstanceViewMail(int index)
    {
        var instanseMail = Instantiate(_mailView, _contentToLoadMails.transform);
        instanseMail.SetBaseInformation(_listMailsData.MailsList[index], index);

        instanseMail.ShowAllData += ShowAllMail;
    }

    private void LoadMailsData()
    {
        if (PlayerPrefs.HasKey("mails"))
        {
            _listMailsData = JsonUtility.FromJson<Mails>(PlayerPrefs.GetString("mails"));
        }
    }

    public void ClearAllPreviusMailsView()
    {
        for (int i = 0; i < _contentToLoadMails.transform.childCount; i++)
        {
            Destroy(_contentToLoadMails.transform.GetChild(i).gameObject);
        }
    }

    public void ClearAllMails()
    {
        PlayerPrefs.DeleteKey("mails");
        _listMailsData.MailsList.Clear();
    }

    private void ShowAllMail(int index, MailView view)
    {
        _listMailsData.MailsList[index].IsReadeble = true;
        view.SetBaseInformation(_listMailsData.MailsList[index], index);

        SaveMailsData();

        if (!_listMailsData.MailsList[index].IsMailTask)
        {
            _viewFullTextableMail.anchoredPosition = new Vector2(0, 0);
            _fullMailViewTextable.SetStandartData(_listMailsData.MailsList[index], index);
        }
        else
        {
            if (_listMailsData.MailsList[index].IsFinishTask)
            {
                _viewFullTextableMail.anchoredPosition = new Vector2(0, 0);
                _fullMailViewTextable.SetStandartData(_listMailsData.MailsList[index], index);
            }
            else
            {
                _viewFullWorkingMail.anchoredPosition = new Vector2(0, 0);
                _fullMailViewWorking.SetStandartData(_listMailsData.MailsList[index], index);
            }
        }
    }

    public void SaveMailsData()
    {
        PlayerPrefs.SetString("mails", JsonUtility.ToJson(_listMailsData));
    }
}
