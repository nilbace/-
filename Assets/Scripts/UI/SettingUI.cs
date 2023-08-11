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

    private void ValidateCoupon(string coupon)
    {
        Coupons couponEnum;
        if (System.Enum.TryParse(coupon, true, out couponEnum))
        {
            if (!IsCouponUsed(couponEnum))
            {
                // 쿠폰 사용 처리
                UseCoupon(couponEnum);
                couponField.text = "상품이 지급되었습니다!";
            }
            else
            {
                couponField.text = "사용된 쿠폰입니다";
            }
        }
        else
        {
            Debug.Log("존재하지 않는 쿠폰입니다");
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
            couponField.text = "상품이 지급되었습니다!";
            Managers.Data.MakeAndAddMail(1000, 10, 0, 0, 0, 10, "찾아와줘서 감사하다냥");
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
        bangalS2
    }

    private bool IsCouponUsed(Coupons coupon)
    {
        // 쿠폰 사용 여부 확인
        return PlayerPrefs.GetInt(coupon.ToString(), 0) == 1;
    }

    private void UseCoupon(Coupons coupon)
    {
        couponField.text = "상품이 지급되었습니다!";

        switch (coupon)
        {
            case Coupons.fnqlwhdk:
                Managers.Data.MakeAndAddMail(0, 10, 0, 0, 0, 0, "2스테이지 클리어를 축하드립니다");
                break;

            case Coupons.Tkfkdgo:
                Managers.Data.MakeAndAddMail(0, 15, 0, 0, 0, 0, "4스테이지 클리어를 축하드립니다");
                break;

            case Coupons.rhakdnj:
                Managers.Data.MakeAndAddMail(0, 20, 0, 0, 0, 0, "6스테이지 클리어를 축하드립니다");
                break;

            case Coupons.Godqhrgo:
                Managers.Data.MakeAndAddMail(0, 25, 0, 0, 0, 0, "8스테이지 클리어를 축하드립니다");
                break;

            case Coupons.Ehakssk:
                Managers.Data.MakeAndAddMail(0, 30, 0, 0, 0, 0, "10스테이지 클리어를 축하드립니다");
                break;

            case Coupons.kimtae05080:
                Managers.Data.MakeAndAddMail(1000, 0, 0, 0, 0, 0, "오픈 1일차 기념쿠폰");
                break;

            case Coupons.Kimji03030:
                Managers.Data.MakeAndAddMail(0, 0, 5, 0, 0, 0, "오픈 2일차 기념쿠폰");
                break;

            case Coupons.choira03300:
                Managers.Data.MakeAndAddMail(0, 0, 0, 2, 0, 0, "오픈 3일차 기념쿠폰");
                break;

            case Coupons.leehun01250:
                Managers.Data.MakeAndAddMail(0, 30, 0, 0, 0, 0, "오픈 4일차 기념쿠폰");
                break;

            case Coupons.im09110:
                Managers.Data.MakeAndAddMail(5000, 0, 5, 0, 0, 0, "오픈 5일차 기념쿠폰");
                break;

            case Coupons.youji05100:
                Managers.Data.MakeAndAddMail(0, 3, 0, 10, 0, 10, "오픈 6일차 기념쿠폰");
                break;

            case Coupons.park06090:
                Managers.Data.MakeAndAddMail(0, 100, 0, 0, 0, 0, "오픈 7일차 기념쿠폰");
                break;

            case Coupons.eight_seven:
                Managers.Data.MakeAndAddMail(0, 100000, 0, 0, 0, 0, "개발자치트");
                break;

            case Coupons.Gamethon_5:
                Managers.Data.MakeAndAddMail(0, 300, 30, 0, 0, 0, "게임톤 5기");
                break;

            case Coupons.bangalS2:
                Managers.Data.MyCharDatas.charSaveDatas[4].bought = true;
                break;
        }
        Managers.Data.SaveAllDatas();

        // 쿠폰 사용 상태 저장
        PlayerPrefs.SetInt(coupon.ToString(), 1);
    }
}