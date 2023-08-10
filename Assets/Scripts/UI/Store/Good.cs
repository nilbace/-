using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Good : MonoBehaviour
{
    int goodCountToday = 0;
    int maxCount = 5;
    [SerializeField] TextMeshProUGUI goodCountText;
    [SerializeField] Button phurchaseBtn;

    DateTime countClearTime;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 시간이 다음 초기화 시간보다 크면 초기화
        if (DateTime.Now >= countClearTime)
        {
            ResetCount();
            countClearTime = countClearTime.AddDays(1);
        }
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        TimeSpan calTime = countClearTime - DateTime.Now;
        goodCountText.text = goodCountToday + " / " + maxCount;
        if (goodCountToday >= maxCount)
        {
            phurchaseBtn.interactable = false;
        }
    }

    private void ResetCount()
    {
        goodCountToday = 0;
        phurchaseBtn.interactable = true;
    }

    private void LoadData()
    {
        goodCountToday = PlayerPrefs.GetInt("goodCountToday", 0);
        long ticks = Convert.ToInt64(PlayerPrefs.GetString("countClearTime", DateTime.Today.AddDays(1).Ticks.ToString()));
        countClearTime = new DateTime(ticks);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("goodCountToday", goodCountToday);
        PlayerPrefs.Save();
    }

    public void PurchaseGood()//고급상자 구매
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if(UnityEngine.Random.Range(0, 100) < 10)
        {
                Managers.Data.MakeAndAddMail(
            /* 코인 */UnityEngine.Random.Range(10000, 55001),
            /* ruby */330,
            /* bell */0,
            /* 소탕권 */SweepRandomValue(),
            /* skip */0,
            /* 부활권 */UnityEngine.Random.Range(5, 21),
            /* text */"상점 구매 상품(고급 상자)");
        }
        else
        {
                Managers.Data.MakeAndAddMail(
            /* 코인 */UnityEngine.Random.Range(10000, 55001),
            /* ruby */100,
            /* bell */0,
            /* 소탕권 */SweepRandomValue(),
            /* skip */0,
            /* 부활권 */UnityEngine.Random.Range(5, 21),
            /* text */"상점 구매 상품(고급 상자)");
        }

        goodCountToday++;
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public int SweepRandomValue()
    {
        int rand = UnityEngine.Random.Range(0, 100); // 1부터 100까지의 랜덤 숫자 생성

        if (rand <= 20)
        {
            return 15;
        }
        else if (rand <= 50)
        {
            return 10;
        }
        else if (rand <= 100)
        {
            return 2;
        }
        else
        {
            // 여기에 처리할 경우의 수 추가
            return 0;
        }

    }

    public void SuccessBuy()
    //결제 성공시 팝업창 뜨는것
    {

        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
    }

    public void FailBuy()
    //결제 실패시 팝업창 뜨는것
    {
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Fail);
    }

    public void CloseBTn()
    {
        SaveData();
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }
}
