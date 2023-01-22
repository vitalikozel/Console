using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _succesConnectionPannel;

    public void ActiveSuccessPannel()
    {
        _succesConnectionPannel.SetActive(true);
    }
}
