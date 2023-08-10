using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Revive : MonoBehaviour
{
    [SerializeField] Sprite ReviveTicketBTNIMg;
    [SerializeField] Button ReviveBTN;
    [SerializeField] TMPro.TMP_Text rubyValueText;

    private void Start()
    {
        SetButton();
    }
    public void ReviveWithAD()
    {

    }

    public void StageFail()
    {
        Managers.UI.ShowPopup(Define.Popup.StageFail);
    }

    void SetButton()
    {
        if(GameScene.instance.hasReviveCoupon)
        {
            ReviveBTN.GetComponent<Image>().sprite = ReviveTicketBTNIMg;
            rubyValueText.text = Managers.Data.MyStoreData.MyReviveTicKetAmount.ToString();
            ReviveBTN.onClick.AddListener(() => ReviveWithTicket());
        }
        else
        {
            rubyValueText.text = "";
            UpdateRubyValue();
            ReviveBTN.onClick.AddListener(() =>ReviveWithRuby(rubyValue));
        }
    }

    private DateTime lastButton2PressTime;
    int rubyValue = 10;

    private void UpdateRubyValue()
    {
        lastButton2PressTime =  LoadLastButtonPressTime();
        // 버튼을 누른 시간과 현재 시간의 차이 계산
        TimeSpan timeSinceLastPress = DateTime.Now - lastButton2PressTime;

        // 하루가 지났는지 확인
        if (timeSinceLastPress.TotalDays >= 1)
        {
            rubyValue = 10;
            SaveRubyValue(rubyValue);
            SaveLastButtonPressTime(DateTime.Now);
        }
        else
        {
            rubyValue = LoadRubyValue();
            
            rubyValueText.text = rubyValue.ToString();
        }
        if(Managers.Data.MyStoreData.MyRubyAmount < rubyValue)
        {
            ReviveBTN.interactable = false;
        }
    }


    private void SaveLastButtonPressTime(DateTime time)
    {
        // 이전에 버튼을 누른 시간을 저장하는 코드 (예: PlayerPrefs)
        PlayerPrefs.SetString("lastButton2PressTime", time.ToString());
        PlayerPrefs.Save();
    }

    public static void SaveRubyValue(int value)
    {
        PlayerPrefs.SetInt("RubyKey", value);
    }

    public static int LoadRubyValue()
    {
        return PlayerPrefs.GetInt("RubyKey", 10);
    }

    private DateTime LoadLastButtonPressTime()
    {
        string savedTime = PlayerPrefs.GetString("lastButton2PressTime", DateTime.MinValue.ToString());
        return DateTime.Parse(savedTime);
    }

    void ReviveWithTicket()
    {
        Managers.Data.MyStoreData.MyReviveTicKetAmount--;
        Managers.Data.SaveAllDatas();
        GameScene.instance.Revive();
    }

    void ReviveWithRuby(int value)
    {
        Managers.Data.MyStoreData.MyRubyAmount -= value;
        if (rubyValue < 100)
            rubyValue += 10;
        SaveRubyValue(rubyValue);
        Managers.Data.SaveAllDatas();
        GameScene.instance.Revive();
    }
}
