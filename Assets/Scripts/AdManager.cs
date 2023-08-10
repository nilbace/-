using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

/*
 * < 다른스크립트에서 광고 호출하는법 >
 * 
   AdManager.instance.ShowAd();
   AdManager.instance.RewardBackEvent += 보상받을내용이담긴함수명;
 * 
 * 
 * < 보상 연결되는 함수 형태 >
 * 
   void 보상받을내용이담긴함수명(object sender, System.EventArgs e)
 */

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private RewardedAd rewardedAd;
    public event EventHandler RewardBackEvent;

    string adUnitId;
    bool watch;
    bool close;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        watch = false;
        close = false;

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //초기화 완료
        });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917"; //보상형 광고 샘플ID
            //ca-app-pub-5263245376517832~2279457320 //보상형 신청ID
#elif UNITY_IOS
            adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            adUnitId = "unexpected_platform";
#endif
        RequestConfiguration requestConfiguration =
         new RequestConfiguration.Builder()
         .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
         .SetTestDeviceIds(new List<string>()
         {
             "532F3560B55E6AB6CE179F300918D961"//최유라 Zflip4
         }).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        LoadRewardedAd();
    }

    public void LoadRewardedAd() //광고 로드 하기
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");
        AndroidToast.I.ShowToastMessage("광고 로딩중");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        
        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    AndroidToast.I.ShowToastMessage("광고 로드 실패 " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());
                //AndroidToast.I.ShowToastMessage("광고 로드 성공");

                rewardedAd = ad;
                ad.OnAdFullScreenContentClosed += () =>
                {
                    close = true;
                    //if(watch) EarnedReward();
                    watch = false;
                    close = false;
                    Debug.Log("광고 정상 닫음");
                    AndroidToast.I.ShowToastMessage("광고 정상 닫음");
                    // Reload the ad so that we can show another as soon as possible.
                    LoadRewardedAd();
                };
                // Raised when the ad failed to open full screen content.
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.LogError("Rewarded ad failed to open full screen content " +
                                   "with error : " + error);
                    AndroidToast.I.ShowToastMessage("광고 열기 실패 " + error);

                    // Reload the ad so that we can show another as soon as possible.
                    LoadRewardedAd();
                };
            });
    }

    public void ShowAd() //광고 보기
    {
        // const string rewardMsg =
        //     "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        
         if (rewardedAd != null && rewardedAd.CanShowAd())
         {
             rewardedAd.Show((Reward reward) =>
             {
                 //보상 획득하기
                 //Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                 EarnedReward();
                 watch = true;
             });
         }
        
    }

    public void EarnedReward()
    {
        if (RewardBackEvent != null)
        {
            RewardBackEvent(this, EventArgs.Empty); // 이벤트 핸들러들을 호출 
            Debug.Log("이벤트 핸들러들을 호출 완료");

        }
        else
        {
            Debug.Log("연결된 이벤트 없음");
        }
        RewardBackEvent = null;

        //테스트보상
        //Managers.Data.MyStoreData.MyRubyAmount += 10;
        //Managers.Data.MyBellData.NowBellCount += 5;
        //Managers.Data.SaveAllDatas();
        //테스트보상
        Debug.Log("보상 완료");
        AndroidToast.I.ShowToastMessage("보상 완료");
    }
}


