using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Supply : MonoBehaviour
{
    int supplyCountToday = 0;
    int maxCount = 3;
    [SerializeField] TextMeshProUGUI supplyCountText;
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
            countClearTime = DateTime.Today.AddDays(1);
        }
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        TimeSpan calTime = countClearTime - DateTime.Now;
        supplyCountText.text = supplyCountToday + " / " + maxCount;
        if(supplyCountToday >= maxCount)
        {
            phurchaseBtn.interactable = false;
        }
    }

    private void ResetCount()
    {
        supplyCountToday = 0;
        phurchaseBtn.interactable = true;
    }

    private void LoadData()
    {
        supplyCountToday = PlayerPrefs.GetInt("supplyCountToday", 0);
        long ticks = Convert.ToInt64(PlayerPrefs.GetString("countClearTime", DateTime.Today.AddDays(1).Ticks.ToString()));
        countClearTime = new DateTime(ticks);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("supplyCountToday", supplyCountToday);
        PlayerPrefs.Save();
    }

    public void PurchaseSupply()//보급상자 구매
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (UnityEngine.Random.Range(0, 10) < 1) // 10%확률로 소탕권 2개지급
        {
            Managers.Data.MakeAndAddMail(
                UnityEngine.Random.Range(1000, 5001),
                0,
                0,
                2,
                0,
                UnityEngine.Random.Range(1, 6),
                "상점 구매 상품(보급 상자)");
        }
        else
        {
            Managers.Data.MakeAndAddMail(
                UnityEngine.Random.Range(1000, 5001),
                0,
                0,
                0,
                0,
                UnityEngine.Random.Range(1, 6),
                "상점 구매 상품(보급 상자)");
        }
        supplyCountToday++;
        Managers.Data.SaveAllDatas();
        SuccessBuy();
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
