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

    public string SkillInfos;
    public Sprite SkillIcon;
    public string Skill2Infos;
    public Sprite Skill2Icon;
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

        charSaveDatas[0].Name = "ġ�����";
        charSaveDatas[1].Name = "�������";
        charSaveDatas[2].Name = "�������";
        charSaveDatas[3].Name = "�νõ�����";
        charSaveDatas[4].Name = "��";
        charSaveDatas[5].Name = "��";


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
