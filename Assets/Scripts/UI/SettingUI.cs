using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] GameObject JoystickSet;
    [SerializeField] Sprite[] truefalseImg;
    [SerializeField] Slider[] sliders;
    void Start()
    {
        SetSettingPannel();
    }


    void SetSettingPannel()
    {
        if(Managers.Data.MySettingData.isFixedJoystick)
        {
            JoystickSet.GetComponent<Image>().sprite = truefalseImg[0];
        }
        else
        {
            JoystickSet.GetComponent<Image>().sprite = truefalseImg[1];
        }

        sliders[0].value = Managers.Data.MySettingData.BGMSound;
        sliders[1].value = Managers.Data.MySettingData.SFXSound;
    }

    public void ClickJoystickFixBTN()
    {
        Managers.Data.MySettingData.isFixedJoystick = !Managers.Data.MySettingData.isFixedJoystick;
        if(Managers.Data.MySettingData.isFixedJoystick)
        {
            JoystickSet.GetComponent<Image>().sprite = truefalseImg[0];
        }
        else
        {
            JoystickSet.GetComponent<Image>().sprite = truefalseImg[1];
        }
        Managers.Data.SaveSettingData();
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


    public void CloseBTN()
    {
        Managers.UI.ClosePopup();
    }
}
