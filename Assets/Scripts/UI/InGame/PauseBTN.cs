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
        
    }

    public void ContinueBTN()
    {
        Time.timeScale = 1f;
        Managers.UI.ClosePopup();
    }

    public void QuitBTN()
    {
        Managers.UI.ShowPopup(Define.Popup.Quit);
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
}
