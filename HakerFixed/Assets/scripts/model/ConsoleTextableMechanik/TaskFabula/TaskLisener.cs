using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskLisener : MonoBehaviour
{
    public Task CurrentTask;
    public Task[] HistoryTasks;

    private int currentSaveTaskId = 0;

    private const string _historySavePathName = "idHistoryTask";

    private void Start()
    {
        LoadCurrentHistoryTask();
    }

    public void SetNextTask()
    {
        currentSaveTaskId = PlayerPrefs.GetInt(_historySavePathName);
        currentSaveTaskId++;
        PlayerPrefs.SetInt(_historySavePathName, currentSaveTaskId);

        for (int i = 0; i < HistoryTasks.Length; i++)
        {
            if (currentSaveTaskId == HistoryTasks[i].Id)
            {
                CurrentTask = HistoryTasks[i];
                break;
            }
        }

        CurrentTask.ActivateAllDataForStartCurrentWorkingTask();
    }

    public void LoadCurrentHistoryTask()
    {
        if (PlayerPrefs.HasKey(_historySavePathName))
        {
            currentSaveTaskId = PlayerPrefs.GetInt(_historySavePathName);

            for (int i = 0; i < HistoryTasks.Length; i++)
            {
                if(currentSaveTaskId == HistoryTasks[i].Id)
                {
                    CurrentTask = HistoryTasks[i];
                    break;
                }
            }

            if(currentSaveTaskId == 0)
            {
                CurrentTask = HistoryTasks[0];
            }
        }
        else
        {
            CurrentTask = HistoryTasks[0];
        }

        CurrentTask.ActivateAllDataForStartCurrentWorkingTask();
    }

    public void SaveCurrentHistoryTask()
    {
        PlayerPrefs.SetInt(_historySavePathName, CurrentTask.Id);
    }
}
