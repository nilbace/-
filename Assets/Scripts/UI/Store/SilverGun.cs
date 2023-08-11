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
    [SerializeField] Sprite[] Imgs;

    int silverBuyCount;
    DateTime silverCooltime;
    // Start is called before the first frame update
    void Start()
    {
        silverBuyCount = PlayerPrefs.GetInt("silverBuyCount", 0); // 은총상자 카운트
        Debug.Log(silverBuyCount);

        long ticks = Convert.ToInt64(PlayerPrefs.GetString("silverCooltime", DateTime.Today.Ticks.ToString()));
        silverCooltime = new DateTime(ticks);
    }
    void Update()
    {
        UpdateUI();
    }

    bool withAdSkipCoupon = false;
    bool withoutAd = false;

    [ContextMenu("은총 리셋")]
    void ResetTime()
    {
        silverCooltime = DateTime.Now.AddHours(-1);
        AdManager.instance.RewardBackEvent -= BuyAfterAd;
        PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
    }

    private void UpdateUI()
    {
        if(silverBuyCount == 0)
        {
            //무료구매가능
            buyFreeBTN.gameObject.SetActive(true);
            buyAdBTN.gameObject.SetActive(false);
            coolTimeBox.SetActive(false);
        }
        else if(silverBuyCount > 0 && DateTime.Now > silverCooltime)
        {
            

            //Debug.Log("광고보기 가능" + DateTime.Now + "/" + silverCooltime);
            //광고보기 가능
            buyFreeBTN.gameObject.SetActive(false);
            buyAdBTN.gameObject.SetActive(true);
            buyAdBTN.interactable = true;
            if (!Managers.Data.MyStoreData.SkipAdActive && Managers.Data.MyStoreData.MySkipCouponAmount > 0)
            {
                buyAdBTN.GetComponent<Image>().sprite = Imgs[0];
                withAdSkipCoupon = true;
            }
            else if(Managers.Data.MyStoreData.SkipAdActive)
            {
                buyAdBTN.GetComponent<Image>().sprite = Imgs[1];
                withoutAd = true;
            }


            coolTimeBox.SetActive(false);
        }
        else if (silverBuyCount > 0 && DateTime.Now < silverCooltime)
        {
            //Debug.Log("광고쿨타임중" + DateTime.Now + "/" + silverCooltime);
            //광고쿨타임중
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
        if(withAdSkipCoupon)
        {
            Managers.Data.MyStoreData.MySkipCouponAmount--;
            Managers.Data.SaveAllDatas();
            buySilverGun();
            Managers.UI.ShowPopup(Define.Popup.PaySuccess);
            Pays.instance.Setting(Pays.Result.Success);
            silverCooltime = DateTime.Now.AddHours(4);
            PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
        }
        else if(withoutAd)
        {
            buySilverGun();
            Managers.UI.ShowPopup(Define.Popup.PaySuccess);
            Pays.instance.Setting(Pays.Result.Success);
            silverCooltime = DateTime.Now.AddHours(4);
            PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
        }
        else
        {
            
            Managers.UI.ShowPopup(Define.Popup.PaySuccess);
            Pays.instance.Setting(Pays.Result.Success);
            silverCooltime = DateTime.Now.AddHours(4);
            PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
            buySilverGun();
            AdManager.instance.ShowAd();
        }
    }

    void BuyAfterAd(object sender, EventArgs e)
    {
        
        Managers.UI.ShowPopup(Define.Popup.PaySuccess);
        Pays.instance.Setting(Pays.Result.Success);
        silverCooltime = DateTime.Now.AddHours(4);
        AdManager.instance.RewardBackEvent -= BuyAfterAd;
        PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
        buySilverGun();
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
        silverBuyCount++;
        Debug.Log(silverBuyCount);
        PlayerPrefs.SetInt("silverBuyCount", silverBuyCount);
        PlayerPrefs.Save();
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
    public void CloseBTn()
    {
        PlayerPrefs.SetInt("silverBuyCount", silverBuyCount);
        PlayerPrefs.SetString("silverCooltime", silverCooltime.Ticks.ToString());
        PlayerPrefs.Save();
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }

}
