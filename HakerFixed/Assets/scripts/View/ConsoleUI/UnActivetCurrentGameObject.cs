using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnActivetCurrentGameObject : MonoBehaviour
{
    [SerializeField] private GameObject _offCurrentObject;
    [SerializeField] private float _timeToOffCurrentObject = -1;

    private void Start()
    {
        if(_timeToOffCurrentObject >= 0)
        {
            StartCoroutine(OffObject(_timeToOffCurrentObject));
        }
    }

    public void OffObject()
    {
        _offCurrentObject.SetActive(false);
    }

    private IEnumerator OffObject(float value)
    {
        yield return new WaitForSeconds(value);
        OffObject();
    }
}
