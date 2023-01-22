using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;

public class ShowAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{ 
    [SerializeField] private float _procentAdsMoneyBust;
    [SerializeField] private PlayerData _playerData;

    [SerializeField] private string _androidAdsID = "Rewarded_Android";
    [SerializeField] private string _iOSAdsID = "Rewarded_iOS";

    private string adId;
    private conclusionViewCommnd _view;

    public void ShowRewardedAds()
    {
        Advertisement.Show(adId, this);
    }

    public void SetViewConclusion(conclusionViewCommnd view)
    {
        _view = view;
    }

    private void Awake()
    {
        adId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdsID
            : _androidAdsID;
    }

    private void Start()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Advertisement.Load(adId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        _view.ConclusionText($"Error loaded");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        _view.ConclusionText($"Error show");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        _view.ConclusionText($"Start show");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        _view.ConclusionText($"Why you click on ads ??)");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            _playerData.AddTakeMoney(System.Convert.ToInt32((_playerData.Data.LvlExpieriens * _procentAdsMoneyBust) / 100));

            _playerData.SaveData();
            _view.ConclusionText($"Success: value to add {System.Convert.ToInt32((_playerData.Data.LvlExpieriens * _procentAdsMoneyBust) / 100)}");
            _view.ConclusionText($"Count money: {_playerData.Data.BTC}");
            _view.ConclusionText("You got a small percentage that just worsened your experience of the game a little :D. But at the same _startTime, he supported the developers, although it would be best if you just subscribed to the telegram channel and watched the news there and not only)))) :D");
            LoadAd();
        }
    }
}
