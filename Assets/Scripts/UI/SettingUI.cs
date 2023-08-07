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
        couponField.text = "������ �Է��ϼ���";
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
            // ���� ����Ǿ����Ƿ� ����
            Managers.Data.MySettingData.BGMSound = sliders[0].value;
            Managers.Data.SaveSettingData();
        }
    }

    public void OnSFXSliderValueChanged()
    {
        if (sliders[1].value != Managers.Data.MySettingData.SFXSound)
        {
            // ���� ����Ǿ����Ƿ� ����
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
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(1000, 10, 0, 0, 0, 10, "ã�ƿ��༭ �����ϴٳ�");
            Managers.Data.MyStoreData.NyaongjimaCouponUsed = true;
            Managers.Data.SaveAllDatas();
        }
    }
}
