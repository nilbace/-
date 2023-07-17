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
    public Sprite FrontImg;
    public Sprite BackImg;
    
    public ThreeValueDatas[] ThreeValues = new ThreeValueDatas[7];

    public int baseCritPer;

    public int[] GetPointMoneyValue = new int[10];
}

[System.Serializable]
public class CharSaveDatas
{
    public CharSaveData[] charSaveDatas;

    public CharSaveDatas()
    {
        charSaveDatas = new CharSaveData[6];
        for(int i = 0; i <6; i++)
        {
            charSaveDatas[i] = new CharSaveData();
        }
        charSaveDatas[0].bought = true;
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
