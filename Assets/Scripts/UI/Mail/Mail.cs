using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mail : MonoBehaviour
{
    [SerializeField] TMP_Text mailText;
    [SerializeField] GameObject[] Items;
    OneMail thisMail;
    int mailindex;

    public void Setting(OneMail mail, int i)
    {
        thisMail = mail;  mailindex = i;
        UpdateItem(0, thisMail.CoinAmount);
        UpdateItem(1, thisMail.RubyAmount);
        UpdateItem(2, thisMail.BellAmount);
        UpdateItem(3, thisMail.SweepAmount);
        UpdateItem(4, thisMail.SkipCouponAmount);
        UpdateItem(5, thisMail.ReviveTicketAmount);
        mailText.text = thisMail.MailText;
    }

    private void UpdateItem(int index, int amount)
    {
        if (amount == 0)
        {
            Items[index].SetActive(false);
        }
        else
        {
            Items[index].SetActive(true);
            Items[index].GetComponentInChildren<TMP_Text>().text = amount.ToString();
        }
    }

    public void GetMail()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.Data.MyStoreData.MyGoldAmount += thisMail.CoinAmount;
        Managers.Data.MyStoreData.MyRubyAmount += thisMail.RubyAmount;
        Managers.Data.MyBellData.NowBellCount += thisMail.BellAmount;
        Managers.Data.MyStoreData.MySweepTicketAmount += thisMail.SweepAmount;
        Managers.Data.MyStoreData.MySkipCouponAmount += thisMail.SkipCouponAmount;
        Managers.Data.MyStoreData.MyReviveTicKetAmount += thisMail.ReviveTicketAmount;

        Managers.Data.GetAndDestroyMail(mailindex);
        MailBox.instance.Init();
    }
}

