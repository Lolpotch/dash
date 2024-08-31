using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAd : MonoBehaviour, IUnityAdsListener
{
    string rewardedVideoID = "rewardedVideo";
    string skippableVideoID = "video";
    string googlePlay_ID = "3830749";

    void Awake()
    {
        InitAds();
    }

    void InitAds()
    {
        Debug.Log("Ad initialized!");
        Advertisement.AddListener(this);
        Advertisement.Initialize(googlePlay_ID);
    }

    void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    public void ShowSkippableAd()
    {
        Advertisement.Show(skippableVideoID);
    }
   

    //Button Methods
    public void ShowAd()
    {
        Advertisement.Show();
    }

    public void ShowVideoAd()
    {
        Advertisement.Show(rewardedVideoID);
    }



    #region script made by Unity

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == rewardedVideoID)
        {
            //myButton.interacta ble = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("You get the REWARD");
            Advertisement.RemoveListener(this);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.LogWarning("You DON'T get the REWARD");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }

    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
    #endregion
}
