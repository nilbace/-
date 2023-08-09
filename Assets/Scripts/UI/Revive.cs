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
            UpdateRubyValue();
            ReviveBTN.onClick.AddListener(() =>ReviveWithRuby(rubyValue));
        }
    }

    private DateTime lastButton2PressTime;
    int rubyValue = 10;

    private void UpdateRubyValue()
    {
        lastButton2PressTime =  LoadLastButtonPressTime();
        // ��ư�� ���� �ð��� ���� �ð��� ���� ���
        TimeSpan timeSinceLastPress = DateTime.Now - lastButton2PressTime;

        // �Ϸ簡 �������� Ȯ��
        if (timeSinceLastPress.TotalDays >= 1)
        {
            rubyValue = 10;
            SaveRubyValue(rubyValue);
            SaveLastButtonPressTime(DateTime.Now);
        }
        else
        {
            rubyValue = LoadRubyValue();
            if (rubyValue < 100)
                rubyValue += 10;
            SaveRubyValue(rubyValue);
            rubyValueText.text = rubyValue.ToString();
        }
        if(Managers.Data.MyStoreData.MyRubyAmount < rubyValue)
        {
            ReviveBTN.interactable = false;
        }
    }


    private void SaveLastButtonPressTime(DateTime time)
    {
        // ������ ��ư�� ���� �ð��� �����ϴ� �ڵ� (��: PlayerPrefs)
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
        Managers.Data.SaveAllDatas();
        GameScene.instance.Revive();
    }
}
