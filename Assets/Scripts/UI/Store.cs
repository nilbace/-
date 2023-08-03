using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public void Bell5Ruby30()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (Managers.Data.MyStoreData.MyRubyAmount >= 30)
        {
            Managers.Data.MyStoreData.MyRubyAmount -= 30;
            Managers.Data.MyBellData.NowBellCount += 5;
            Managers.Data.SaveAllDatas();
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
        }
    }

    public void StarterPackage()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(10000, 100, 0, 0, 0,0, "스타터 패키지");
    }

    public void RichPackAge()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MakeAndAddMail(30000, 300, 0, 0, 0, 0, "부자 패키지");
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
