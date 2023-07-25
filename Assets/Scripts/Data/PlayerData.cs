using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<InGameData> Chars;

}



[System.Serializable]
public class InGameData
{
    public string CatName;
    public Sprite FrontImg;
    public Sprite BackImg;
    
    public ThreeValueDatas[] ThreeValues = new ThreeValueDatas[7];

    public int baseCritPer;

    public int[] GetPointMoneyValue = new int[10];

    public string SkillName;
    public string SkillInfos;
    public Sprite SkillIcon;
}

[System.Serializable]
public class CharSaveDatas
{
    public CharSaveData[] charSaveDatas;
    public int nowSelectCatIndex;

    public CharSaveDatas()
    {
        charSaveDatas = new CharSaveData[6];
        for(int i = 0; i <6; i++)
        {
            charSaveDatas[i] = new CharSaveData();
        }
        charSaveDatas[0].bought = true;

        charSaveDatas[0].Name = "Ä¡Áî³ÉÀÌ";
        charSaveDatas[1].Name = "»ï»ö³ÉÀÌ";
        charSaveDatas[2].Name = "°íµî¾î³ÉÀÌ";
        charSaveDatas[3].Name = "ÅÎ½Ãµµ³ÉÀÌ";
        charSaveDatas[4].Name = "¼¤";
        charSaveDatas[5].Name = "¹ð°¥";


        nowSelectCatIndex = 0;
    }
}


[System.Serializable]
public class CharSaveData
{
    public string Name;
    public bool bought;
    public int TotalStatPoint;
    public int[] StatLevels = new int[7];

    public CharSaveData()
    {
        Name = null;
        this.bought = false;
        this.TotalStatPoint = 0;
        for (int i = 0; i < 7; i++)
        {
            StatLevels[i] = 0;
        }
    }
}

[System.Serializable]
public class ThreeValueDatas
{
    public string StatName;
    public int baseStat;
    public int UpValue;
    public int MaxValue;
}
