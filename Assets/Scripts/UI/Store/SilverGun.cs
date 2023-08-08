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
        //닫힘 버튼
        CloseBTN.onClick.AddListener(() => Managers.UI.ClosePopup());



        SilverGunBTN.onClick.AddListener(OnButtonClick);

        // 이전에 버튼을 누른 시간 불러오기 (실제 게임 로드할 때 사용하십시오)
        lastButtonPressTime = LoadLastButtonPressTime();

        
    }

    public Button SilverGunBTN;
    private DateTime lastButtonPressTime;



    private void OnButtonClick()
    {
        // 현재 시간 저장
        lastButtonPressTime = DateTime.Now;

        // 버튼 동작 실행
        ExecuteButtonAction();


        // 이전에 버튼을 누른 시간 저장 (실제 게임 종료 시 사용하십시오)
        SaveLastButtonPressTime(lastButtonPressTime);
    }


    private void ExecuteButtonAction()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);

        TimeSpan timeSinceLastPress = DateTime.Now - lastButtonPressTime;

        if (timeSinceLastPress.TotalDays >= 1)
        {
            buySilverGun();
            Managers.UI.ShowPopup(Define.Popup.PaySuccess);
            Pays.instance.Setting(Pays.Result.Success);
        }
        else
        {
            Managers.UI.ShowPopup(Define.Popup.PaySuccess);
            Pays.instance.Setting(Pays.Result.Fail);
        }
        
    }

    void buySilverGun()
    {
        int ruby = UnityEngine.Random.Range(1, 11);
        int gold = GenerateRandomValue();

        if (ruby == 1)
            Managers.Data.MakeAndAddMail(gold, 30, 0, 0, 0, 0, "상점 구매 상품 (은총 상자)");
        else
        {
            Managers.Data.MakeAndAddMail(gold, 0, 0, 0, 0, 0, "상점 구매 상품 (은총 상자)");
        }

        Managers.Data.SaveAllDatas();
    }

    public int GenerateRandomValue()
    {
        int rand = UnityEngine.Random.Range(1, 101); // 1부터 100까지의 랜덤 숫자 생성

        if (rand <= 10)
        {
            return 3000;
        }
        else if (rand <= 40)
        {
            return 1000;
        }
        else if (rand <= 100)
        {
            return 500;
        }
        else
        {
            // 여기에 처리할 경우의 수 추가
            return 0;
        }

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
