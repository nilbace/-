using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] Sprite[] truefalseImg;
    [SerializeField] Slider[] sliders;
    void Start()
    {
        SetSettingPannel();
    }


    void SetSettingPannel()
    {
        

        sliders[0].value = Managers.Data.MySettingData.BGMSound;
        sliders[1].value = Managers.Data.MySettingData.SFXSound;
    }

    public void ClickJoystickFixBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MySettingData.isFixedJoystick = !Managers.Data.MySettingData.isFixedJoystick;
    
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
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
