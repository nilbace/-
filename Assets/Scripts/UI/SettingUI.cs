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

        for(int i = 0; i <Coupon7s.Length; i++)
        {
            Coupon7s[i] = PlayerPrefs.GetInt("Coupon7s_" + i, 0) == 1;
        }
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

    public bool[] Coupon7s;

    private void SaveCoupon7s()
    {
        for (int i = 0; i < Coupon7s.Length; i++)
        {
            PlayerPrefs.SetInt("Coupon7s_" + i, Coupon7s[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
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




        if (couponField.text == "kimjihyeon0303" && Coupon7s[0] == false)
        {
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 10, 0, "���±�� ���� 1");
            Coupon7s[0] = true;
            SaveCoupon7s();
            Managers.Data.SaveAllDatas();
        }

        if (couponField.text == "parkheejune0609" && Coupon7s[1] == false)
        {
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 0, 0, "���±�� ���� 1");
            Coupon7s[1] = true;
            SaveCoupon7s();
            Managers.Data.SaveAllDatas();
        }

        if (couponField.text == "kimjintae0508" && Coupon7s[2] == false)
        {
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 0, 0, "���� 2");
            Coupon7s[2] = true;
            SaveCoupon7s();
            Managers.Data.SaveAllDatas();
        }

        if (couponField.text == "your_coupon_code_3" && Coupon7s[3] == false)
        {
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 0, 0, "���� 3");
            Coupon7s[3] = true;
            SaveCoupon7s();
            Managers.Data.SaveAllDatas();
        }

        if (couponField.text == "your_coupon_code_4" && Coupon7s[4] == false)
        {
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 0, 0, "���� 4");
            Coupon7s[4] = true;
            SaveCoupon7s();
            Managers.Data.SaveAllDatas();
        }

        if (couponField.text == "your_coupon_code_5" && Coupon7s[5] == false)
        {
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 0, 0, "���� 5");
            Coupon7s[5] = true;
            SaveCoupon7s();
            Managers.Data.SaveAllDatas();
        }

        if (couponField.text == "your_coupon_code_6" && Coupon7s[6] == false)
        {
            couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            Managers.Data.MakeAndAddMail(0, 0, 0, 0, 0, 0, "���� 6");
            Coupon7s[6] = true;
            SaveCoupon7s();
            Managers.Data.SaveAllDatas();
        }

    }
}
