using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

/*
 * < �ٸ���ũ��Ʈ���� ���� ȣ���ϴ¹� >
 * 
   AdManager.instance.ShowAd();
   AdManager.instance.RewardBackEvent += ������������̴���Լ���;
 * 
 * 
 * < ���� ����Ǵ� �Լ� ���� >
 * 
   void ������������̴���Լ���(object sender, System.EventArgs e)
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
            //�ʱ�ȭ �Ϸ�
        });

#if UNITY_ANDROID
        adUnitId = "ca-app-pub-5263245376517832/5897856879";//�Ŀ����� ID
            //"ca-app-pub-3940256099942544/5224354917"; //������ ���� ����ID
            //ca-app-pub-5263245376517832~2279457320 //������ ��ûID
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
             "532F3560B55E6AB6CE179F300918D961"//������ Zflip4
         }).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        LoadRewardedAd();
    }

    public void LoadRewardedAd() //���� �ε� �ϱ�
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");
        //AndroidToast.I.ShowToastMessage("���� �ε���");

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
                    AndroidToast.I.ShowToastMessage("���� �ε� ���� " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());
                //AndroidToast.I.ShowToastMessage("���� �ε� ����");

                rewardedAd = ad;
                ad.OnAdFullScreenContentClosed += () =>
                {
                    close = true;
                    //if(watch) EarnedReward();
                    watch = false;
                    close = false;
                    Debug.Log("���� ���� ����");
                    //AndroidToast.I.ShowToastMessage("���� ���� ����");
                    // Reload the ad so that we can show another as soon as possible.
                    LoadRewardedAd();
                };
                // Raised when the ad failed to open full screen content.
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.LogError("Rewarded ad failed to open full screen content " +
                                   "with error : " + error);
                    AndroidToast.I.ShowToastMessage("���� ���� ���� " + error);

                    // Reload the ad so that we can show another as soon as possible.
                    LoadRewardedAd();
                };
            });
    }

    public void ShowAd() //���� ����
    {
        // const string rewardMsg =
        //     "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        
         if (rewardedAd != null && rewardedAd.CanShowAd())
         {
             rewardedAd.Show((Reward reward) =>
             {
                 //���� ȹ���ϱ�
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
            RewardBackEvent(this, EventArgs.Empty); // �̺�Ʈ �ڵ鷯���� ȣ�� 
            Debug.Log("�̺�Ʈ �ڵ鷯���� ȣ�� �Ϸ�");

        }
        else
        {
            Debug.Log("����� �̺�Ʈ ����");
        }
        RewardBackEvent = null;

        //�׽�Ʈ����
        //Managers.Data.MyStoreData.MyRubyAmount += 10;
        //Managers.Data.MyBellData.NowBellCount += 5;
        //Managers.Data.SaveAllDatas();
        //�׽�Ʈ����
        Debug.Log("���� �Ϸ�");
        AndroidToast.I.ShowToastMessage("���� �Ϸ�");
    }
}


