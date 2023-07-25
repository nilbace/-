using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public static string FormatNumber(int value)
    {
        if (value < 1000)
        {
            return value.ToString();
        }
        else
        {
            string formattedNumber = value.ToString("N0");
            return formattedNumber;
        }
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        StageSelect,
        Game,
    }

    public enum Popup
    { 
        none,
        Setting,
        Selected,
        CharPet,
        CharUpgrade,
        Pause,
        Quit,
        D_Continue,
        StageClear,
        StageFail,
        MaxCount
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    [System.Serializable]
    public struct BulletPosition
    {
        public Vector3 Level1;
        public Vector3 Level2_1;
        public Vector3 Level2_2;
        public Vector3 Level3_1;
        public Vector3 Level3_2;
        public Vector3 Level3_3;
        public Vector3 Level3_4;
    }

    [System.Serializable]
    public struct MonsterWave
    {
        public WaveData[] waveDatas;
    }

    [System.Serializable]
    public struct WaveData
    {
        public string WaveName;
        public GameObject WaveMonster;
        public int MonsterHP;
        public float WaveMoveSpeed;
        public Vector3 WaveDir;
        public Vector2 SpawnPoint;
    }


    [System.Serializable]
    public class CharData {
        public string CharName;

        public int CharBaseATK;
        public int CharUPStatATK;

        public Sprite frontImg;
        public Sprite backImg;
    }

    public enum BossName { 
    none,
    Pig,
    Dog,
    Chick,
    Monkey,
    Sheep,
    Horse,
    Snake,
    Dragon,
    Rabbit,
    Tiger,
    Cow,
    Rat,
    MaxCount,
         
    }

    [System.Serializable]
    public class StageInfoData
    {
        public string BossName;
        public int ThreeStarScore;
        public int TwoStarScore;
        public int OneStarScore;

        public int OneStarReward;
        public int TwoStarReward;
        public int ThreeStarReward;
        public Sprite BossImg;
    }

    public enum StatName
    {
        atkPower,
        atkSpeed,
        CritDmg,
        SkillDmg,
        GoldBonus,
        HpBonus,
        extra,
        Total,
        MaxCount
    }

    public enum CatName
    { 
        Cheese,
        ThreeColor,
        Mackerel,
        Tuxedo,
        Siamese,
        Bengal,
        MaxCount
    }

    public enum DropItems
    {
        DropGold,

    }

    [System.Serializable]
    public class CharBomb
    {
        public GameObject BombGO;
        public Sprite BombSprite;
    }


}
