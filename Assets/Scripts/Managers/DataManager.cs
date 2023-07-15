using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataManager 
{
    //스테이지 셀렉트
    public int SelectedBossindex { get; set; } = 1;
    public BellData MyBellData { get; set; }
    public StoreData MyStoreData { get; set; }
    public SettingData MySettingData { get; set; }

    private string jsonDataPath;


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
    
    void LoadAllData()
    {
        LoadBellData();
        LoadStoreData();
        LoadSettingData();
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

    public bool UseBell()
    {
        if(MyBellData.NowBellCount == 5)
        {
            MyBellData.BellPlusTime = GetDateTime(MyBellData.BellPlusTime).AddMinutes(30).ToString();
            MyBellData.NowBellCount--;
            SaveBellData();
            return true;
        }
        else if(MyBellData.NowBellCount > 0)
        {
            MyBellData.NowBellCount--;
            SaveBellData();
            return true;
        }
        return false;
    }
    void SaveBellData()
    {
        string BellDataPath = Path.Combine(jsonDataPath, "bellData.json");
        if(MyBellData == null)
        {
            Debug.Log("no BellData");
            return;
        }
        string jsonData = JsonUtility.ToJson(MyBellData);
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
        if (MyBellData == null)
        {
            Debug.Log("no BellData");
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
        if (MyBellData == null)
        {
            Debug.Log("No BellData");
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



