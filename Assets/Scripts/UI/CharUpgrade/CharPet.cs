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

    [SerializeField] Image CharSprite;
    [SerializeField] TMP_Text CharName;

    [SerializeField] Slider[] StatSliders;
    [SerializeField] GameObject[] growlevel;

    [Header("½ºÅ³")]
    [SerializeField] TMP_Text skillName;
    [SerializeField] TMP_Text skillInfo;
    [SerializeField] Image    skillImg;


    [SerializeField] int[] MaxValues;

    [SerializeField] CP_EachChar[] eachcar;
    public static CharPet instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       Init();
    }

    public void Init()
    {
        thiscatData = PlayerData.Chars[Managers.Data.MyCharDatas.nowSelectCatIndex];

        int _temp = Managers.Data.GetThisCatStat(Define.StatName.Total);

        CharSprite.sprite = thiscatData.FrontImg;
        CharName.text = "Lv." + _temp.ToString() + " " + thiscatData.CatName;

        for(int i = 0; i<6;i++)
        {
            StatSliders[i].value = (float)GetResultStat(i) / (float)MaxValues[i];
        }
        StatSliders[6].value = (float)(thiscatData.baseCritPer / 10f);

        
        for(int i = 0; i<10;i++)
        {
            growlevel[i].SetActive(false);
        }
        for(int i =0; i<_temp; i++)
        {
            growlevel[i].SetActive(true);
        }

        skillName.text  = thiscatData.SkillName;
        skillInfo.text  = thiscatData.SkillInfos;
        skillImg.sprite = thiscatData.SkillIcon;

        foreach(CP_EachChar temp in eachcar)
        {
            temp.Init();
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
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ClosePopup();
    }

    public void UpgradeBTN()
    {
        TempSound.instance.SFX(TempSound.EffectSoundName.button1);
        Managers.UI.ShowPopup(Define.Popup.CharUpgrade);
    }

    public void ChangeBTN()
    {
        
    }
}
