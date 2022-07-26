using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Mediation;
using System;
using UnityEngine.UI;
using TMPro;

public class DoubleCoinsAd : MonoBehaviour
{
    [Header("Ad Unit Ids"), Tooltip("Ad Unit Ids for each platform that represent Mediation waterfalls.")]
    public string androidAdUnitId;
    [Tooltip("Ad Unit Ids for each platform that represent Mediation waterfalls.")]
    public string iosAdUnitId;

    [SerializeField] CoinsPerDistanceTracker_Module coinsPerDistance;
    [SerializeField] TextMeshProUGUI adStateDisplay;

    [SerializeField] Button button;
    [SerializeField] GameObject checkOutObj;

    IRewardedAd m_RewardedAd;

    async void Start()
    {
        if (PremiumData.hasNoAds)
        {
            adStateDisplay.color = new Color(1, 1, 1, 0);

            button.onClick.AddListener(RewardDouble);
        }
        else
        {
            button.onClick.AddListener(ShowRewarded);
        }

        try
        {
            Debug.Log("Initializing...");
            await UnityServices.InitializeAsync();
            Debug.Log("Initialized!");
            InitializationComplete();
        }
        catch (Exception e)
        {
            InitializationFailed(e);

            adStateDisplay.color = Color.red; // Error de inicialización
        }
    }

    void OnDestroy()
    {
        m_RewardedAd?.Dispose();
    }

    public async void ShowRewarded()
    {
        if (m_RewardedAd?.AdState == AdState.Loaded)
        {
            try
            {
                RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                showOptions.AutoReload = true;
                await m_RewardedAd.ShowAsync(showOptions);
                Debug.Log("Rewarded Shown!");
            }
            catch (ShowFailedException e)
            {
                Debug.LogWarning($"Rewarded failed to show: {e.Message}");

                adStateDisplay.color = Color.blue; // Error de mostrar
            }
        }

        adStateDisplay.color = Color.yellow; // Ad no cargado
    }

    public async void ShowRewardedWithOptions()
    {
        if (m_RewardedAd?.AdState == AdState.Loaded)
        {
            try
            {
                //Here we provide a user id and custom data for server to server validation.
                RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                showOptions.AutoReload = true;
                S2SRedeemData s2SData;
                s2SData.UserId = "my cool user id";
                s2SData.CustomData = "{\"reward\":\"Gems\",\"amount\":20}";
                showOptions.S2SData = s2SData;

                await m_RewardedAd.ShowAsync(showOptions);
                Debug.Log("Rewarded Shown!");
            }
            catch (ShowFailedException e)
            {
                Debug.LogWarning($"Rewarded failed to show: {e.Message}");
            }
        }
    }

    async void LoadAd()
    {
        try
        {
            await m_RewardedAd.LoadAsync();
        }
        catch (LoadFailedException)
        {
            // We will handle the failure in the OnFailedLoad callback
        }
    }

    void InitializationComplete()
    {
        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                m_RewardedAd = MediationService.Instance.CreateRewardedAd(androidAdUnitId);
                break;

            case RuntimePlatform.IPhonePlayer:
                m_RewardedAd = MediationService.Instance.CreateRewardedAd(iosAdUnitId);
                break;
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
                m_RewardedAd = MediationService.Instance.CreateRewardedAd(!string.IsNullOrEmpty(androidAdUnitId) ? androidAdUnitId : iosAdUnitId);
                break;
            default:
                Debug.LogWarning("Mediation service is not available for this platform:" + Application.platform);
                return;
        }

        // Load Events
        m_RewardedAd.OnLoaded += AdLoaded;
        m_RewardedAd.OnFailedLoad += AdFailedLoad;

        // Show Events
        m_RewardedAd.OnUserRewarded += UserRewarded;
        m_RewardedAd.OnClosed += AdClosed;

        Debug.Log($"Initialized On Start. Loading Ad...");

        LoadAd();
    }

    void InitializationFailed(Exception error)
    {
        SdkInitializationError initializationError = SdkInitializationError.Unknown;
        if (error is InitializeFailedException initializeFailedException)
        {
            initializationError = initializeFailedException.initializationError;
        }
        Debug.Log($"Initialization Failed: {initializationError}:{error.Message}");
    }

    void UserRewarded(object sender, RewardEventArgs e)
    {
        RewardDouble();
    }

    void AdClosed(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Closed! Loading Ad...");
    }

    void AdLoaded(object sender, EventArgs e)
    {
        Debug.Log("Ad loaded");

        adStateDisplay.color = Color.green; // Se pudo cargar el Ad
    }

    void AdFailedLoad(object sender, LoadErrorEventArgs e)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(e.Message);

        adStateDisplay.color = Color.magenta; // No se pudo cargar el Ad
    }

    void ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        Debug.Log($"Impression event from ad unit id {args.AdUnitId} : {impressionData}");
    }

    private void FixedUpdate()
    {
        if (PremiumData.hasNoAds)
        {
            adStateDisplay.color = new Color(1, 1, 1, 0);
        }
    }

    void RewardDouble()
    {
        print("Doubled coins");

        coinsPerDistance.earnedMoneysDisplay.text = $"{coinsPerDistance.coinsEarnedThisRun * 2}";
        EconomyData.AddCoins(coinsPerDistance.coinsEarnedThisRun);

        button.interactable = false;
        checkOutObj.SetActive(true);
    }
}
