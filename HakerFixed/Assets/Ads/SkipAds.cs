using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class SkipAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdsId = "Interstitial_Android";
    [SerializeField] private string _iosAdsId = "Interstitial_iOS";

    private string _adId;

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAds();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void ShowAds()
    {
        Advertisement.Show(_adId, this);
    }

    private void Awake()
    {
        _adId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iosAdsId
            : _androidAdsId;

        LoadAds();
    }

    private void LoadAds()
    {
        Advertisement.Load(_adId, this);
    }
}
