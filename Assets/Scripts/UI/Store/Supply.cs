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
        // ���� �ð��� ���� �ʱ�ȭ �ð����� ũ�� �ʱ�ȭ
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

    public void PurchaseSupply()//���޻��� ����
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if (UnityEngine.Random.Range(0, 10) < 1) // 10%Ȯ���� ������ 2������
        {
            Managers.Data.MakeAndAddMail(
                UnityEngine.Random.Range(1000, 5001),
                0,
                0,
                2,
                0,
                UnityEngine.Random.Range(1, 6),
                "���� ���� ��ǰ(���� ����)");
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
                "���� ���� ��ǰ(���� ����)");
        }
        supplyCountToday++;
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public void SuccessBuy()
    //���� ������ �˾�â �ߴ°�
    {
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
    }

    public void FailBuy()
    //���� ���н� �˾�â �ߴ°�
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
