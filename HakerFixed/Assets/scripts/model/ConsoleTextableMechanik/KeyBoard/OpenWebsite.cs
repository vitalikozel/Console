using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class OpenWebsite : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string _link;

    public void OnPointerDown(PointerEventData eventData)
    {
        Application.OpenURL(_link);
    }
}
