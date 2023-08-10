using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject skipAddAfterActive;
    [SerializeField] GameObject starterAfterActive;

    [SerializeField] TextMeshProUGUI clearCountText;
    DateTime countClearTime;

    [SerializeField] GameObject silverGunAdUI;
 
    private void Start()
    {
        if (Managers.Data.MyStoreData.SkipAdActive)
            skipAddAfterActive.SetActive(true);

        if (Managers.Data.MyStoreData.PurchaseStarterPakage)
            starterAfterActive.SetActive(true);

        long ticks = Convert.ToInt64(PlayerPrefs.GetString("countClearTime", DateTime.Today.AddDays(1).Ticks.ToString()));
        countClearTime = new DateTime(ticks);
    }

    private void ResetCount()
    {
        Debug.Log("ResetCount");
        PlayerPrefs.SetInt("silverBuyCount", 0); // 은총상자 카운트 초기화
        PlayerPrefs.SetInt("supplyCountToday", 0); // 보급상자 카운트 초기화
        PlayerPrefs.SetInt("goodCountToday", 0);   // 고급상자 카운트 초기화

        PlayerPrefs.Save();
    }

    void Update()
    {
        // 현재 시간이 다음 초기화 시간보다 크면 초기화
        if (DateTime.Now > countClearTime)
        {
            ResetCount();
            countClearTime = DateTime.Today.AddDays(1);
            PlayerPrefs.SetString("countClearTime", countClearTime.Ticks.ToString()); // 초기화시간 갱신
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        //은총상자 무료/광고 전환 표기
        if (PlayerPrefs.GetInt("silverBuyCount", 0) > 0)
        {
            silverGunAdUI.SetActive(true);
        }
        else
        {
            silverGunAdUI.SetActive(false);
        }
        //초기화타임 표기
        TimeSpan calTime = countClearTime - DateTime.Now;
        clearCountText.text = "횟수 초기화 남은 시간 " + calTime.Hours + "h" + calTime.Minutes + "m";
    }

    public void SuccessBuy()
    //결제 성공시 팝업창 뜨는것
    {
        
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
    }

    public void FailBuy()
    //결제 실패시 팝업창 뜨는것
    {
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Fail);
    }

    public void PurchaseAdSkip()//광고사라졌냥 구매
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MyStoreData.SkipAdActive = true;
        Managers.Data.SaveAllDatas();
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting("결제 완료, 광고 평생 제거가 적용됩니다.");
    }

    public void Bell5Ruby30()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (Managers.Data.MyStoreData.MyRubyAmount >= 30)
        {
            Managers.Data.MyStoreData.MyRubyAmount -= 30;
            Managers.Data.MakeAndAddMail(0, 0, 5, 0, 0, 0, "상점 구매 상품(방울)");
            Managers.Data.SaveAllDatas();
            SuccessBuy();
        }
        else
        {
            FailBuy();
        }
    }

    public void Skip1Gold500()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (Managers.Data.MyStoreData.MyGoldAmount >= 500)
        {
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 1, 0, "상점 구매 상품(광고 스킵 쿠폰)");
            Managers.Data.MyStoreData.MyGoldAmount -= 500;
            Managers.Data.SaveAllDatas();
            SuccessBuy();
        }
        else
        {
            FailBuy();
        }
        
    }

    public void skip10Gold4500()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (Managers.Data.MyStoreData.MyGoldAmount >= 4500)
        {
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 10, 0, "상점 구매 상품(광고 스킵 쿠폰)");
            Managers.Data.MyStoreData.MyGoldAmount -= 4500;
            Managers.Data.SaveAllDatas();
            SuccessBuy();
        }
        else
        {
            FailBuy();
        }
    }
    
    public void PayServices()//준비중입니다 팝업창
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Ready);
    }


    public void StarterPopup()//스타터패키지 내 상자 확률안내창
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.StarterBox);
    }

    public void PurchaseStarter()//스타터패키지 구매
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if(UnityEngine.Random.Range(0, 10) < 1) // 10%확률로 소탕권 2개지급
        {
            Managers.Data.MakeAndAddMail(
                10000 + UnityEngine.Random.Range(1000, 5001), 
                100, 
                0, 
                2, 
                0, 
                UnityEngine.Random.Range(1, 6), 
                "상점 구매 상품(스타터 패키지)");
        }
        else
        {
            Managers.Data.MakeAndAddMail(
                10000 + UnityEngine.Random.Range(1000, 5001), 
                100, 
                0, 
                0, 
                0, 
                UnityEngine.Random.Range(1, 6), 
                "상점 구매 상품(스타터 패키지)");
        }
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public void PurchaseRich() //부자패키지 구매
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(30000, 300, 0, 0, 0, 0, "상점 구매 상품(부자 패키지)");
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public void SilverGunOpen() //은총상자 선택시 확률안내창
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.SilverGun);
    }

    public void SupplyBoxOpen() //보급상자 선택시 확률안내창
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.SupplyBox);
    }



    public void GoodBoxOpen() //고급상자 선택시 확률안내창
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.GoodBox);//만들고 거기안에서 구매연결필요
    }

    public void PurchaseRuby(int n) //루비 구매
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(0, n, 0, 0, 0, 0, "상점 구매 상품(루비 x " + n + ")");
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public void TempAddRuby(int n)
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(0, n, 0, 0, 0, 0, $"{n}루비 패키지");
    }

    public void CloseBTn()
    {
        PlayerPrefs.SetString("countClearTime", countClearTime.Ticks.ToString());
        PlayerPrefs.Save();
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
