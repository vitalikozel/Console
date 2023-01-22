using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCheker : MonoBehaviour
{
    [SerializeField] private TaskLisener _lisener;

    public bool CurrentTaskFinished()
    {
        _lisener.CurrentTask.Check();
        return _lisener.CurrentTask.IsFinished;
    }
}
