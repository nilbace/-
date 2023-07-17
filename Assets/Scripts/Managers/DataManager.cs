using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataManager 
{
    //스테이지 셀렉트
    public int SelectedBossindex { get; set; } = 1;
    public int SelectedCatIndex { get; set; } = (int)Define.CatName.Cheese;
    public BellData MyBellData { get; set; }
    public StoreData MyStoreData { get; set; }
    public SettingData MySettingData { get; set; }
    public StageHighScoreData MyHighScoreData { get; set; }
    public CharSaveDatas MyCharDatas { get; set; }


    public void Init()
    {
#if UNITY_EDITOR
        jsonDataPath = Path.Combine(Application.dataPath, "SaveDatas");
#else
        jsonDataPath = Path.Combine(Application.persistentDataPath, "SaveDatas");
#endif
        LoadAllData();
        CalculateAndAddBell();
    }



    #region AboutJson
    private string jsonDataPath;
    void LoadAllData()
    {
        LoadBellData();
        LoadStoreData();
        LoadSettingData();
        LoadStageHighScoreData();
        LoadCharDatas();
    }

    public void SaveDatas()
    {
        SaveBellData();
        SaveStoreData();
        SaveSettingData();
        SaveStageHighScoreData();
        SaveCharDatas();
    }

    #endregion

    #region About Bell

    public string LastTimeForBell()
    {
        string temp = (GetDateTime(MyBellData.BellPlusTime) - DateTime.Now).Minutes.ToString()
            + ":" + (GetDateTime(MyBellData.BellPlusTime) - DateTime.Now).Seconds.ToString();

        return temp; 
    }

    public void CalculateAndAddBell()
    {
        while(MyBellData.NowBellCount < 5 && DateTime.Now > GetDateTime(MyBellData.BellPlusTime))
        {
            MyBellData.NowBellCount++;
            MyBellData.BellPlusTime = GetDateTime(MyBellData.BellPlusTime).AddMinutes(30).ToString();
        }
        SaveBellData();
    }

    public bool CanUseBell()
    {
        if(MyBellData.NowBellCount > 0)
        {
            return true;
        }
        return false;
    }

    public void UseBell()
    {
        if (MyBellData.NowBellCount == 5)
        {
            MyBellData.BellPlusTime = GetDateTime(MyBellData.BellPlusTime).AddMinutes(30).ToString();
            MyBellData.NowBellCount--;
            SaveBellData();
        }
        else if (MyBellData.NowBellCount > 0)
        {
            MyBellData.NowBellCount--;
            SaveBellData();
        }
    }
    void SaveBellData()
    {
        string BellDataPath = Path.Combine(jsonDataPath, "bellData.json");
        if(MyBellData == null)
        {
            Debug.Log("no BellData");
            return;
        }
        string jsonData = JsonUtility.ToJson(MyBellData, true);
        File.WriteAllText(BellDataPath, jsonData);
    }
    void LoadBellData()
    {
        string filePath = Path.Combine(jsonDataPath, "bellData.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MyBellData = JsonUtility.FromJson<BellData>(jsonData);
        }
        else
        {
            MyBellData = new BellData();
            SaveBellData();
        }
    }

    public DateTime GetDateTime(string timeString)
    {
        DateTime bellUsedTime;
        if (DateTime.TryParse(timeString, out bellUsedTime))
        {
            return bellUsedTime; // 문자열을 DateTime으로 변환하여 반환
        }
        else
        {
            return DateTime.MinValue;
        }
    }
    #endregion

    #region Store
    void SaveStoreData()
    {
        string StoreDataPath = Path.Combine(jsonDataPath, "storeData.json");
        if (MyStoreData == null)
        {
            Debug.Log("no StoreData");
            return;
        }
        string jsonData = JsonUtility.ToJson(MyStoreData);
        File.WriteAllText(StoreDataPath, jsonData);
    }
    void LoadStoreData()
    {
        string filePath = Path.Combine(jsonDataPath, "storeData.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MyStoreData = JsonUtility.FromJson<StoreData>(jsonData);
        }
        else
        {
            MyStoreData = new StoreData();
            SaveStoreData();
        }
    }
    #endregion

    #region Setting
    public void SaveSettingData()
    {
        string settingDataPath = Path.Combine(jsonDataPath, "settingData.json");
        if (MySettingData == null)
        {
            Debug.Log("No SettingData");
            return;
        }
        string jsonData = JsonUtility.ToJson(MySettingData);
        File.WriteAllText(settingDataPath, jsonData);
    }

    void LoadSettingData()
    {
        string filePath = Path.Combine(jsonDataPath, "settingData.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MySettingData = JsonUtility.FromJson<SettingData>(jsonData);
        }
        else
        {
            MySettingData = new SettingData();
            SaveSettingData();
        }
    }
    #endregion


    #region HighScore
    public void SaveStageHighScoreData()
    {
        string stageHighScoreDataPath = Path.Combine(jsonDataPath, "stageHighScoreData.json");
        if (MyHighScoreData == null)
        {
            Debug.Log("No HighScore");
            return;
        }
        string jsonData = JsonUtility.ToJson(MyHighScoreData);
        File.WriteAllText(stageHighScoreDataPath, jsonData);
    }

    void LoadStageHighScoreData()
    {
        string filePath = Path.Combine(jsonDataPath, "stageHighScoreData.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MyHighScoreData = JsonUtility.FromJson<StageHighScoreData>(jsonData);
        }
        else
        {
            MyHighScoreData = new StageHighScoreData();
            SaveStageHighScoreData();
        }
    }

    #endregion

    #region CharDatas

    public void ResetCatStat()
    {
        for(int i = 0; i<6;i++)
        {
            int temp = GetThisCatStat((Define.StatName)i);
            CalThisCatStat((Define.StatName)i, -temp);
            CalThisCatStat(Define.StatName.extra, temp);
        }
        SaveDatas();
    }

    public int GetThisCatStat(Define.StatName statname)
    {
        if(statname == Define.StatName.Total)
        {
            int temp = 0;
            for(int i = 0; i < 7;i++)
            {
                temp += GetThisCatStat((Define.StatName)i);
            }
            return temp;
        }
        return MyCharDatas.charSaveDatas[SelectedCatIndex].StatLevels[(int)statname];
    }

    public void CalThisCatStat(Define.StatName statName, int n)
    {
        MyCharDatas.charSaveDatas[SelectedCatIndex].StatLevels[(int)statName] += n;
        SaveDatas();
    }
    
    public void SaveCharDatas()
    {
        string charDatapath = Path.Combine(jsonDataPath, "CharData.json");
        if (MyCharDatas == null)
        {
            Debug.Log("No HighScore");
            return;
        }
        string jsonData = JsonUtility.ToJson(MyCharDatas, true);
        File.WriteAllText(charDatapath, jsonData);
    }

    public void LoadCharDatas()
    {
        string filePath = Path.Combine(jsonDataPath, "CharData.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MyCharDatas = JsonUtility.FromJson<CharSaveDatas>(jsonData);
        }
        else
        {
            MyCharDatas = new CharSaveDatas();
            SaveCharDatas();
        }
    }

    #endregion
}

[System.Serializable]
public class BellData
{
    public int NowBellCount;
    public string BellPlusTime;
    public BellData()
    {
        this.NowBellCount = 5;
        BellPlusTime = DateTime.Now.ToString();
    }
}

[System.Serializable]
public class StoreData
{
    public int MyGoldAmount;
    public int MyRubyAmount;

    public StoreData()
    {
        MyGoldAmount = 0;
        MyRubyAmount = 0;
    }
}

[System.Serializable]
public class SettingData
{
    public bool isFixedJoystick;
    public float BGMSound;
    public float SFXSound;

    public SettingData()
    {
        this.isFixedJoystick = true;
        BGMSound = 1f;
        SFXSound = 1f;
    }
}

[System.Serializable]
public class StageHighScoreData
{
    public int[] HighScores = new int[12];
    public int clearStageIndex;
    public bool[] GetReward = new bool[36];

    public StageHighScoreData()
    {
        for (int i = 0; i < HighScores.Length; i++)
        {
            HighScores[i] = 0;
        }
        clearStageIndex = 0;
        for (int i = 0; i < GetReward.Length; i++)
        {
            GetReward[i] = false;
        }
    }
}



