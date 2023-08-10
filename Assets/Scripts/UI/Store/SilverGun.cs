using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SilverGun : MonoBehaviour
{
    [SerializeField] Button buyFreeBTN;
    [SerializeField] Button buyAdBTN;
    [SerializeField] GameObject coolTimeBox;
    [SerializeField] TextMeshProUGUI coolTimeBoxText;

    int silverBuyCount;
    DateTime silverCooltime;
    // Start is called before the first frame update
    void Start()
    {
        silverBuyCount = PlayerPrefs.GetInt("silverBuyCount", 0); // ���ѻ��� ī��Ʈ
        Debug.Log(silverBuyCount);

        long ticks = Convert.ToInt64(PlayerPrefs.GetString("silverCooltime", DateTime.Today.Ticks.ToString()));
        silverCooltime = new DateTime(ticks);
    }
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(silverBuyCount == 0)
        {
            //���ᱸ�Ű���
            buyFreeBTN.gameObject.SetActive(true);
            buyAdBTN.gameObject.SetActive(false);
            coolTimeBox.SetActive(false);
        }
        else if(silverBuyCount > 0 && DateTime.Now > silverCooltime)
        {
            //Debug.Log("������ ����" + DateTime.Now + "/" + silverCooltime);
            //������ ����
            buyFreeBTN.gameObject.SetActive(false);
            buyAdBTN.gameObject.SetActive(true);
            buyAdBTN.interactable = true;
            coolTimeBox.SetActive(false);
        }
        else if (silverBuyCount > 0 && DateTime.Now < silverCooltime)
        {
            //Debug.Log("������Ÿ����" + DateTime.Now + "/" + silverCooltime);
            //������Ÿ����
            buyFreeBTN.gameObject.SetActive(false);
            buyAdBTN.gameObject.SetActive(true);
            buyAdBTN.interactable = false;
            coolTimeBox.SetActive(true);

            TimeSpan calTime = silverCooltime - DateTime.Now;
            coolTimeBoxText.text = calTime.Hours + "h" + calTime.Minutes + "m";
        }
    }

    public void ExecuteButtonAction()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);

        buySilverGun();
        silverCooltime = DateTime.Now;
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
    }

    public void ShowAdButton()
    {
        AdManager.instance.ShowAd();
        AdManager.instance.RewardBackEvent += BuyAfterAd;
    }

    void BuyAfterAd(object sender, EventArgs e)
    {
        buySilverGun();
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
        silverCooltime = DateTime.Now.AddHours(4);
        PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
    }

    void buySilverGun()
    {
        int ruby = UnityEngine.Random.Range(1, 11);
        int gold = GenerateRandomValue();

        if (ruby == 1)
            Managers.Data.MakeAndAddMail(gold, 30, 0, 0, 0, 0, "���� ���� ��ǰ (���� ����)");
        else
        {
            Managers.Data.MakeAndAddMail(gold, 0, 0, 0, 0, 0, "���� ���� ��ǰ (���� ����)");
        }
        silverBuyCount++;
        Debug.Log(silverBuyCount);
        PlayerPrefs.SetInt("silverBuyCount", silverBuyCount);
        PlayerPrefs.Save();
        Managers.Data.SaveAllDatas();
    }

    public int GenerateRandomValue()
    {
        int rand = UnityEngine.Random.Range(1, 101); // 1���� 100������ ���� ���� ����

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
            // ���⿡ ó���� ����� �� �߰�
            return 0;
        }

    }
    public void CloseBTn()
    {
        PlayerPrefs.SetInt("silverBuyCount", silverBuyCount);
        PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
        PlayerPrefs.Save();
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }

}
