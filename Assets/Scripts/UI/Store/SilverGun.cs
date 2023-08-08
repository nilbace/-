using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SilverGun : MonoBehaviour
{
    [SerializeField] Button CloseBTN;
    

    // Start is called before the first frame update
    void Start()
    {
        CloseBTN.onClick.AddListener(() => Managers.UI.ClosePopup());
    }

    public Button SilverGunBTN;
    private DateTime lastButtonPressTime;

    private void UpdateButtonState()
    {
        // 버튼을 누른 시간과 현재 시간의 차이 계산
        TimeSpan timeSinceLastPress = DateTime.Now - lastButtonPressTime;

        // 하루가 지났는지 확인
        if (timeSinceLastPress.TotalDays >= 1)
        {
            SilverGunBTN.interactable = true; // 버튼 활성화
        }
        else
        {
            SilverGunBTN.interactable = false; // 버튼 비활성화
        }
    }

    private void OnButtonClick()
    {
        // 현재 시간 저장
        lastButtonPressTime = DateTime.Now;

        // 버튼 동작 실행
        ExecuteButtonAction();

        // 버튼 활성화 여부 설정
        UpdateButtonState();

        // 이전에 버튼을 누른 시간 저장 (실제 게임 종료 시 사용하십시오)
        SaveLastButtonPressTime(lastButtonPressTime);
    }


    private void ExecuteButtonAction()
    {
        Debug.Log("Button Clicked!");
    }

    private void SaveLastButtonPressTime(DateTime time)
    {
        // 이전에 버튼을 누른 시간을 저장하는 코드 (예: PlayerPrefs)
        PlayerPrefs.SetString("LastButtonPressTime", time.ToString());
        PlayerPrefs.Save();
    }

    private DateTime LoadLastButtonPressTime()
    {
        // 이전에 버튼을 누른 시간을 불러오는 코드 (예: PlayerPrefs)
        string savedTime = PlayerPrefs.GetString("LastButtonPressTime", DateTime.MinValue.ToString());
        return DateTime.Parse(savedTime);
    }

}
