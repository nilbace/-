using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class DataManager 
{
    //스테이지 셀렉트
    public int SelectedBossindex { get; set; } = 0;
    public int SelectedCatIndex { get; set; } = (int)Define.CatName.Cheese;
    public BellData MyBellData { get; set; }
    public StoreData MyStoreData { get; set; }
    public SettingData MySettingData { get; set; }
    public StageHighScoreData MyHighScoreData { get; set; }
    public CharSaveDatas MyCharDatas { get; set; }
    


    public void Init()
    {
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

    public void SaveAllDatas()
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
            MyBellData.BellPlusTime = DateTime.Now.AddMinutes(30).ToString();
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
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyBellData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyBellData.json");
        }
        string jsonData = JsonUtility.ToJson(MyBellData, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    void LoadBellData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyBellData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyBellData.json");
        }

        if(!File.Exists(path))
        {
            MyBellData = new BellData();
            SaveBellData();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        MyBellData = JsonUtility.FromJson<BellData>(jsonData);
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
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyStoreData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyStoreData.json");
        }
        string jsonData = JsonUtility.ToJson(MyStoreData, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    void LoadStoreData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyStoreData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyStoreData.json");
        }

        if (!File.Exists(path))
        {
            MyStoreData = new StoreData();
            SaveStoreData();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        MyStoreData = JsonUtility.FromJson<StoreData>(jsonData);
    }
    #endregion

    #region Setting
    public void SaveSettingData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MySettingData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MySettingData.json");
        }
        string jsonData = JsonUtility.ToJson(MySettingData, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void LoadSettingData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MySettingData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MySettingData.json");
        }

        if (!File.Exists(path))
        {
            MySettingData = new SettingData();
            SaveSettingData();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        MySettingData = JsonUtility.FromJson<SettingData>(jsonData);
    }
    #endregion


    #region HighScore
    public void SaveStageHighScoreData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyHighScoreData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyHighScoreData.json");
        }
        string jsonData = JsonUtility.ToJson(MyHighScoreData, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void LoadStageHighScoreData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyHighScoreData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyHighScoreData.json");
        }

        if (!File.Exists(path))
        {
            MyHighScoreData = new StageHighScoreData();
            SaveStageHighScoreData();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        MyHighScoreData = JsonUtility.FromJson<StageHighScoreData>(jsonData);
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
        SaveAllDatas();
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
        SaveAllDatas();
    }
    
    public void SaveCharDatas()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyCharDatas.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyCharDatas.json");
        }
        string jsonData = JsonUtility.ToJson(MyCharDatas, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public void LoadCharDatas()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyCharDatas.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyCharDatas.json");
        }

        if (!File.Exists(path))
        {
            MyCharDatas = new CharSaveDatas();
            SaveCharDatas();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        MyCharDatas = JsonUtility.FromJson<CharSaveDatas>(jsonData);
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
        clearStageIndex = -1;
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



