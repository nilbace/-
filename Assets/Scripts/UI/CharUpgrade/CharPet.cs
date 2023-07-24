using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class CharPet : MonoBehaviour
{
    [SerializeField] PlayerData PlayerData;
    InGameData thiscatData;
    [SerializeField] Slider[] StatSliders;
    [SerializeField] GameObject[] growlevel;
    [SerializeField] int[] MaxValues;
    public static CharPet instance;

    private void Awake()
    {
        instance = this;
        thiscatData = PlayerData.Chars[Managers.Data.SelectedCatIndex];
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
    private void Start()
    {
       Init();
    }

    public void Init()
    {
        for(int i = 0; i<6;i++)
        {
            StatSliders[i].value = (float)GetResultStat(i) / (float)MaxValues[i];
        }
        StatSliders[6].value = (float)(thiscatData.baseCritPer / 10f);

        int _temp = Managers.Data.GetThisCatStat(Define.StatName.Total);
        for(int i =0; i<_temp; i++)
        {
            growlevel[i].SetActive(true);
        }
    }

    int GetResultStat(int n)
    {
        int temp = 0;

        temp += thiscatData.ThreeValues[n].baseStat;
        temp += Managers.Data.GetThisCatStat((Define.StatName)n) * thiscatData.ThreeValues[n].UpValue;

        return temp;
    }
    public void CloseBTN()
    {
        Managers.UI.ClosePopup();
    }

    public void UpgradeBTN()
    {
        Managers.UI.ShowPopup(Define.Popup.CharUpgrade);
    }

    public void ChangeBTN()
    {
        
    }
}
