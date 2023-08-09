using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Revive : MonoBehaviour
{
    [SerializeField] Sprite ReviveTicketBTNIMg;
    [SerializeField] Button ReviveBTN;

    private void Start()
    {
        SetButton();
    }
    public void ReviveWithAD()
    {

    }

    public void StageFail()
    {
        
    }

    void SetButton()
    {
        if(GameScene.instance.hasReviveCoupon)
        {
            ReviveBTN.GetComponent<Image>().sprite = ReviveTicketBTNIMg;
            ReviveBTN.onClick.AddListener(() => ReviveWithTicket());
        }
        else
        {
            ReviveBTN.onClick.AddListener(() => { });
        }
    }

    void ReviveWithTicket()
    {
        Managers.Data.MyStoreData.MyReviveTicKetAmount--;
        GameScene.instance.Revive();
        Managers.Data.SaveAllDatas();

    }
}
