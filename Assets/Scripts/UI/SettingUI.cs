using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] Slider[] sliders;
    [SerializeField] TMPro.TMP_InputField couponField;
    void Start()
    {
        couponField.text = "쿠폰을 입력하세요";
        SetSettingPannel();
    }


    void SetSettingPannel()
    {
        

        sliders[0].value = Managers.Data.MySettingData.BGMSound;
        sliders[1].value = Managers.Data.MySettingData.SFXSound;
    }

    public void OpenInsta()
    {
        Application.OpenURL("https://www.instagram.com/na0ojima_official/");
    }



    public void OnBGMSliderValueChanged()
    {
        if (sliders[0].value != Managers.Data.MySettingData.BGMSound)
        {
            // 값이 변경되었으므로 저장
            Managers.Data.MySettingData.BGMSound = sliders[0].value;
            Managers.Data.SaveSettingData();
        }
    }

    public void OnSFXSliderValueChanged()
    {
        if (sliders[1].value != Managers.Data.MySettingData.SFXSound)
        {
            // 값이 변경되었으므로 저장
            Managers.Data.MySettingData.SFXSound = sliders[1].value;
            Managers.Data.SaveSettingData();
        }
    }

    public void DevsBTN()
    {
        Managers.UI.ShowPopup(Define.Popup.Devs);
    }

    public void CloseBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }

    public void InputEnd()
    {
        if (couponField.text == "Nyaongjima" && Managers.Data.MyStoreData.NyaongjimaCouponUsed == false)
        {
            couponField.text = "상품이 지급되었습니다!";
            Managers.Data.MakeAndAddMail(1000, 10, 0, 0, 0, 10, "찾아와줘서 감사하다냥");
            Managers.Data.MyStoreData.NyaongjimaCouponUsed = true;
            Managers.Data.SaveAllDatas();
        }
    }
}
