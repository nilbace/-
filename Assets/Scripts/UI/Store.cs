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
    {
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
    }

    void FailBuy()
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
            Managers.Data.MyBellData.NowBellCount += 5;
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
            Managers.Data.MyStoreData.MySkipCouponAmount++;
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
            Managers.Data.MyStoreData.MySkipCouponAmount+= 10;
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
        Managers.Data.MakeAndAddMail(0, n, 0, 0, 0, 0, $"{n}루비 패키지");
    }

    public void CloseBTn()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
