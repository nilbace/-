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
    [SerializeField] TMP_Text mySkipCount;
 
    private void Start()
    {
        if (Managers.Data.MyStoreData.SkipAdActive)
            skipAddAfterActive.SetActive(true);

        if (Managers.Data.MyStoreData.PurchaseStarterPakage)
            starterAfterActive.SetActive(true);

        long ticks = Convert.ToInt64(PlayerPrefs.GetString("countClearTime", DateTime.Today.AddDays(1).Ticks.ToString()));
        countClearTime = new DateTime(ticks);

        mySkipCount.text = Managers.Data.MyStoreData.MySkipCouponAmount.ToString();
    }

    private void ResetCount()
    {
        Debug.Log("ResetCount");
        PlayerPrefs.SetInt("silverBuyCount", 0); // ���ѻ��� ī��Ʈ �ʱ�ȭ
        PlayerPrefs.SetInt("supplyCountToday", 0); // ���޻��� ī��Ʈ �ʱ�ȭ
        PlayerPrefs.SetInt("goodCountToday", 0);   // ��޻��� ī��Ʈ �ʱ�ȭ

        PlayerPrefs.Save();
    }

    void Update()
    {
        // ���� �ð��� ���� �ʱ�ȭ �ð����� ũ�� �ʱ�ȭ
        if (DateTime.Now > countClearTime)
        {
            ResetCount();
            countClearTime = DateTime.Today.AddDays(1);
            PlayerPrefs.SetString("countClearTime", countClearTime.Ticks.ToString()); // �ʱ�ȭ�ð� ����
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        //���ѻ��� ����/���� ��ȯ ǥ��
        if (PlayerPrefs.GetInt("silverBuyCount", 0) > 0)
        {
            silverGunAdUI.SetActive(true);
        }
        else
        {
            silverGunAdUI.SetActive(false);
        }
        //�ʱ�ȭŸ�� ǥ��
        TimeSpan calTime = countClearTime - DateTime.Now;
        clearCountText.text = "Ƚ�� �ʱ�ȭ ���� �ð� " + calTime.Hours + "h" + calTime.Minutes + "m";
    }

    public void SuccessBuy()
    //���� ������ �˾�â �ߴ°�
    {
        
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
    }

    public void FailBuy()
    //���� ���н� �˾�â �ߴ°�
    {
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Fail);
    }

    public void PurchaseAdSkip()//���������� ����
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MyStoreData.SkipAdActive = true;
        Managers.Data.SaveAllDatas();
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting("���� �Ϸ�, ���� ��� ���Ű� ����˴ϴ�.");
    }

    public void Bell5Ruby30()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (Managers.Data.MyStoreData.MyRubyAmount >= 30)
        {
            Managers.Data.MyStoreData.MyRubyAmount -= 30;
            Managers.Data.MakeAndAddMail(0, 0, 5, 0, 0, 0, "���� ���� ��ǰ(���)");
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
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 1, 0, "���� ���� ��ǰ(���� ��ŵ ����)");
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
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 10, 0, "���� ���� ��ǰ(���� ��ŵ ����)");
            Managers.Data.MyStoreData.MyGoldAmount -= 4500;
            Managers.Data.SaveAllDatas();
            SuccessBuy();
        }
        else
        {
            FailBuy();
        }
    }
    
    public void PayServices()//�غ����Դϴ� �˾�â
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Ready);
    }


    public void StarterPopup()//��Ÿ����Ű�� �� ���� Ȯ���ȳ�â
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.StarterBox);
    }

    public void PurchaseStarter()//��Ÿ����Ű�� ����
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if(UnityEngine.Random.Range(0, 10) < 1) // 10%Ȯ���� ������ 2������
        {
            Managers.Data.MakeAndAddMail(
                10000 + UnityEngine.Random.Range(1000, 5001), 
                100, 
                0, 
                2, 
                0, 
                UnityEngine.Random.Range(1, 6), 
                "���� ���� ��ǰ(��Ÿ�� ��Ű��)");
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
                "���� ���� ��ǰ(��Ÿ�� ��Ű��)");
        }
        Managers.Data.MyStoreData.PurchaseStarterPakage = true;
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public void PurchaseRich() //������Ű�� ����
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(30000, 300, 0, 0, 0, 0, "���� ���� ��ǰ(���� ��Ű��)");
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public void SilverGunOpen() //���ѻ��� ���ý� Ȯ���ȳ�â
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.SilverGun);
    }

    public void SupplyBoxOpen() //���޻��� ���ý� Ȯ���ȳ�â
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.SupplyBox);
    }



    public void GoodBoxOpen() //��޻��� ���ý� Ȯ���ȳ�â
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.GoodBox);//����� �ű�ȿ��� ���ſ����ʿ�
    }

    public void PurchaseRuby(int n) //��� ����
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(0, n, 0, 0, 0, 0, "���� ���� ��ǰ(��� x " + n + ")");
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public void TempAddRuby(int n)
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(0, n, 0, 0, 0, 0, $"{n}��� ��Ű��");
    }

    public void CloseBTn()
    {
        PlayerPrefs.SetString("countClearTime", countClearTime.Ticks.ToString());
        PlayerPrefs.Save();
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
