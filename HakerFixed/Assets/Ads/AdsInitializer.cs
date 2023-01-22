using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _adroidGameId = "4943979";
    [SerializeField] private bool _isTestMode = true;
    private string gameId;

    public void OnInitializationComplete()
    {
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }

    private void Awake()
    {
        gameId = _adroidGameId;
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(gameId, _isTestMode, this);
    }
}
