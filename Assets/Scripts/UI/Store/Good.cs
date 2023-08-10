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
        // ���� �ð��� ���� �ʱ�ȭ �ð����� ũ�� �ʱ�ȭ
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

    public void PurchaseGood()//��޻��� ����
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        if(UnityEngine.Random.Range(0, 100) < 10)
        {
                Managers.Data.MakeAndAddMail(
            /* ���� */UnityEngine.Random.Range(10000, 55001),
            /* ruby */330,
            /* bell */0,
            /* ������ */SweepRandomValue(),
            /* skip */0,
            /* ��Ȱ�� */UnityEngine.Random.Range(5, 21),
            /* text */"���� ���� ��ǰ(��� ����)");
        }
        else
        {
                Managers.Data.MakeAndAddMail(
            /* ���� */UnityEngine.Random.Range(10000, 55001),
            /* ruby */100,
            /* bell */0,
            /* ������ */SweepRandomValue(),
            /* skip */0,
            /* ��Ȱ�� */UnityEngine.Random.Range(5, 21),
            /* text */"���� ���� ��ǰ(��� ����)");
        }

        goodCountToday++;
        Managers.Data.SaveAllDatas();
        SuccessBuy();
    }

    public int SweepRandomValue()
    {
        int rand = UnityEngine.Random.Range(0, 100); // 1���� 100������ ���� ���� ����

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
            // ���⿡ ó���� ����� �� �߰�
            return 0;
        }

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
