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

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,
        
    }
    public enum MouseEvent{
        Press,
        Click,
    }
    public enum CameraMode{
        QuaterView,
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
        public GameObject BossMonster;
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
}
