using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Store : MonoBehaviour
{

    private void Start()
    {
        
    }

    void SuccessBuy()
    //���� ������ �˾�â �ߴ°�
    {
        
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
    }

    void FailBuy()
    //���� ���н� �˾�â �ߴ°�
    {
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Fail);
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

    public void PayServices()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Ready);
    }


    public void SilverGunOpen()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.SilverGun);
    }
    


   

    public void TempAddRuby(int n)
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(0, n, 0, 0, 0, 0, $"{n}��� ��Ű��");
    }

    public void CloseBTn()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
