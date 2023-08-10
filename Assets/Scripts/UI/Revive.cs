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

    [SerializeField] Button SecondBTN;
    [SerializeField] Sprite[] seImgs;

    private void Start()
    {
        SetButton();
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

        if(Managers.Data.MyStoreData.MySkipCouponAmount > 0 && Managers.Data.MyStoreData.SkipAdActive)
        {
            SecondBTN.GetComponent<Image>().sprite = seImgs[0];
            SecondBTN.onClick.AddListener(ReviveWithTicket);
        }
        else if(Managers.Data.MyStoreData.SkipAdActive)
        {
            SecondBTN.GetComponent<Image>().sprite = seImgs[0];
            SecondBTN.onClick.AddListener(ReviveWithADSkip);
        }
        else
        {
            SecondBTN.GetComponent<Image>().sprite = seImgs[0];
            //�߰� �ٶ�
            SecondBTN.onClick.AddListener(ReviveWithAd);
        }

    }

    #region withRuby

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
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
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

    #endregion

    #region withAD

    void ReviveWithSkipTicket()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MyStoreData.MySkipCouponAmount--;
        Managers.Data.SaveAllDatas();
        GameScene.instance.Revive();
    }

    void ReviveWithADSkip()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        GameScene.instance.Revive();
    }

    void ReviveWithAd()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        //���� ���� �� �ڿ� ���͸� ����Ǹ� ��
        GameScene.instance.Revive();
    }

    #endregion
}
