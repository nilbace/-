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
    public BellData MyBellData { get; set; }
    public StoreData MyStoreData { get; set; }
    public SettingData MySettingData { get; set; }
    public StageHighScoreData MyHighScoreData { get; set; }
    public HousingData MyHousingData { get; set; }
    public CharSaveDatas MyCharDatas { get; set; }
    public MailData MyMailData { get; set; }
    public PetStat MyPetStat { get; set; }



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
        LoadHousingData();
        LoadCharDatas();
        LoadMailData();
    }

    public void SaveAllDatas()
    {
        SaveBellData();
        SaveStoreData();
        SaveSettingData();
        SaveStageHighScoreData();
        SaveHousingData();
        SaveCharDatas();
        SaveMailData();
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
        while (MyBellData.NowBellCount < 5 && DateTime.Now > GetDateTime(MyBellData.BellPlusTime))
        {
            MyBellData.NowBellCount++;
            MyBellData.BellPlusTime = GetDateTime(MyBellData.BellPlusTime).AddMinutes(30).ToString();
        }
        SaveBellData();
    }

    public bool CanUseBell()
    {
        if (MyBellData.NowBellCount > 0)
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

        if (!File.Exists(path))
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

    #region HighScoreAndPetStat
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

    #region Housing
    public void SaveHousingData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyHousingData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyHousingData.json");
        }
        string jsonData = JsonUtility.ToJson(MyHousingData, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    void LoadHousingData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyHousingData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyHousingData.json");
        }

        if (!File.Exists(path))
        {
            MyHousingData = new HousingData();
            SaveHousingData();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        MyHousingData = JsonUtility.FromJson<HousingData>(jsonData);
    }
    #endregion

    #region CharDatas

    public void ResetCatStat()
    {
        for (int i = 0; i < 6; i++)
        {
            int temp = GetThisCatStat((Define.StatName)i);
            CalThisCatStat((Define.StatName)i, -temp);
            CalThisCatStat(Define.StatName.extra, temp);
        }
        SaveAllDatas();
    }

    public string GetNowCatName()
    {
        return ((Define.CatName)MyCharDatas.nowSelectCatIndex).ToString();
    }

    public int GetCatStat(Define.CatName catname, Define.StatName statname)
    {
        if (statname == Define.StatName.Total)
        {
            int temp = 0;
            for (int i = 0; i < 7; i++)
            {
                temp += GetCatStat(catname, (Define.StatName)i);
            }
            return temp;
        }
        return MyCharDatas.charSaveDatas[(int)catname].StatLevels[(int)statname];
    }

    public int GetThisCatStat(Define.StatName statname)
    {
        if (statname == Define.StatName.Total)
        {
            int temp = 0;
            for (int i = 0; i < 7; i++)
            {
                temp += GetThisCatStat((Define.StatName)i);
            }
            return temp;
        }
        return MyCharDatas.charSaveDatas[MyCharDatas.nowSelectCatIndex].StatLevels[(int)statname];
    }

    public void CalThisCatStat(Define.StatName statName, int n)
    {
        MyCharDatas.charSaveDatas[MyCharDatas.nowSelectCatIndex].StatLevels[(int)statName] += n;
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

    #region Mail
    void SaveMailData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyMailData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyMailData.json");
        }
        string jsonData = JsonUtility.ToJson(MyMailData, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void LoadMailData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "MyMailData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "MyMailData.json");
        }

        if (!File.Exists(path))
        {
            MyMailData = new MailData();
            SaveMailData();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        MyMailData = JsonUtility.FromJson<MailData>(jsonData);
    }

    public void GetAndDestroyMail(int i)
    {
        MyMailData.MailBox.RemoveAt(i);
        MyMailData.MailAmount--;
        SaveAllDatas();
    }

    public void MakeAndAddMail(int gold = 0, int ruby = 0, int bell = 0, int sweep = 0, int skip = 0, int revive = 0, string text = null)
    {
        OneMail temp = new OneMail();
        temp.CoinAmount = gold;
        temp.RubyAmount = ruby;
        temp.BellAmount = bell;
        temp.SweepAmount = sweep;
        temp.SkipCouponAmount = skip;
        temp.ReviveTicketAmount = revive;
        temp.MailText = text;
        MyMailData.MailBox.Add(temp);
        MyMailData.MailAmount++;
        SaveAllDatas();
    }



    #endregion

    #region PetStat
    public PetStat GetPetResultStat()
    {
        MyPetStat = new PetStat();
        int nowclearindex = MyHighScoreData.clearStageIndex;
        for (int i = 0; i <= nowclearindex - 1; i++)
        {
            if (i == 12) continue;
            switch (i)
            {
                case 0:
                    AddPetStat(5, 0, 5, 0, 0, 0);
                    break;
                case 1:
                    AddPetStat(0, 0, 0, 5, 1, 0);
                    break;
                case 2:
                    AddPetStat(0, 1, 5, 0, 0, 0);
                    break;
                case 3:
                    AddPetStat(0, 2, 0, 0, 1, 0);
                    break;
                case 4:
                    AddPetStat(10, 0, 5, 0, 0, 0);
                    break;
                case 5:
                    AddPetStat(0, 3, 0, 0, 1, 0);
                    break;
                case 6:
                    AddPetStat(10, 0, 0, 10, 0, 0);
                    break;
                case 7:
                    AddPetStat(10, 4, 10, 0, 2, 0);
                    break;
                case 8:
                    AddPetStat(15, 5, 0, 10, 2, 0);
                    break;
                case 9:
                    AddPetStat(15, 7, 10, 10, 0, 0);
                    break;
                case 10:
                    AddPetStat(15, 8, 0, 15, 3, 0);
                    break;
                case 11:
                    AddPetStat(20, 10, 15, 0, 0, 10);
                    break;
            }
        }
        return MyPetStat;
    }

    void AddPetStat(int atk, int critper, int atkspeed, int goldB, int score, int life)
    {
        MyPetStat.petatk += atk;
        MyPetStat.petCritper += critper;
        MyPetStat.petAtkSpeed += atkspeed;
        MyPetStat.petGoldBonus += goldB;
        MyPetStat.ScoreBonus += score;
        MyPetStat.HeartBonus += life;
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
    public int MySweepTicketAmount;
    public int MyReviveTicKetAmount;
    public int MySkipCouponAmount;
    public bool NyaongjimaCouponUsed;
    public StoreData()
    {
        MyGoldAmount = 0;
        MyRubyAmount = 0;
        MySweepTicketAmount = 0;
        MyReviveTicKetAmount = 0;
        MySkipCouponAmount = 0;
        NyaongjimaCouponUsed = false;
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
    public int[] HighGoldScores = new int[12];
    public int clearStageIndex;
    public bool[] GetReward = new bool[36];

    public StageHighScoreData()
    {
        clearStageIndex = -1;
        for (int i = 0; i < HighScores.Length; i++)
        {
            HighScores[i] = 0;
            HighGoldScores[i] = 0;
        }
        clearStageIndex = 0;
        for (int i = 0; i < GetReward.Length; i++)
        {
            GetReward[i] = false;
        }
    }
    
}


[System.Serializable]
public class HousingData
{
    public bool[] houses = new bool[13];
    public bool[] talkBox = new bool[13];

    public HousingData()
    {
        for (int i = 0; i < houses.Length; i++)
        {
            houses[i] = false;
        }
        for (int i = 0; i < talkBox.Length - 1; i++)
        {
            talkBox[i] = false;
        }
        talkBox[12] = true;
    }

}

[System.Serializable]
public class OneMail
{
    public int CoinAmount;
    public int RubyAmount;
    public int BellAmount;
    public int SweepAmount;
    public int SkipCouponAmount;
    public int ReviveTicketAmount;
    public string MailText;

    public OneMail()
    {
        CoinAmount = 0;
        RubyAmount = 0;
        BellAmount = 0;
        SweepAmount = 0;
        SkipCouponAmount = 0;
        ReviveTicketAmount = 0;
        MailText = null;
    }
}

[System.Serializable]
public class MailData
{
    public List<OneMail> MailBox = new List<OneMail>();
    public int MailAmount;

    public MailData()
    {
        
        MailAmount = 0;
    }
}

[System.Serializable]
public class PetStat
{
    public int petatk;
    public int petCritper;
    public int petAtkSpeed;
    public int petGoldBonus;
    public int ScoreBonus;
    public int HeartBonus;
}

