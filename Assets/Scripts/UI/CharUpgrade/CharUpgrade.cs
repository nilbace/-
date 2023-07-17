using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharUpgrade : MonoBehaviour
{
    public PlayerData playerData;
    InGameData _NowCatData;
    public UpgradeList[] UpgradeLists;
    public TMP_Text ReqGoldToGetPoint;
    public TMP_Text ExtraPoint;

    public static CharUpgrade instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _NowCatData = playerData.Chars[Managers.Data.SelectedCatIndex];
        Init();
    }

    public void Init()
    {
        ReqGoldToGetPoint.text = Define.FormatNumber(_NowCatData.GetPointMoneyValue
            [Managers.Data.GetThisCatStat(Define.StatName.Total)]);

        ExtraPoint.text = "�ܿ�����Ʈ : " + Managers.Data.GetThisCatStat(Define.StatName.extra).ToString();

        for(int i = 0; i<6; i++)
        {
            UpgradeLists[i].Setting(_NowCatData, i);
        }
    }
    public void CloseBTN()
    {
        Managers.UI.ClosePopup();
    }

    public void GetPointByMoney()
    {
        if(Managers.Data.MyStoreData.MyGoldAmount >= _NowCatData.GetPointMoneyValue
            [Managers.Data.MyCharDatas.charSaveDatas[Managers.Data.SelectedCatIndex].StatLevels[6]]
            
            && Managers.Data.GetThisCatStat(Define.StatName.Total) <= 9)
        {
            
            Managers.Data.MyStoreData.MyGoldAmount -= _NowCatData.GetPointMoneyValue
            [Managers.Data.MyCharDatas.charSaveDatas[Managers.Data.SelectedCatIndex].StatLevels[6]];

            Managers.Data.CalThisCatStat(Define.StatName.extra, 1);

        }
        Init();
    }
}
