using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetDataProfile : MonoBehaviour
{
    [SerializeField] private GlobalAplicationParametrs _work;
    [SerializeField] private PlayerData _playerData;

    [SerializeField] private TMP_Text _money;
    [SerializeField] private TMP_Text _expierienses;
    [SerializeField] private TMP_Text _mashinePower;
    [SerializeField] private TMP_Text _mashineHealth;
    [SerializeField] private TMP_Text _statusCount;
    [SerializeField] private TMP_Text _countBreakMashine;
    [SerializeField] private TMP_Text _myIp;
    [SerializeField] private TMP_Text _currentConnectionMashine;
    [SerializeField] private TMP_Text _versionAplicetion;

    private void Start()
    {
        UpdateData();
    }

    private void OnEnable()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        _playerData.LoadData();

        if(_work.CurrentConnectionMashine != null)
        {
            _currentConnectionMashine.text = _work.CurrentConnectionMashine.ip;
        }
        else
        {
            _currentConnectionMashine.text = "<color=green>NULL</color>";
        }

        _money.text = _playerData.Data.BTC.ToString();
        _expierienses.text = _playerData.Data.LvlExpieriens.ToString();
        _mashinePower.text = _playerData.Data.LvlMashine.ToString();
        _mashineHealth.text = _playerData.Data.Health.ToString();
        _statusCount.text = _playerData.Data.LvlStatus.ToString();
        _countBreakMashine.text = _playerData.Data.CountBreakMashine.ToString();
        _myIp.text = _playerData.Data._currentMashineIp;
        _versionAplicetion.text = _playerData.VersionAplication.ToString();
    }
}
