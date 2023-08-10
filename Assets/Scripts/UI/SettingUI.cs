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

    private void ValidateCoupon(string coupon)
    {
        Coupons couponEnum;
        if (System.Enum.TryParse(coupon, true, out couponEnum))
        {
            if (!IsCouponUsed(couponEnum))
            {
                // ���� ��� ó��
                UseCoupon(couponEnum);
                couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
            }
            else
            {
                couponField.text = "���� �����Դϴ�";
            }
        }
        else
        {
            Debug.Log("�������� �ʴ� �����Դϴ�");
        }
    }

    void RewardByIndex(Coupons coupon)
    {
        switch (coupon)
        {
            case Coupons.fnqlwhdk:
                break;


        }
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

        ValidateCoupon(couponField.text);

    }

    public enum Coupons
    {
        fnqlwhdk,
        Tkfkdgo,
        rhakdnj,
        Godqhrgo,
        Ehakssk,
        kimtae05080,
        Kimji03030,
        choira03300,
        leehun01250,
        im09110,
        youji05100,
        park06090,
        eight_seven,
        Gamethon_5,
    }

    private bool IsCouponUsed(Coupons coupon)
    {
        // ���� ��� ���� Ȯ��
        return PlayerPrefs.GetInt(coupon.ToString(), 0) == 1;
    }

    private void UseCoupon(Coupons coupon)
    {
        couponField.text = "��ǰ�� ���޵Ǿ����ϴ�!";
        switch (coupon)
        {
            case Coupons.fnqlwhdk:
                Managers.Data.MakeAndAddMail(0, 10, 0, 0, 0, 0, "2�������� Ŭ��� ���ϵ帳�ϴ�");
                break;

            case Coupons.Tkfkdgo:
                Managers.Data.MakeAndAddMail(0, 15, 0, 0, 0, 0, "4�������� Ŭ��� ���ϵ帳�ϴ�");
                break;

            case Coupons.rhakdnj:
                Managers.Data.MakeAndAddMail(0, 20, 0, 0, 0, 0, "6�������� Ŭ��� ���ϵ帳�ϴ�");
                break;

            case Coupons.Godqhrgo:
                Managers.Data.MakeAndAddMail(0, 25, 0, 0, 0, 0, "8�������� Ŭ��� ���ϵ帳�ϴ�");
                break;

            case Coupons.Ehakssk:
                Managers.Data.MakeAndAddMail(0, 30, 0, 0, 0, 0, "10�������� Ŭ��� ���ϵ帳�ϴ�");
                break;

            case Coupons.kimtae05080:
                Managers.Data.MakeAndAddMail(1000, 0, 0, 0, 0, 0, "���� 1���� �������");
                break;

            case Coupons.Kimji03030:
                Managers.Data.MakeAndAddMail(0, 0, 5, 0, 0, 0, "���� 2���� �������");
                break;

            case Coupons.choira03300:
                Managers.Data.MakeAndAddMail(0, 0, 0, 2, 0, 0, "���� 3���� �������");
                break;

            case Coupons.leehun01250:
                Managers.Data.MakeAndAddMail(0, 30, 0, 0, 0, 0, "���� 4���� �������");
                break;

            case Coupons.im09110:
                Managers.Data.MakeAndAddMail(5000, 0, 5, 0, 0, 0, "���� 5���� �������");
                break;

            case Coupons.youji05100:
                Managers.Data.MakeAndAddMail(0, 0, 0, 0, 0, 10, "���� 6���� �������");
                break;

            case Coupons.park06090:
                Managers.Data.MakeAndAddMail(0, 100, 0, 0, 0, 0, "���� 7���� �������");
                break;

            case Coupons.eight_seven:
                Managers.Data.MakeAndAddMail(0, 100000, 0, 0, 0, 0, "������ġƮ");
                break;

            case Coupons.Gamethon_5:
                Managers.Data.MakeAndAddMail(0, 300, 30, 0, 0, 0, "������ 5��");
                break;
        }
        Managers.Data.SaveAllDatas();

        // ���� ��� ���� ����
        PlayerPrefs.SetInt(coupon.ToString(), 1);
    }
}