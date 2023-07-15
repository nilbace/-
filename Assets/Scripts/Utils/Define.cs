using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{

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

}
