using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBTN : MonoBehaviour
{
    [SerializeField] Slider[] sliders;
    [SerializeField] 
    void Start()
    {
        sliders[0].value = Managers.Data.MySettingData.BGMSound;
        sliders[1].value = Managers.Data.MySettingData.SFXSound;
    }

    public void ContinueBTN()
    {
        Time.timeScale = 1f;
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }

    public void QuitBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.Quit);
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
}
