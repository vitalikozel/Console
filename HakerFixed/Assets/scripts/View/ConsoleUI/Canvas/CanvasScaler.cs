using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    [SerializeField] private RectTransform _consoleCanvas;
    [SerializeField] private RectTransform _buttonContainer;


    void Start()
    {
        float currentConsoleCanvasWidth = Screen.currentResolution.height - _buttonContainer.sizeDelta.y;
        currentConsoleCanvasWidth = currentConsoleCanvasWidth - 35;

        _consoleCanvas.sizeDelta = new Vector2(_consoleCanvas.sizeDelta.x, currentConsoleCanvasWidth);
    }
}
