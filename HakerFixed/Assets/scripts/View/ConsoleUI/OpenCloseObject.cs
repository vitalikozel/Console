using UnityEngine;
using UnityEngine.UI;

public class OpenCloseObject : MonoBehaviour
{
    [SerializeField] private Button _buttonToCloseOpenAction;
    [SerializeField] private GameObject _pannelToAction;
    [SerializeField] private bool _isOpenAction;

    private void OnEnable()
    {
        _buttonToCloseOpenAction.onClick.AddListener(OpenCloseObejct);
    }

    private void OnDisable()
    {
        _buttonToCloseOpenAction.onClick.RemoveListener(OpenCloseObejct);
    }

    private void OpenCloseObejct()
    {
        if(_isOpenAction)
            _pannelToAction.SetActive(true);
        else
            _pannelToAction.SetActive(false);
    }
}
